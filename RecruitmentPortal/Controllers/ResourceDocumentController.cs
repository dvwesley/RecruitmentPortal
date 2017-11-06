using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentPortal.Models;
using RecruitmentPortal.Common;


namespace RecruitmentPortal.Controllers
{
    public class ResourceDocumentController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /ResourceDocument/
        public ActionResult Index(int resourceId)
        {
            try
            {
                ViewBag.ResourceId = resourceId;
                var resourcedocuments = db.ResourceDocuments.Include(r => r.DocumentType).Include(r => r.Resource).Where(d => d.ResourceId == resourceId);
                if (resourcedocuments != null && resourcedocuments.Count() > 0)
                {

                    ViewBag.ResourceName = resourcedocuments.FirstOrDefault().Resource.AspNetUser.FirstName + " " + resourcedocuments.FirstOrDefault().Resource.AspNetUser.MiddleInitial + " "
                        + resourcedocuments.FirstOrDefault().Resource.AspNetUser.LastName;
                }
                else
                {
                    var resource = db.Resources.Find(resourceId);
                    ViewBag.ResourceName = resource.AspNetUser.FirstName + " " + resource.AspNetUser.MiddleInitial + " " + resource.AspNetUser.LastName;
                }
                return View(resourcedocuments.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ResourceDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceDocument resourcedocument = db.ResourceDocuments.Find(id);
            if (resourcedocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(resourcedocument);
        }

        // GET: /ResourceDocument/Create
        public ActionResult Upload(int resourceId)
        {
            try
            {
                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.ResourceDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                var resourceDoc = new ResourceDocument();
                resourceDoc.ResourceId = resourceId;
                return View(resourceDoc);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /ResourceDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload([Bind(Include = "ResourceDocumentId,ResourceId,DocumentTypeId,Description")] ResourceDocument resourcedocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        foreach (string fileName in Request.Files)
                        {
                            HttpPostedFileBase file = file = Request.Files[fileName];
                            if (file != null && file.ContentLength > 0)
                            {
                                if (db.ResourceDocuments.Where(d => d.DocumentTypeId == resourcedocument.DocumentTypeId && d.Description == resourcedocument.Description).Count() > 0)
                                {
                                    ModelState.AddModelError("Description", "The Description specified already exists.");
                                    ViewBag.DocumentTypes = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", resourcedocument.DocumentTypeId);
                                    return View(resourcedocument);
                                }
                                var allowedExtensions = new Utility().DocumentExtensions;
                                var extension = Path.GetExtension(file.FileName);
                                if (!allowedExtensions.Contains(extension))
                                {
                                    ModelState.AddModelError("FilePath", "The extension " + extension + " files are not allowed.");
                                    ViewBag.DocumentTypes = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", resourcedocument.DocumentTypeId);
                                    return View(resourcedocument);
                                }
                                string path = resourcedocument.DocumentPath + resourcedocument.ResourceId.ToString() + "_" + resourcedocument.DocumentTypeId.ToString() + "_"
                                    + DateTime.Now.ToString("MMddyyyyhhmmss") + "_" + Path.GetFileName(file.FileName);
                                string savedFileName = Path.Combine(Server.MapPath("~" + path));

                                file.SaveAs(savedFileName);
                                resourcedocument.ContentType = file.ContentType;
                                resourcedocument.FilePath = path;
                                resourcedocument.FileName = file.FileName;
                                resourcedocument.UploadedBy = User.Identity.GetUserId();
                                resourcedocument.UploadedTimestamp = DateTime.Now;
                                db.ResourceDocuments.Add(resourcedocument);
                                db.SaveChanges();

                                //update checklist for the document.
                                return RedirectToAction("UpdateChecklist", "Resource", new { resourceId = resourcedocument.ResourceId, documentTypeId = resourcedocument.DocumentTypeId, flag = true });

                                //return RedirectToAction("Index", "Resource");
                            }
                        }
                    }
                }

                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.OrderBy(d => d.Name), "DocumentTypeId", "Name", resourcedocument.DocumentTypeId);
                return View(resourcedocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ResourceDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceDocument resourcedocument = db.ResourceDocuments.Find(id);
            if (resourcedocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", resourcedocument.DocumentTypeId);
            ViewBag.ResourceId = new SelectList(db.Resources, "ResourceId", "FirstName", resourcedocument.ResourceId);
            return View(resourcedocument);
        }

        // POST: /ResourceDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ResourceDocumentId,ResourceId,DocumentTypeId,Description,FilePath")] ResourceDocument resourcedocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(resourcedocument).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", resourcedocument.DocumentTypeId);
                ViewBag.ResourceId = new SelectList(db.Resources, "ResourceId", "FirstName", resourcedocument.ResourceId);
                return View(resourcedocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ResourceDocument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceDocument resourcedocument = db.ResourceDocuments.Find(id);
            if (resourcedocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(resourcedocument);
        }

        // POST: /ResourceDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ResourceDocument resourcedocument = db.ResourceDocuments.Find(id);
                string path = Request.MapPath(resourcedocument.FilePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                db.ResourceDocuments.Remove(resourcedocument);
                db.SaveChanges();

                return RedirectToAction("UpdateChecklist", "Resource", new { resourceId = resourcedocument.ResourceId, documentTypeId = resourcedocument.DocumentTypeId, flag = false });
                //return RedirectToAction("Index", new { resourceId = resourcedocument.ResourceId });
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

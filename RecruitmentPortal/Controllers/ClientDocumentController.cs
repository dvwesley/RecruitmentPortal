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
    public class ClientDocumentController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /ClientDocument/
        public ActionResult Index(int clientId)
        {
            try
            {
                ViewBag.ClientId = clientId;
                var clientdocuments = db.ClientDocuments.Include(c => c.DocumentType).Include(c => c.Client).Where(d => d.ClientId == clientId);
                var client = db.Clients.Find(clientId);
                ViewBag.DocumentTitle = "Documents list for - " + client.Name;
                return View(clientdocuments.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ClientDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ClientDocument clientdocument = db.ClientDocuments.Find(id);
            if (clientdocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(clientdocument);
        }

        // GET: /ResourceDocument/Create
        public ActionResult Upload(int clientId)
        {
            try
            {
                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.ClientDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                var clientDoc = new ClientDocument();
                clientDoc.ClientId = clientId;
                return View(clientDoc);
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
        public ActionResult Upload([Bind(Include = "ClientDocumentId,ClientId,DocumentTypeId,Description")] ClientDocument clientdocument)
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
                                var allowedExtensions = new Utility().DocumentExtensions;
                                var extension = Path.GetExtension(file.FileName);
                                if (!allowedExtensions.Contains(extension))
                                {
                                    ModelState.AddModelError("FilePath", "The extension " + extension + " files are not allowed.");
                                    ViewBag.DocumentTypes = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", clientdocument.DocumentTypeId);
                                    return View(clientdocument);
                                }
                                string path = clientdocument.DocumentPath + clientdocument.ClientId.ToString() + "_" + clientdocument.DocumentTypeId.ToString() + "_"
                                    + DateTime.Now.ToString("MMddyyyyhhmmss") + "_" + Path.GetFileName(file.FileName);
                                string savedFileName = Path.Combine(Server.MapPath("~" + path));

                                file.SaveAs(savedFileName);
                                clientdocument.ContentType = file.ContentType;
                                clientdocument.FilePath = path;
                                clientdocument.FileName = file.FileName;
                                clientdocument.UploadedBy = User.Identity.GetUserId();
                                clientdocument.UploadedTimestamp = DateTime.Now;
                                db.ClientDocuments.Add(clientdocument);
                                db.SaveChanges();

                                return RedirectToAction("Index", new { clientId = clientdocument.ClientId });
                            }
                        }
                    }
                }
                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.ClientDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                return View(clientdocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ClientDocument/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name");
                return View();
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /ClientDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClientDocumentId,ClientId,DocumentTypeId,FilePath")] ClientDocument clientdocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ClientDocuments.Add(clientdocument);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", clientdocument.ClientId);
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", clientdocument.DocumentTypeId);
                return View(clientdocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ClientDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ClientDocument clientdocument = db.ClientDocuments.Find(id);
            if (clientdocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", clientdocument.ClientId);
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", clientdocument.DocumentTypeId);
            return View(clientdocument);
        }

        // POST: /ClientDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClientDocumentId,ClientId,DocumentTypeId,FilePath")] ClientDocument clientdocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(clientdocument).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", clientdocument.ClientId);
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", clientdocument.DocumentTypeId);
                return View(clientdocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ClientDocument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ClientDocument clientdocument = db.ClientDocuments.Find(id);
            if (clientdocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(clientdocument);
        }

        // POST: /ClientDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ClientDocument clientdocument = db.ClientDocuments.Find(id);
                string path = Request.MapPath(clientdocument.FilePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                db.ClientDocuments.Remove(clientdocument);
                db.SaveChanges();
                return RedirectToAction("Index", new { clientId = clientdocument.ClientId });
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

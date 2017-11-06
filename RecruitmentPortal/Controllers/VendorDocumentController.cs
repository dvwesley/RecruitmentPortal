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
    public class VendorDocumentController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /VendorDocument/
        public ActionResult Index(int vendorId)
        {
            try
            {
                ViewBag.VendorId = vendorId;
                var vendordocuments = db.VendorDocuments.Include(v => v.DocumentType).Include(v => v.Vendor).Where(d => d.VendorId == vendorId);
                var vendor = db.Vendors.Find(vendorId);
                ViewBag.DocumentTitle = "Documents list for - " + vendor.Name;
                return View(vendordocuments.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /VendorDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            VendorDocument vendordocument = db.VendorDocuments.Find(id);
            if (vendordocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(vendordocument);
        }

        public ActionResult Upload(int vendorId)
        {
            try
            {
                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.VendorDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                var vendorDoc = new VendorDocument();
                vendorDoc.VendorId = vendorId;
                return View(vendorDoc);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }
                
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload([Bind(Include = "VendorDocumentId,VendorId,DocumentTypeId,Description")] VendorDocument vendordocument)
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
                                    ViewBag.DocumentTypes = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", vendordocument.DocumentTypeId);
                                    return View(vendordocument);
                                }
                                string path = vendordocument.DocumentPath + vendordocument.VendorId.ToString() + "_" + vendordocument.DocumentTypeId.ToString() + "_"
                                    + DateTime.Now.ToString("MMddyyyyhhmmss") + "_" + Path.GetFileName(file.FileName);
                                string savedFileName = Path.Combine(Server.MapPath("~" + path));

                                file.SaveAs(savedFileName);
                                vendordocument.ContentType = file.ContentType;
                                vendordocument.FilePath = path;
                                vendordocument.FileName = file.FileName;
                                vendordocument.UploadedBy = User.Identity.GetUserId();
                                vendordocument.UploadedTimestamp = DateTime.Now;
                                db.VendorDocuments.Add(vendordocument);
                                db.SaveChanges();

                                return RedirectToAction("Index", new { vendorId = vendordocument.VendorId });
                            }
                        }
                    }
                }
                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.VendorDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                return View(vendordocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /VendorDocument/Create
        public ActionResult Create()
        {
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name");
            return View();
        }

        // POST: /VendorDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="VendorDocumentId,VendorId,DocumentTypeId,FilePath,Description,FileName,ContentType,UploadedBy,UploadedTimestamp")] VendorDocument vendordocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.VendorDocuments.Add(vendordocument);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", vendordocument.DocumentTypeId);
                ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", vendordocument.VendorId);
                return View(vendordocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /VendorDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            VendorDocument vendordocument = db.VendorDocuments.Find(id);
            if (vendordocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", vendordocument.DocumentTypeId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", vendordocument.VendorId);
            return View(vendordocument);
        }

        // POST: /VendorDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="VendorDocumentId,VendorId,DocumentTypeId,FilePath,Description,FileName,ContentType,UploadedBy,UploadedTimestamp")] VendorDocument vendordocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vendordocument).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", vendordocument.DocumentTypeId);
                ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", vendordocument.VendorId);
                return View(vendordocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /VendorDocument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            VendorDocument vendordocument = db.VendorDocuments.Find(id);
            if (vendordocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(vendordocument);
        }

        // POST: /VendorDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                VendorDocument vendordocument = db.VendorDocuments.Find(id);
                string path = Request.MapPath(vendordocument.FilePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                db.VendorDocuments.Remove(vendordocument);
                db.SaveChanges();
                return RedirectToAction("Index", new { vendorId = vendordocument.VendorId });
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

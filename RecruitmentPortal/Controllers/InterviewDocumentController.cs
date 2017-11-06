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
    public class InterviewDocumentController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /InterviewDocument/
        public ActionResult Index(int interviewId)
        {
            try
            {
                ViewBag.InterviewId = interviewId;
                var interviewdocuments = db.InterviewDocuments.Include(i => i.DocumentType).Include(i => i.Interview).Where(d => d.InterviewId == interviewId);
                var interview = db.Interviews.Find(interviewId);
                ViewBag.DocumentTitle = interview.Resource.AspNetUser.FirstName + " " + interview.Resource.AspNetUser.LastName + " - Interview documents for " + interview.StartTime.ToString("MM/dd/yyyy");
                return View(interviewdocuments.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            InterviewDocument interviewdocument = db.InterviewDocuments.Find(id);
            if (interviewdocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(interviewdocument);
        }

        // GET: /ResourceDocument/Create
        public ActionResult Upload(int interviewId)
        {
            try
            {
                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.InterviewDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                var interviewDoc = new InterviewDocument();
                interviewDoc.InterviewId = interviewId;
                return View(interviewDoc);
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
        public ActionResult Upload([Bind(Include = "InterviewDocumentId,InterviewId,DocumentTypeId,Description")] InterviewDocument interviewdocument)
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
                                    ViewBag.DocumentTypes = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", interviewdocument.DocumentTypeId);
                                    return View(interviewdocument);
                                }
                                string path = interviewdocument.DocumentPath + interviewdocument.InterviewId.ToString() + "_" + interviewdocument.DocumentTypeId.ToString() + "_"
                                    + DateTime.Now.ToString("MMddyyyyhhmmss") + "_" + Path.GetFileName(file.FileName);
                                string savedFileName = Path.Combine(Server.MapPath("~" + path));

                                file.SaveAs(savedFileName);
                                interviewdocument.ContentType = file.ContentType;
                                interviewdocument.FilePath = path;
                                interviewdocument.FileName = file.FileName;
                                interviewdocument.UploadedBy = User.Identity.GetUserId();
                                interviewdocument.UploadedTimeStamp = DateTime.Now;
                                db.InterviewDocuments.Add(interviewdocument);
                                db.SaveChanges();

                                return RedirectToAction("Index", new { interviewId = interviewdocument.InterviewId });
                            }
                        }
                    }
                }

                ViewBag.DocumentTypes = new SelectList(db.DocumentTypes.Where(d => d.DocumentGroup == (int)DocumentGroup.InterviewDocument).OrderBy(d => d.Name), "DocumentTypeId", "Name");
                return View(interviewdocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDocument/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name");
                ViewBag.InterviewId = new SelectList(db.Interviews, "InterviewId", "PhoneNo");
                return View();
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /InterviewDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InterviewDocumentId,InterviewId,DocumentTypeId,FilePath,Description,FileName,ContentType,UploadedBy,UploadedTimeStamp")] InterviewDocument interviewdocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.InterviewDocuments.Add(interviewdocument);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", interviewdocument.DocumentTypeId);
                ViewBag.InterviewId = new SelectList(db.Interviews, "InterviewId", "PhoneNo", interviewdocument.InterviewId);
                return View(interviewdocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            InterviewDocument interviewdocument = db.InterviewDocuments.Find(id);
            if (interviewdocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", interviewdocument.DocumentTypeId);
            ViewBag.InterviewId = new SelectList(db.Interviews, "InterviewId", "PhoneNo", interviewdocument.InterviewId);
            return View(interviewdocument);
        }

        // POST: /InterviewDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InterviewDocumentId,InterviewId,DocumentTypeId,FilePath,Description,FileName,ContentType,UploadedBy,UploadedTimeStamp")] InterviewDocument interviewdocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(interviewdocument).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "DocumentTypeId", "Name", interviewdocument.DocumentTypeId);
                ViewBag.InterviewId = new SelectList(db.Interviews, "InterviewId", "PhoneNo", interviewdocument.InterviewId);
                return View(interviewdocument);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDocument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            InterviewDocument interviewdocument = db.InterviewDocuments.Find(id);
            if (interviewdocument == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(interviewdocument);
        }

        // POST: /InterviewDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                InterviewDocument interviewdocument = db.InterviewDocuments.Find(id);
                string path = Request.MapPath(interviewdocument.FilePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                db.InterviewDocuments.Remove(interviewdocument);
                db.SaveChanges();
                return RedirectToAction("Index", new { interviewId = interviewdocument.InterviewId });
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

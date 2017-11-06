using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecruitmentPortal.Models;

namespace RecruitmentPortal.Controllers
{
    public class DocumentTypeController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /DocumentType/
        public ActionResult Index()
        {
            return View(db.DocumentTypes.ToList());
        }

        // GET: /DocumentType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        // GET: /DocumentType/Create
        public ActionResult Create()
        {
            ViewBag.DocumentGroups = new Utility().GetDocumentGroups();
            return View();
        }

        // POST: /DocumentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DocumentTypeId,Name,DocumentGroup")] DocumentType documenttype)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypes.Add(documenttype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentGroups = new Utility().GetDocumentGroups();
            return View(documenttype);
        }

        // GET: /DocumentType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentGroups = new Utility().GetDocumentGroups();
            return View(documenttype);
        }

        // POST: /DocumentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DocumentTypeId,Name,DocumentGroup")] DocumentType documenttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documenttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentGroups = new Utility().GetDocumentGroups();
            return View(documenttype);
        }

        // GET: /DocumentType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        // POST: /DocumentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentType documenttype = db.DocumentTypes.Find(id);
            db.DocumentTypes.Remove(documenttype);
            db.SaveChanges();
            return RedirectToAction("Index");
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

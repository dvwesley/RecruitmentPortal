using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentPortal.Models;

namespace RecruitmentPortal.Controllers
{
    public class ReferrerController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Referrer/
        public ActionResult Index()
        {
            return View(db.Referrers.ToList());
        }

        // GET: /Referrer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Referrer referrer = db.Referrers.Find(id);
            if (referrer == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(referrer);
        }

        // GET: /Referrer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Referrer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ReferrerId,FirstName,LastName,MiddleInitial,Email,PhoneNo")] Referrer referrer)
        {
            if (ModelState.IsValid)
            {
                referrer.CreatedBy = User.Identity.GetUserId();
                referrer.CreatedTimestamp = DateTime.Now;
                db.Referrers.Add(referrer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(referrer);
        }

        // GET: /Referrer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Referrer referrer = db.Referrers.Find(id);
            if (referrer == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(referrer);
        }

        // POST: /Referrer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ReferrerId,FirstName,LastName,MiddleInitial,Email,PhoneNo")] Referrer referrer)
        {
            if (ModelState.IsValid)
            {
                referrer.CreatedBy = User.Identity.GetUserId();
                referrer.CreatedTimestamp = DateTime.Now;
                referrer.UpdatedBy = User.Identity.GetUserId();
                referrer.UpdatedTimestamp = DateTime.Now;
                db.Entry(referrer).State = EntityState.Modified;
                db.Entry(referrer).Property(r => r.CreatedBy).IsModified = false;
                db.Entry(referrer).Property(r => r.CreatedTimestamp).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referrer);
        }

        // GET: /Referrer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Referrer referrer = db.Referrers.Find(id);
            if (referrer == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(referrer);
        }

        // POST: /Referrer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referrer referrer = db.Referrers.Find(id);
            db.Referrers.Remove(referrer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "ReferrerId,FirstName,LastName,Email,PhoneNo")] Referrer referrer)
        {
            if (ModelState.IsValid)
            {
                referrer.CreatedBy = User.Identity.GetUserId();
                referrer.CreatedTimestamp = DateTime.Now;
                db.Referrers.Add(referrer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referrer);
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

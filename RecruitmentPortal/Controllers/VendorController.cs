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
using RecruitmentPortal.Common;

namespace RecruitmentPortal.Controllers
{
    public class VendorController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Vendor/
        public ActionResult Index()
        {
            return View(db.Vendors.ToList());
        }

        // GET: /Vendor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(vendor);
        }

        // GET: /Vendor/Create
        public ActionResult Create()
        {
            ViewBag.States = new Utility().States;
            return View();
        }

        // POST: /Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="VendorId,Name,Address1,Address2,City,State,ZipCode,PhoneNo,BillingContact,Terms,W9,InsuranceCertificates,Contracts,BlackListed")] Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vendor.CreatedBy = User.Identity.GetUserId();
                    vendor.CreatedTimestamp = DateTime.Now;
                    db.Vendors.Add(vendor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.States = new Utility().States;
                return View(vendor);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Vendor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.States = new Utility().States;
            return View(vendor);
        }

        // POST: /Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="VendorId,Name,Address1,Address2,City,State,ZipCode,PhoneNo,BillingContact,Terms,W9,InsuranceCertificates,Contracts,BlackListed")] Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vendor.CreatedBy = User.Identity.GetUserId();
                    vendor.UpdatedBy = User.Identity.GetUserId();
                    vendor.UpdatedTimestamp = DateTime.Now;
                    db.Entry(vendor).State = EntityState.Modified;
                    db.Entry(vendor).Property(v => v.CreatedBy).IsModified = false;
                    db.Entry(vendor).Property(v => v.CreatedTimestamp).IsModified = false;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.States = new Utility().States;
                return View(vendor);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Vendor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(vendor);
        }

        // POST: /Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Vendor vendor = db.Vendors.Find(id);
                db.Vendors.Remove(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        public ActionResult New()
        {
            ViewBag.States = new Utility().States;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "VendorId,Name,City,State,PhoneNo")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                vendor.CreatedBy = User.Identity.GetUserId();
                vendor.CreatedTimestamp = DateTime.Now;
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.States = new Utility().States;
            return View(vendor);
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

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
    public class CommissionTypeController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /CommissionType/
        public ActionResult Index()
        {
            return View(db.CommissionTypes.ToList());
        }

        // GET: /CommissionType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommissionType commissiontype = db.CommissionTypes.Find(id);
            if (commissiontype == null)
            {
                return HttpNotFound();
            }
            return View(commissiontype);
        }

        // GET: /CommissionType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CommissionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CommissionTypeId,Name,Value,IsPercentage")] CommissionType commissiontype)
        {
            if (ModelState.IsValid)
            {
                db.CommissionTypes.Add(commissiontype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commissiontype);
        }

        // GET: /CommissionType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommissionType commissiontype = db.CommissionTypes.Find(id);
            if (commissiontype == null)
            {
                return HttpNotFound();
            }
            return View(commissiontype);
        }

        // POST: /CommissionType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CommissionTypeId,Name,Value,IsPercentage")] CommissionType commissiontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commissiontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commissiontype);
        }

        // GET: /CommissionType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommissionType commissiontype = db.CommissionTypes.Find(id);
            if (commissiontype == null)
            {
                return HttpNotFound();
            }
            return View(commissiontype);
        }

        // POST: /CommissionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommissionType commissiontype = db.CommissionTypes.Find(id);
            db.CommissionTypes.Remove(commissiontype);
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

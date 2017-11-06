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
    public class ClientController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Client/
        public ActionResult Index()
        {
            try
            {
                return View(db.Clients.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(client);
        }

        // GET: /Client/Create
        public ActionResult Create()
        {
            ViewBag.States = new Utility().States;
            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClientId,Name,Account,PhoneNo,Email,BillingContact,Address1,Address2,City,State,ZipCode,BlackListed")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    client.CreatedBy = User.Identity.GetUserId();
                    client.CreatedTimestamp = DateTime.Now;
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.States = new Utility().States;
                return View(client);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.States = new Utility().States;
            return View(client);
        }

        // POST: /Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClientId,Name,Account,PhoneNo,Email,BillingContact,Address1,Address2,City,State,ZipCode,BlackListed")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    client.CreatedBy = User.Identity.GetUserId();                    
                    client.UpdatedBy = User.Identity.GetUserId();
                    client.UpdatedTimestamp = DateTime.Now;
                    db.Entry(client).State = EntityState.Modified;
                    db.Entry(client).Property(c => c.CreatedBy).IsModified = false;
                    db.Entry(client).Property(c => c.CreatedTimestamp).IsModified = false;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.States = new Utility().States;
                return View(client);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(client);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Client client = db.Clients.Find(id);
                db.Clients.Remove(client);
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

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New([Bind(Include = "ClientId,Name,PhoneNo,Email,City,State")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    client.CreatedBy = User.Identity.GetUserId();
                    client.CreatedTimestamp = DateTime.Now;
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.States = new Utility().States;
                return View(client);
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

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
    public class RecruiterController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Recruiter/
        public ActionResult Index()
        {
            try
            {
                var recruiters = db.Recruiters.Include(i => i.AspNetUser).Select(x => new RecruiterViewModel
                {
                    RecruiterId = x.RecruiterId,
                    FirstName = x.AspNetUser.FirstName,
                    LastName = x.AspNetUser.LastName,
                    Email = x.AspNetUser.UserName,
                    Specialty = x.Specialty,
                    Communities = x.Communities
                });
                return View(recruiters.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Recruiter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(recruiter);
        }

        // GET: /Recruiter/Create
        public ActionResult Create()
        {
            try
            {
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.States = new Utility().States;
                return View();
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Recruiter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecruiterId,FirstName,LastName,MiddleInitial,Address1,Address2,City,State,ZipCode,PhoneNo,Email,Specialty,AreasOfImprovment,CareerPath,Communities,Salary,ManagerId,TargetYear,TargetType,TargetDate,TargetCount,CommissionType")] RecruiterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleInitial = model.MiddleInitial,
                        Address1 = model.Address1,
                        Address2 = model.Address2,
                        City = model.City,
                        State = model.State,
                        Zipcode = model.Zipcode,
                        PhoneNo = model.PhoneNo,
                        CreatedTimestamp = DateTime.Now,
                        Active = true
                    };

                    TempData["User"] = user;
                    TempData["recruitermodel"] = model;
                    return RedirectToAction("CreateOrUpdate", "Account", new { returnAction = "Create1", returnController = "Recruiter", userId = true });
                }
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.States = new Utility().States;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        public ActionResult Create1(string userId)
        {
            try
            {
                var model = TempData["recruitermodel"] as RecruiterViewModel;
                var recruiter = new Recruiter
                {
                    Specialty = model.Specialty,
                    AreasOfImprovment = model.AreasOfImprovment,
                    CareerPath = model.CareerPath,
                    Communities = model.Communities,
                    Salary = model.Salary,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedTimestamp = DateTime.Now,
                    UserId = userId,
                    ManagerId = model.ManagerId
                };
                db.Recruiters.Add(recruiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        private void SetTarget(Recruiter recruiter, RecruiterViewModel model)
        {            
        }

        // GET: /Recruiter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                RecruiterViewModel model = new RecruiterViewModel()
                {
                    RecruiterId = recruiter.RecruiterId,
                    FirstName = recruiter.AspNetUser.FirstName,
                    MiddleInitial = recruiter.AspNetUser.MiddleInitial,
                    LastName = recruiter.AspNetUser.LastName,
                    Address1 = recruiter.AspNetUser.Address1,
                    Address2 = recruiter.AspNetUser.Address2,
                    City = recruiter.AspNetUser.City,
                    State = recruiter.AspNetUser.State,
                    Zipcode = recruiter.AspNetUser.Zipcode,
                    PhoneNo = recruiter.AspNetUser.PhoneNo,
                    Email = recruiter.AspNetUser.UserName,
                    Specialty = recruiter.Specialty,
                    AreasOfImprovment = recruiter.AreasOfImprovment,
                    CareerPath = recruiter.CareerPath,
                    Communities = recruiter.Communities,
                    Salary = recruiter.Salary,
                    ManagerId = recruiter.ManagerId
                };
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.States = new Utility().States;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Recruiter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecruiterId,FirstName,LastName,MiddleInitial,Address1,Address2,City,State,ZipCode,PhoneNo,Email,Specialty,AreasOfImprovment,CareerPath,Communities,Salary,ManagerId")] RecruiterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var recruiter = db.Recruiters.Find(model.RecruiterId);
                    recruiter.Specialty = model.Specialty;
                    recruiter.AreasOfImprovment = model.AreasOfImprovment;
                    recruiter.CareerPath = model.CareerPath;
                    recruiter.Communities = model.Communities;
                    recruiter.Salary = model.Salary;
                    recruiter.ManagerId = model.ManagerId;
                    recruiter.UpdatedBy = User.Identity.GetUserId();
                    recruiter.UpdatedTimestamp = DateTime.Now;
                    db.Entry(recruiter).State = EntityState.Modified;
                    db.SaveChanges();

                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleInitial = model.MiddleInitial,
                        Address1 = model.Address1,
                        Address2 = model.Address2,
                        City = model.City,
                        State = model.State,
                        Zipcode = model.Zipcode,
                        PhoneNo = model.PhoneNo
                    };

                    TempData["User"] = user;
                    return RedirectToAction("CreateOrUpdate", "Account", new { returnAction = "Index", returnController = "Recruiter", userId = false });
                }
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.States = new Utility().States;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }        

        // GET: /Recruiter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(recruiter);
        }

        // POST: /Recruiter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Recruiter recruiter = db.Recruiters.Find(id);
                db.Recruiters.Remove(recruiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        [HttpGet]
        public JsonResult GetUserDetails(string email)
        {
            ApplicationUser data = null;
            if (email.Length > 0)
            {
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                data = UserManager.FindByName(email);                
            }
            return Json(data, JsonRequestBehavior.AllowGet);
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

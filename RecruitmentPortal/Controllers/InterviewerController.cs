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
    public class InterviewerController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Interviewer/
        public ActionResult Index()
        {
            try
            {
                var interviewers = db.Interviewers.Include(i => i.Resource).Include(i => i.AspNetUser).Select(x => new InterviewerViewModel
                {
                    InterviewerId = x.InterviewerId,
                    FirstName = x.AspNetUser.FirstName,
                    LastName = x.AspNetUser.LastName,
                    HourlyRate = x.HourlyRate,
                    FixedFee = x.FixedFee,
                    AvailableonSundays = x.AvailableonSundays,
                    AvailableonMondays = x.AvailableonMondays,
                    AvailableonTuesdays = x.AvailableonTuesdays,
                    AvailableonWednesdays = x.AvailableonWednesdays,
                    AvailableonThursdays = x.AvailableonThursdays,
                    AvailableonFridays = x.AvailableonFridays,
                    AvailabaleonSaturdays = x.AvailabaleonSaturdays,
                    PhoneNo = x.AspNetUser.PhoneNo,
                    Email = x.AspNetUser.UserName
                });
                return View(interviewers.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Interviewer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Interviewer interviewer = db.Interviewers.Find(id);
            if (interviewer == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                InterviewerViewModel model = new InterviewerViewModel()
                {
                    InterviewerId = interviewer.InterviewerId,
                    FirstName = interviewer.AspNetUser.FirstName,
                    LastName = interviewer.AspNetUser.LastName,
                    HourlyRate = interviewer.HourlyRate,
                    FixedFee = interviewer.FixedFee,
                    AvailableonSundays = interviewer.AvailableonSundays,
                    AvailableonMondays = interviewer.AvailableonMondays,
                    AvailableonTuesdays = interviewer.AvailableonTuesdays,
                    AvailableonWednesdays = interviewer.AvailableonWednesdays,
                    AvailableonThursdays = interviewer.AvailableonThursdays,
                    AvailableonFridays = interviewer.AvailableonFridays,
                    AvailabaleonSaturdays = interviewer.AvailabaleonSaturdays,
                    PhoneNo = interviewer.AspNetUser.PhoneNo,
                    Email = interviewer.AspNetUser.UserName,
                    Summary = interviewer.Summary,
                    Interviews = db.Interviews.Where(i => i.InterviewerId == interviewer.InterviewerId).ToList(),
                    InterviewerSkillSets = db.InterviewerSkillSets.Where(i => i.InterviewerId == interviewer.InterviewerId).ToList()
                };
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Interviewer/Create
        public ActionResult Create()
        {
            try
            {
                var interviewer = new InterviewerViewModel();
                ViewBag.States = new Utility().States;
                return View(interviewer);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Interviewer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,MiddleInitial,Address1,Address2,City,State,ZipCode,Summary,AvailableonSundays,AvailableonMondays,AvailableonTuesdays,AvailableonWednesdays,AvailableonThursdays,AvailableonFridays,AvailabaleonSaturdays,HourlyRate,FixedFee,PhoneNo,Email,SelectedSkillSetCSV")] InterviewerViewModel model)
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
                    TempData["interviewermodel"] = model;
                    return RedirectToAction("CreateOrUpdate", "Account", new { returnAction = "Create1", returnController = "Interviewer", userId = true });
                }
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
                var model = TempData["interviewermodel"] as InterviewerViewModel;
                var interviewer = new Interviewer
                {
                    Summary = model.Summary,
                    AvailableonSundays = model.AvailableonSundays,
                    AvailableonMondays = model.AvailableonMondays,
                    AvailableonTuesdays = model.AvailableonTuesdays,
                    AvailableonWednesdays = model.AvailableonWednesdays,
                    AvailableonThursdays = model.AvailableonThursdays,
                    AvailableonFridays = model.AvailableonFridays,
                    AvailabaleonSaturdays = model.AvailabaleonSaturdays,
                    HourlyRate = model.HourlyRate,
                    FixedFee = model.FixedFee,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedTimestamp = DateTime.Now,
                    UserId = userId
                };

                AddSkillSet(interviewer, model);
                db.Interviewers.Add(interviewer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        private void AddSkillSet(Interviewer interviewer, InterviewerViewModel model)
        {
            try
            {
                if (model.SelectedSkillSetCSV != null && model.SelectedSkillSetCSV.Length > 0)
                {
                    if (model.InterviewerId != 0)
                    {
                        db.Entry(interviewer).Collection(r => r.InterviewerSkillSets).Load();
                        interviewer.InterviewerSkillSets.Clear();
                    }
                    if (interviewer.InterviewerSkillSets == null)
                    {
                        interviewer.InterviewerSkillSets = new List<InterviewerSkillSet>();
                    }
                    InterviewerSkillSet interviewerSkillset = null;
                    string[] skillsets = model.SelectedSkillSetCSV.Split(",".ToCharArray());
                    foreach (string skillset in skillsets)
                    {
                        if (skillset.Length > 0)
                        {
                            interviewerSkillset = new InterviewerSkillSet()
                            {
                                SkillSetId = int.Parse(skillset),
                                CreatedBy = User.Identity.GetUserId(),
                                CreatedTimestamp = DateTime.Now
                            };
                            interviewer.InterviewerSkillSets.Add(interviewerSkillset);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                //return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Interviewer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Interviewer interviewer = db.Interviewers.Find(id);
            if (interviewer == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                InterviewerViewModel model = new InterviewerViewModel()
                {
                    InterviewerId = interviewer.InterviewerId,
                    FirstName = interviewer.AspNetUser.FirstName,
                    MiddleInitial = interviewer.AspNetUser.MiddleInitial,
                    LastName = interviewer.AspNetUser.LastName,
                    Address1 = interviewer.AspNetUser.Address1,
                    Address2 = interviewer.AspNetUser.Address2,
                    City = interviewer.AspNetUser.City,
                    State = interviewer.AspNetUser.State,
                    Zipcode = interviewer.AspNetUser.Zipcode,
                    PhoneNo = interviewer.AspNetUser.PhoneNo,
                    Email = interviewer.AspNetUser.UserName,
                    HourlyRate = interviewer.HourlyRate,
                    FixedFee = interviewer.FixedFee,
                    AvailableonSundays = interviewer.AvailableonSundays,
                    AvailableonMondays = interviewer.AvailableonMondays,
                    AvailableonTuesdays = interviewer.AvailableonTuesdays,
                    AvailableonWednesdays = interviewer.AvailableonWednesdays,
                    AvailableonThursdays = interviewer.AvailableonThursdays,
                    AvailableonFridays = interviewer.AvailableonFridays,
                    AvailabaleonSaturdays = interviewer.AvailabaleonSaturdays,
                    Summary = interviewer.Summary,
                    InterviewerSkillSets = db.InterviewerSkillSets.Where(i => i.InterviewerId == interviewer.InterviewerId).ToList()
                };
                ViewBag.States = new Utility().States;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Interviewer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterviewerId,FirstName,LastName,MiddleInitial,Address1,Address2,City,State,ZipCode,Summary,AvailableonSundays,AvailableonMondays,AvailableonTuesdays,AvailableonWednesdays,AvailableonThursdays,AvailableonFridays,AvailabaleonSaturdays,HourlyRate,FixedFee,PhoneNo,Email,SelectedSkillSetCSV")] InterviewerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Interviewer interviewer = db.Interviewers.Find(model.InterviewerId);
                    interviewer.FixedFee = model.FixedFee;
                    interviewer.HourlyRate = model.HourlyRate;
                    interviewer.Summary = model.Summary;
                    interviewer.AvailableonSundays = model.AvailableonSundays;
                    interviewer.AvailableonMondays = model.AvailableonMondays;
                    interviewer.AvailableonTuesdays = model.AvailableonTuesdays;
                    interviewer.AvailableonWednesdays = model.AvailableonWednesdays;
                    interviewer.AvailableonThursdays = model.AvailableonThursdays;
                    interviewer.AvailableonFridays = model.AvailableonFridays;
                    interviewer.AvailabaleonSaturdays = model.AvailabaleonSaturdays;
                    interviewer.UpdatedBy = User.Identity.GetUserId();
                    interviewer.UpdatedTimestamp = DateTime.Now;
                    AddSkillSet(interviewer, model);

                    db.Entry(interviewer).State = EntityState.Modified;
                    db.SaveChanges();

                    var user = new ApplicationUser
                    {
                        Id = interviewer.UserId,
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
                    return RedirectToAction("CreateOrUpdate", "Account", new { returnAction = "Index", returnController = "Interviewer", userId = false });
                }
                ViewBag.States = new Utility().States;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        public ActionResult DeleteSkillSet(int id)
        {
            try
            {
                InterviewerSkillSet skillset = db.InterviewerSkillSets.Find(id);
                if (skillset != null)
                {
                    db.InterviewerSkillSets.Remove(skillset);
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = skillset.InterviewerId });
                }
                return View(Response);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Interviewer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Interviewer interviewer = db.Interviewers.Find(id);
            if (interviewer == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                InterviewerViewModel model = new InterviewerViewModel()
                {
                    InterviewerId = interviewer.InterviewerId,
                    FirstName = interviewer.AspNetUser.FirstName,
                    LastName = interviewer.AspNetUser.LastName,
                    HourlyRate = interviewer.HourlyRate,
                    FixedFee = interviewer.FixedFee,
                    AvailableonSundays = interviewer.AvailableonSundays,
                    AvailableonMondays = interviewer.AvailableonMondays,
                    AvailableonTuesdays = interviewer.AvailableonTuesdays,
                    AvailableonWednesdays = interviewer.AvailableonWednesdays,
                    AvailableonThursdays = interviewer.AvailableonThursdays,
                    AvailableonFridays = interviewer.AvailableonFridays,
                    AvailabaleonSaturdays = interviewer.AvailabaleonSaturdays,
                    PhoneNo = interviewer.AspNetUser.PhoneNo,
                    Email = interviewer.AspNetUser.UserName,
                    Summary = interviewer.Summary
                };
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Interviewer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Interviewer interviewer = db.Interviewers.Find(id);
                db.Interviewers.Remove(interviewer);
                db.SaveChanges();
                return RedirectToAction("Index");
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

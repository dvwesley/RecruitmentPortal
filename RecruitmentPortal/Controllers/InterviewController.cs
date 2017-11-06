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
using RecruitmentPortal.Extensions;
using RecruitmentPortal.Common;

namespace RecruitmentPortal.Controllers
{
    public class InterviewController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Interview/
        public ActionResult Index()
        {
            try
            {
                var interviews = db.Interviews.Include(i => i.Client).Include(i => i.Interviewer).Include(i => i.Requirement).Include(i => i.Resource);
                return View(interviews.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Interview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(interview);
        }

        // GET: /Interview/Create
        public ActionResult Create(int? requirementId = null, int? resourceId = null)
        {
            try
            {
                var interviewers = db.Interviewers.Include(i => i.AspNetUser).Select(i => new
                {
                    InterviewerId = i.InterviewerId,
                    InterviewerName = i.AspNetUser.FirstName + " " + i.AspNetUser.LastName
                });
                ViewBag.Interviewers = new SelectList(interviewers, "InterviewerId", "InterviewerName");
                var requirements = db.Requirements.Include(r => r.Client).ToList().Select(r => new
                {
                    RequirementId = r.RequirementId,
                    RequirementName = r.JobNumber + " - " + r.JobTitle + " - " + r.Client.Name
                });
                ViewBag.Requirements = new SelectList(requirements, "RequirementId", "RequirementName");
                var resources = db.Resources.ToList().Select(r => new
                {
                    ResourceId = r.ResourceId,
                    ResourceName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Resources = new SelectList(resources, "ResourceId", "ResourceName");
                ViewBag.Hours = new Utility().Hours;
                ViewBag.Minutes = new Utility().Minutes;
                Interview interview = new Interview();
                interview.Status = "S";
                if (requirementId != null)
                {
                    interview.RequirementId = requirementId;
                }
                if (resourceId != null && resourceId.HasValue)
                {
                    interview.ResourceId = resourceId.Value;
                }
                interview.StartTime = DateTime.Today;
                interview.EndTime = DateTime.Today;
                return View(interview);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }
        
        // POST: /Interview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterviewId,ResourceId,PhoneNo,MeetingLink,InterviewType,Status,InterviewerId,Fee,StartTime,EndTime,RequirementId")] Interview interview)
        {
            try
            {
                interview.Status = "S";
                if (ModelState.IsValid)
                {
                    LoadInterviewDateTime(interview);
                    if (interview.EndTime > interview.StartTime)
                    {
                        interview.CreatedBy = User.Identity.GetUserId();
                        interview.CreatedTimestamp = DateTime.Now;
                        // this has to be checked later.
                        if (interview.RequirementId.HasValue && interview.RequirementId.Value > 0)
                        {
                            interview.ClientId = db.Requirements.Find(interview.RequirementId).Tier1ClientId;
                        }
                        db.Interviews.Add(interview);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("EndTime", "End Date Time cannot be less than Start Date Time.");
                }
                var interviewers = db.Interviewers.Include(i => i.AspNetUser).Select(i => new
                {
                    InterviewerId = i.InterviewerId,
                    InterviewerName = i.AspNetUser.FirstName + " " + i.AspNetUser.LastName
                });
                ViewBag.Interviewers = new SelectList(interviewers, "InterviewerId", "InterviewerName");
                var requirements = db.Requirements.Include(r => r.Client).ToList().Select(r => new
                {
                    RequirementId = r.RequirementId,
                    RequirementName = r.JobNumber + " - " + r.JobTitle + " - " + r.Client.Name
                });
                ViewBag.Requirements = new SelectList(requirements, "RequirementId", "RequirementName");
                var resources1 = db.Resources.ToList().Select(r => new
                {
                    ResourceId = r.ResourceId,
                    ResourceName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Resources = new SelectList(resources1, "ResourceId", "ResourceName");
                ViewBag.Hours = new Utility().Hours;
                ViewBag.Minutes = new Utility().Minutes;
                return View(interview);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        private void LoadInterviewDateTime(Interview interview)
        {
            int hours = 0;
            int minutes = 0;
            if (Request["StHours"] != null && Request["StHours"] != "")
            {
                hours = Int32.Parse(Request["StHours"]);
            }
            if (Request["StMinutes"] != null && Request["StMinutes"] != "")
            {
                minutes = Int32.Parse(Request["StMinutes"]);
            }
            interview.StartTime = new DateTime(interview.StartTime.Year, interview.StartTime.Month, interview.StartTime.Day, hours, minutes, 0);
            hours = 0;
            minutes = 0;
            if (Request["EndHours"] != null && Request["EndHours"] != "")
            {
                hours = Int32.Parse(Request["EndHours"]);
            }
            if (Request["EndMinutes"] != null && Request["EndMinutes"] != "")
            {
                minutes = Int32.Parse(Request["EndMinutes"]);
            }
            interview.EndTime = new DateTime(interview.EndTime.Year, interview.EndTime.Month, interview.EndTime.Day, hours, minutes, 0);
        }

        // GET: /Interview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                var interviewers = db.Interviewers.Include(i => i.AspNetUser).Select(i => new
                {
                    InterviewerId = i.InterviewerId,
                    InterviewerName = i.AspNetUser.FirstName + " " + i.AspNetUser.LastName
                });
                ViewBag.Interviewers = new SelectList(interviewers, "InterviewerId", "InterviewerName");
                var requirements = db.Requirements.Include(r => r.Client).ToList().Select(r => new
                {
                    RequirementId = r.RequirementId,
                    RequirementName = r.JobNumber + " - " + r.JobTitle + " - " + r.Client.Name
                });
                ViewBag.Requirements = new SelectList(requirements, "RequirementId", "RequirementName");
                var resources = db.Resources.ToList().Select(r => new
                {
                    ResourceId = r.ResourceId,
                    ResourceName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Resources = new SelectList(resources, "ResourceId", "ResourceName");
                ViewBag.InterviewStatus = new Utility().InterviewStatus;
                ViewBag.SHours = new Utility().Hours;
                ViewBag.SMinutes = new Utility().Minutes;
                ViewBag.EHours = new Utility().Hours;
                ViewBag.EMinutes = new Utility().Minutes;
                return View(interview);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Interview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterviewId,ResourceId,PhoneNo,MeetingLink,Feedback,InterviewType,Status,ClientId,InterviewerId,FeePaid,Fee,StartTime,EndTime,RequirementId")] Interview interview)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoadInterviewDateTime(interview);
                    if (interview.EndTime > interview.StartTime)
                    {
                        interview.CreatedBy = User.Identity.GetUserId();
                        interview.UpdatedBy = User.Identity.GetUserId();
                        interview.UpdatedTimestamp = DateTime.Now;

                        db.Entry(interview).State = EntityState.Modified;
                        db.Entry(interview).Property(x => x.CreatedBy).IsModified = false;
                        db.Entry(interview).Property(x => x.CreatedTimestamp).IsModified = false;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("EndTime", "End Date Time cannot be less than Start Date Time.");
                }
                var interviewers = db.Interviewers.Include(i => i.AspNetUser).Select(i => new
                {
                    InterviewerId = i.InterviewerId,
                    InterviewerName = i.AspNetUser.FirstName + " " + i.AspNetUser.LastName
                });
                ViewBag.Interviewers = new SelectList(interviewers, "InterviewerId", "InterviewerName");
                var requirements = db.Requirements.Include(r => r.Client).ToList().Select(r => new
                {
                    RequirementId = r.RequirementId,
                    RequirementName = r.JobNumber + " - " + r.JobTitle + " - " + r.Client.Name
                });
                ViewBag.Requirements = new SelectList(requirements, "RequirementId", "RequirementName");
                var resources = db.Resources.ToList().Select(r => new
                {
                    ResourceId = r.ResourceId,
                    ResourceName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Resources = new SelectList(resources, "ResourceId", "ResourceName");
                ViewBag.InterviewStatus = new Utility().InterviewStatus;
                ViewBag.SHours = new Utility().Hours;
                ViewBag.SMinutes = new Utility().Minutes;
                ViewBag.EHours = new Utility().Hours;
                ViewBag.EMinutes = new Utility().Minutes;
                return View(interview);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Interview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(interview);
        }

        // POST: /Interview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Interview interview = db.Interviews.Find(id);
                db.Interviews.Remove(interview);
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

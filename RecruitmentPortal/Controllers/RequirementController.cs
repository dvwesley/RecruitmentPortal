using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentPortal.Models;
using RecruitmentPortal.Common;

namespace RecruitmentPortal.Controllers
{
    public class RequirementController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Requirement/
        public ActionResult Index()
        {
            try
            {
                var requirements = db.Requirements.Include(r => r.Client).Include(r => r.Client1);
                return View(requirements.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Requirement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Requirement requirement = db.Requirements.Find(id);
            if (requirement == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(requirement);
        }

        // GET: /Requirement/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Tier1Clients = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.Tier2Clients = new SelectList(db.Clients, "ClientId", "Name");
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.AccountManagers = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.Priorities = new Utility().Priority;
                ViewBag.RequirementStatus = new Utility().RequirementStatus;
                ViewBag.JobType = new Utility().JobType;
                return View();
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Requirement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string SelectedSkillSetCSV, string MandatorySkillSetCSV, [Bind(Include = "RequirementId,JobNumber,JobTitle,Priority,Description,Location,Duration,HourlyBuyRate,HourlyBillingRate,JobTypeId,OneTimeFee,Status,RecruiterId,Tier1ClientId,Tier2ClientId,AccountManagerId,NoofPositions")] Requirement requirement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (requirement.NoofPositions == 1)
                    {
                        requirement.CreatedBy = User.Identity.GetUserId();
                        requirement.CreatedTimestamp = DateTime.Now;
                        AddSkillSet(SelectedSkillSetCSV, MandatorySkillSetCSV, requirement);
                        db.Requirements.Add(requirement);
                        db.SaveChanges();
                    }
                    else
                    {
                        for (int i = 1; i <= requirement.NoofPositions; i++)
                        {
                            if (requirement.RequirementId != 0)
                            {
                                var duprequirement = requirement.Copy();
                                duprequirement.CreatedBy = User.Identity.GetUserId();
                                duprequirement.CreatedTimestamp = DateTime.Now;
                                AddSkillSet(SelectedSkillSetCSV, MandatorySkillSetCSV, duprequirement);
                                db.Requirements.Add(duprequirement);
                            }
                            else
                            {
                                requirement.CreatedBy = User.Identity.GetUserId();
                                requirement.CreatedTimestamp = DateTime.Now;
                                AddSkillSet(SelectedSkillSetCSV, MandatorySkillSetCSV, requirement);
                                db.Requirements.Add(requirement);
                            }
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");

                }

                ViewBag.Tier1Clients = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.Tier2Clients = new SelectList(db.Clients, "ClientId", "Name");
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.AccountManagers = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.Priorities = new Utility().Priority;
                ViewBag.RequirementStatus = new Utility().RequirementStatus;
                ViewBag.JobType = new Utility().JobType;
                return View(requirement);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                Logger.LogMessage(exceptionMessage);
                return RedirectToAction("AppError", "Error");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        private void AddSkillSet(string SelectedSkillSetCSV, string MandatorySkillSetCSV, Requirement requirement)
        {
            if (SelectedSkillSetCSV.Length > 0)
            {
                if (requirement.RequirementId != 0)
                {
                    db.Entry(requirement).Collection(r => r.RequirementSkillSets).Load();
                    foreach(RequirementSkillSet skillset in requirement.RequirementSkillSets)
                    {
                        skillset.IsMandatory = MandatorySkillSetCSV.IndexOf("," + skillset.SkillSetId + ",") >= 0 ? true : false;
                        skillset.UpdatedBy = User.Identity.GetUserId();
                        skillset.UpdatedTimestamp = DateTime.Now;
                    }
                }
                if (requirement.RequirementSkillSets == null)
                {
                    requirement.RequirementSkillSets = new List<RequirementSkillSet>();
                }
                RequirementSkillSet reqskillset = null;
                string[] skillsets = SelectedSkillSetCSV.Split(",".ToCharArray());
                foreach (string skillset in skillsets)
                {
                    if (skillset.Length > 0)
                    {
                        reqskillset = new RequirementSkillSet()
                        {
                            SkillSetId = int.Parse(skillset),
                            IsMandatory = MandatorySkillSetCSV.IndexOf("," + skillset + ",") >= 0 ? true : false,
                            CreatedBy = User.Identity.GetUserId(),
                            CreatedTimestamp = DateTime.Now
                        };
                        requirement.RequirementSkillSets.Add(reqskillset);
                    }
                }
            }
        }

        public ActionResult DeleteSkillSet(int id)
        {
            try
            {
                RequirementSkillSet skillset = db.RequirementSkillSets.Find(id);
                if (skillset != null)
                {
                    db.RequirementSkillSets.Remove(skillset);
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = skillset.RequirementId });
                }
                return View(Response);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }


        // GET: /Requirement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Requirement requirement = db.Requirements.Find(id);
            requirement.NoofPositions = 1;
            if (requirement == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                ViewBag.Tier1Clients = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.Tier2Clients = new SelectList(db.Clients, "ClientId", "Name");
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.AccountManagers = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.Priorities = new Utility().Priority;
                ViewBag.RequirementStatus = new Utility().RequirementStatus;
                ViewBag.JobType = new Utility().JobType;
                return View(requirement);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Requirement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string SelectedSkillSetCSV, string MandatorySkillSetCSV, [Bind(Include = "RequirementId,JobNumber,JobTitle,Priority,Description,Location,Duration,HourlyBuyRate,HourlyBillingRate,JobTypeId,OneTimeFee,Status,ReasonNotClosed,RecruiterId,Tier1ClientId,Tier2ClientId,AccountManagerId,NoofPositions,CreatedBy")] Requirement requirement)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(requirement).State = EntityState.Modified;
                    db.Entry(requirement).Property(x => x.CreatedBy).IsModified = false;
                    db.Entry(requirement).Property(x => x.CreatedTimestamp).IsModified = false;
                    requirement.UpdatedBy = User.Identity.GetUserId();
                    requirement.UpdatedTimestamp = DateTime.Now;
                    AddSkillSet(SelectedSkillSetCSV, MandatorySkillSetCSV, requirement);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                ViewBag.Tier1Clients = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.Tier2Clients = new SelectList(db.Clients, "ClientId", "Name");
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.AccountManagers = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                ViewBag.Priorities = new Utility().Priority;
                ViewBag.RequirementStatus = new Utility().RequirementStatus;
                ViewBag.JobType = new Utility().JobType;
                return View(requirement);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                Logger.LogMessage(exceptionMessage);
                return RedirectToAction("AppError", "Error");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Requirement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Requirement requirement = db.Requirements.Find(id);
            if (requirement == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(requirement);
        }

        // POST: /Requirement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Requirement requirement = db.Requirements.Find(id);
                db.Requirements.Remove(requirement);
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

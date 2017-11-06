using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentPortal.Models;
using RecruitmentPortal.Common;

namespace RecruitmentPortal.Controllers
{
    public class RequirementDetailController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /RequirementDetail/
        public ActionResult Index(int requirementId)
        {
            try
            {
                ViewBag.RequirementId = requirementId;
                var requirementdetails = db.RequirementDetails.Include(r => r.Requirement).Include(r => r.Resource).Where(r => r.RequirementId == requirementId);
                var requirement = db.Requirements.Find(requirementId);
                ViewBag.DocumentTitle = "Resource list for the requirement - " + requirement.JobNumber;
                return View(requirementdetails.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RequirementDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RequirementDetail requirementdetail = db.RequirementDetails.Find(id);
            if (requirementdetail == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(requirementdetail);
        }

        // GET: /RequirementDetail/Create
        public ActionResult Create(int requirementId)
        {
            try
            {
                var model = RequirementDetailViewModel.Initialize(requirementId);                
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /RequirementDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ResourceIds, [Bind(Include = "RequirementDetailId,RequirementId,RecruiterId,ResourceId,ReferredToClient,CandidateSelected,Notes")] RequirementDetail requirementdetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ResourceIds.Length > 0)
                    {
                        string[] resources = ResourceIds.Split(",".ToCharArray());
                        RequirementDetail detail = null;
                        foreach (string resource in resources)
                        {
                            if (resource.Length > 0)
                            {
                                detail = new RequirementDetail();
                                detail.RequirementId = requirementdetail.RequirementId;
                                detail.RecruiterId = requirementdetail.RecruiterId;
                                detail.ResourceId = Int32.Parse(resource);
                                detail.CandidateSelected = false;
                                detail.Notes = requirementdetail.Notes;
                                detail.CreatedBy = User.Identity.GetUserId();
                                detail.CreatedTimestamp = DateTime.Now;
                                db.RequirementDetails.Add(detail);
                                db.SaveChanges();
                            }
                        }
                    }
                    return RedirectToAction("Index", new { requirementId = requirementdetail.RequirementId });
                }
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                return View(requirementdetail);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RequirementDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RequirementDetail requirementdetail = db.RequirementDetails.Find(id);
            if (requirementdetail == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                return View(requirementdetail);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /RequirementDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RequirementDetailId,RequirementId,RecruiterId,ResourceId,ReferredToClient,CandidateSelected,Notes")] RequirementDetail requirementdetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    requirementdetail.CreatedBy = User.Identity.GetUserId();
                    requirementdetail.CreatedTimestamp = DateTime.Now;
                    requirementdetail.UpdatedBy = User.Identity.GetUserId();
                    requirementdetail.UpdatedTimestamp = DateTime.Now;
                    db.Entry(requirementdetail).State = EntityState.Modified;
                    db.Entry(requirementdetail).Property(r => r.CreatedBy).IsModified = false;
                    db.Entry(requirementdetail).Property(r => r.CreatedTimestamp).IsModified = false;

                    db.SaveChanges();

                    if (requirementdetail.CandidateSelected)
                    {
                        return RedirectToAction("OnBoard", new { id = requirementdetail.RequirementDetailId });
                    }
                    return RedirectToAction("Index", new { requirementId = requirementdetail.RequirementId });
                }
                var recruiters = db.Recruiters.Include(u => u.AspNetUser).ToList().Select(r => new
                {
                    RecruiterId = r.RecruiterId,
                    RecruiterName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                ViewBag.Recruiters = new SelectList(recruiters, "RecruiterId", "RecruiterName");
                return View(requirementdetail);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RequirementDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RequirementDetail requirementdetail = db.RequirementDetails.Find(id);
            if (requirementdetail == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(requirementdetail);
        }

        // POST: /RequirementDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                RequirementDetail requirementdetail = db.RequirementDetails.Find(id);
                db.RequirementDetails.Remove(requirementdetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { requirementId = requirementdetail.RequirementId });
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        public ActionResult OnBoard(int id)
        {
            try
            {
                var detail = db.RequirementDetails.Include(d => d.Resource).Include(d => d.Requirement).Where(d => d.RequirementDetailId == id).FirstOrDefault();                
                var model = new ResourceOnBoardModel
                {
                    RequirementDetailId = detail.RequirementDetailId,
                    RequirementId = detail.RequirementId,
                    ResourceId = detail.ResourceId.Value,
                    RecruiterId = detail.RecruiterId,
                    BuyRate = detail.Requirement.HourlyBuyRate,
                    BillingRate = detail.Requirement.HourlyBillingRate,
                    FinalRate = detail.Requirement.HourlyBuyRate
                };
                var projectDetail = db.ProjectDetails.Where(p => p.RequirementId == model.RequirementId && p.ResourceId == model.ResourceId).FirstOrDefault();
                if(projectDetail != null)
                {
                    model.ProjectStartDate = projectDetail.StartDate;
                    model.ProjectEndDate = projectDetail.EndDate;
                    model.PlacementDate = projectDetail.PlacementDate;
                }
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnBoard([Bind(Include = "RequirementDetailId,ResourceId,RequirementId,RecruiterId,ProjectStartDate,ProjectEndDate,PlacementDate,FinalRate")] ResourceOnBoardModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var projectDetail = db.ProjectDetails.Where(p => p.RequirementId == model.RequirementId && p.ResourceId == model.ResourceId).FirstOrDefault();
                    var requirement = db.Requirements.Find(model.RequirementId);                    
                    // update project details.
                    if (projectDetail != null)
                    {
                        projectDetail.StartDate = model.ProjectStartDate.Value;
                        projectDetail.EndDate = model.ProjectEndDate;
                        projectDetail.PlacementDate = model.PlacementDate.Value;
                        db.Entry(projectDetail).State = EntityState.Modified;                        
                    }
                    else
                    {
                        projectDetail = new ProjectDetail
                        {
                            RequirementId = model.RequirementId,
                            ResourceId = model.ResourceId,
                            RecruiterId = model.RecruiterId,
                            StartDate = model.ProjectStartDate.Value,
                            EndDate = model.ProjectEndDate,
                            PlacementDate = model.PlacementDate.Value,                            
                            AccountManagerId = requirement.AccountManagerId
                        };                        
                        db.ProjectDetails.Add(projectDetail);                        
                    }
                    db.SaveChanges();

                    //update final buy rate in requirement                    
                    requirement.RequirementId = model.RequirementId;
                    requirement.HourlyBuyRate = model.FinalRate;
                    requirement.UpdatedBy = User.Identity.GetUserId();
                    requirement.UpdatedTimestamp = DateTime.Now;
                    db.Requirements.Attach(requirement);
                    db.Entry(requirement).Property(r => r.HourlyBuyRate).IsModified = true;
                    db.Entry(requirement).Property(r => r.UpdatedBy).IsModified = true;
                    db.Entry(requirement).Property(r => r.UpdatedTimestamp).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    //update project end date and client for the resource.
                    var resource = new Resource();
                    resource.ResourceId = model.ResourceId;
                    resource.ClientId = requirement.Tier1ClientId;
                    resource.ProjectEndDate = model.ProjectEndDate;
                    resource.UpdatedBy = User.Identity.GetUserId();
                    resource.UpdatedTime = DateTime.Now;
                    db.Resources.Attach(resource);
                    db.Entry(resource).Property(r => r.ProjectEndDate).IsModified = true;
                    db.Entry(resource).Property(r => r.UpdatedBy).IsModified = true;
                    db.Entry(resource).Property(r => r.UpdatedTime).IsModified = true;
                    db.Entry(resource).Property(r => r.ClientId).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    return RedirectToAction("Index", "Requirement", new { requirementId = model.RequirementId });
                }
                return View(model);
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

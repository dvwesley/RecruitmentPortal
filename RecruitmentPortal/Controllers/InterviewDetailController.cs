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
    public class InterviewDetailController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /InterviewDetail/
        public ActionResult Index(int interviewId)
        {
            try
            {
                var interviewdetails = db.InterviewDetails.Where(d => d.InterviewId == interviewId);
                if (interviewdetails == null || interviewdetails != null && interviewdetails.Count() == 0)
                {
                    //populate skill for the resource.
                    int resourceId = db.Interviews.Find(interviewId).ResourceId;
                    var skillsetDetails = db.ResourceSkillSets.Where(s => s.ResourceId == resourceId).Select(s => new
                    {

                        SkillSetId = s.SkillSetId,
                        InterviewId = interviewId
                    }).AsEnumerable().Select(x => new InterviewDetail
                    {
                        SkillSetId = x.SkillSetId,
                        InterviewId = x.InterviewId,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedTimestamp = DateTime.Now
                    }).ToList();

                    if (skillsetDetails.Count() > 0)
                    {
                        db.InterviewDetails.AddRange(skillsetDetails);
                        db.SaveChanges();
                    }
                }
                ViewBag.Ratings = Utility.GetRatings();
                ViewBag.InterviewId = interviewId;
                return View(db.InterviewDetails.Include(i => i.SkillSet).Where(i => i.InterviewId == interviewId).OrderBy(i => i.InterviewDetailId).ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            try
            {
                int interviewId = int.Parse(Request["InterviewId"]);
                var interviewDetails = db.InterviewDetails.Where(i => i.InterviewId == interviewId).OrderBy(i => i.InterviewDetailId);

                if (Request["item.Rating"] != null && Request["item.Comments"] != null)
                {
                    string[] ratings = Request["item.Rating"].Split(",".ToCharArray());
                    string[] comments = Request["item.Comments"].Split(",".ToCharArray());
                    int i = 0;
                    foreach (InterviewDetail detail in interviewDetails)
                    {
                        detail.Rating = int.Parse(ratings[i]);
                        detail.Comments = comments[i];
                        detail.UpdatedBy = User.Identity.GetUserId();
                        detail.UpdatedTimestamp = DateTime.Now;
                        i++;
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index", new { interviewId = interviewId });
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            InterviewDetail interviewdetail = db.InterviewDetails.Find(id);
            if (interviewdetail == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(interviewdetail);
        }

        // GET: /InterviewDetail/Create
        public ActionResult Create(int interviewId)
        {
            try
            {
                ViewBag.Ratings = Utility.GetRatings();
                ViewBag.SkillSets = new SelectList(db.SkillSets.OrderBy(s => s.Name), "SkillSetId", "Name");
                var detail = new InterviewDetail();
                detail.InterviewId = interviewId;
                return View(detail);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /InterviewDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InterviewDetailId,InterviewId,SkillSetId,Rating,Comments")] InterviewDetail interviewdetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interviewdetail.CreatedBy = User.Identity.GetUserId();
                    interviewdetail.CreatedTimestamp = DateTime.Now;
                    db.InterviewDetails.Add(interviewdetail);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { interviewId = interviewdetail.InterviewId });
                }

                ViewBag.Ratings = Utility.GetRatings();
                ViewBag.SkillSets = new SelectList(db.SkillSets.OrderBy(s => s.Name), "SkillSetId", "Name");
                return View(interviewdetail);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            InterviewDetail interviewdetail = db.InterviewDetails.Find(id);
            if (interviewdetail == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.Ratings = Utility.GetRatings();
            ViewBag.SkillSets = new SelectList(db.SkillSets.OrderBy(s => s.Name), "SkillSetId", "Name");            
            return View(interviewdetail);
        }

        // POST: /InterviewDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InterviewDetailId,InterviewId,SkillSetId,Rating,Comments")] InterviewDetail interviewdetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    interviewdetail.CreatedBy = User.Identity.GetUserId();
                    interviewdetail.UpdatedBy = User.Identity.GetUserId();
                    interviewdetail.UpdatedTimestamp = DateTime.Now;

                    db.Entry(interviewdetail).State = EntityState.Modified;
                    db.Entry(interviewdetail).Property(x => x.CreatedBy).IsModified = false;
                    db.Entry(interviewdetail).Property(x => x.CreatedTimestamp).IsModified = false;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { interviewId = interviewdetail.InterviewId });
                }
                ViewBag.Ratings = Utility.GetRatings();
                ViewBag.SkillSets = new SelectList(db.SkillSets.OrderBy(s => s.Name), "SkillSetId", "Name");
                return View(interviewdetail);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /InterviewDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            InterviewDetail interviewdetail = db.InterviewDetails.Find(id);
            if (interviewdetail == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(interviewdetail);
        }

        // POST: /InterviewDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                InterviewDetail interviewdetail = db.InterviewDetails.Find(id);
                db.InterviewDetails.Remove(interviewdetail);
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

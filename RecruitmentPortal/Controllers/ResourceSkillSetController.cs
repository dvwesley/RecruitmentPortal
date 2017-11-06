using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecruitmentPortal.Models;
using RecruitmentPortal.Common;

namespace RecruitmentPortal.Controllers
{
    public class ResourceSkillSetController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /ResourceSkillSet/
        public ActionResult Index(int resourceId)
        {
            try
            {
                ViewBag.ResourceId = resourceId;
                var resourceskillsets = db.ResourceSkillSets.Include(r => r.Resource).Include(r => r.SkillSet).Where(s => s.ResourceId == resourceId);
                if (resourceskillsets != null && resourceskillsets.Count() > 0)
                {

                    ViewBag.ResourceName = resourceskillsets.FirstOrDefault().Resource.AspNetUser.FirstName + " " + resourceskillsets.FirstOrDefault().Resource.AspNetUser.MiddleInitial + " "
                        + resourceskillsets.FirstOrDefault().Resource.AspNetUser.LastName;
                }
                else
                {
                    var resource = db.Resources.Find(resourceId);
                    ViewBag.ResourceName = resource.AspNetUser.FirstName + " " + resource.AspNetUser.MiddleInitial + " " + resource.AspNetUser.LastName;
                }
                return View(resourceskillsets.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ResourceSkillSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceSkillSet resourceskillset = db.ResourceSkillSets.Find(id);
            if (resourceskillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(resourceskillset);
        }

        // GET: /ResourceSkillSet/Create
        public ActionResult Create(int resourceId)
        {
            try
            {
                ViewBag.SkillSets = new SelectList(db.SkillSets.OrderBy(s => s.Name), "SkillSetId", "Name");
                ViewBag.Ratings = Utility.GetRatings();
                var skillset = new ResourceSkillSet();
                skillset.ResourceId = resourceId;
                return View(skillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /ResourceSkillSet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ResourceSkillSetId,ResourceId,SkillSetId,SelfRating,SelfComments,RecruiterRating,RecruiterComments")] ResourceSkillSet resourceskillset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.ResourceSkillSets.Where(s => s.ResourceId == resourceskillset.ResourceId && s.SkillSetId == resourceskillset.SkillSetId).Count() > 0)
                    {
                        ModelState.AddModelError("SkillSetId", "The Skill Set chosen has already been added.");
                        ViewBag.SkillSets = new SelectList(db.SkillSets, "SkillSetId", "Name");
                        ViewBag.Ratings = Utility.GetRatings();
                        return View(resourceskillset);
                    }
                    db.ResourceSkillSets.Add(resourceskillset);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { resourceId = resourceskillset.ResourceId });
                }
                ViewBag.SkillSets = new SelectList(db.SkillSets, "SkillSetId", "Name");
                ViewBag.Ratings = Utility.GetRatings();
                return View(resourceskillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ResourceSkillSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceSkillSet resourceskillset = db.ResourceSkillSets.Find(id);
            if (resourceskillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.SkillSets = new SelectList(db.SkillSets, "SkillSetId", "Name");
            ViewBag.Ratings = Utility.GetRatings();
            ViewBag.Ratings1 = Utility.GetRatings();
            return View(resourceskillset);
        }

        // POST: /ResourceSkillSet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ResourceSkillSetId,ResourceId,SkillSetId,SelfRating,SelfComments,RecruiterRating,RecruiterComments")] ResourceSkillSet resourceskillset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(resourceskillset).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { resourceId = resourceskillset.ResourceId });
                }
                ViewBag.SkillSets = new SelectList(db.SkillSets, "SkillSetId", "Name");
                ViewBag.Ratings = Utility.GetRatings();
                return View(resourceskillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /ResourceSkillSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceSkillSet resourceskillset = db.ResourceSkillSets.Find(id);
            if (resourceskillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(resourceskillset);
        }

        // POST: /ResourceSkillSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ResourceSkillSet resourceskillset = db.ResourceSkillSets.Find(id);
                db.ResourceSkillSets.Remove(resourceskillset);
                db.SaveChanges();
                return RedirectToAction("Index", new { resourceId = resourceskillset.ResourceId });
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

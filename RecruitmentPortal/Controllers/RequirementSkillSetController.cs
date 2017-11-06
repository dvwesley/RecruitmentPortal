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
    public class RequirementSkillSetController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /RequirementSkillSet/
        public ActionResult Index()
        {
            var requirementskillsets = db.RequirementSkillSets.Include(r => r.Requirement).Include(r => r.SkillSet);
            return View(requirementskillsets.ToList());
        }

        // GET: /RequirementSkillSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RequirementSkillSet requirementskillset = db.RequirementSkillSets.Find(id);
            if (requirementskillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(requirementskillset);
        }

        // GET: /RequirementSkillSet/Create
        public ActionResult Create()
        {
            ViewBag.RequirementId = new SelectList(db.Requirements, "RequirementId", "JobNumber");
            ViewBag.SkillSetId = new SelectList(db.SkillSets, "SkillSetId", "Name");
            return View();
        }

        // POST: /RequirementSkillSet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RequirementSkillSetId,RequirementId,SkillSetId,IsMandatory")] RequirementSkillSet requirementskillset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.RequirementSkillSets.Add(requirementskillset);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RequirementId = new SelectList(db.Requirements, "RequirementId", "JobNumber", requirementskillset.RequirementId);
                ViewBag.SkillSetId = new SelectList(db.SkillSets, "SkillSetId", "Name", requirementskillset.SkillSetId);
                return View(requirementskillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RequirementSkillSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RequirementSkillSet requirementskillset = db.RequirementSkillSets.Find(id);
            if (requirementskillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.RequirementId = new SelectList(db.Requirements, "RequirementId", "JobNumber", requirementskillset.RequirementId);
            ViewBag.SkillSetId = new SelectList(db.SkillSets, "SkillSetId", "Name", requirementskillset.SkillSetId);
            return View(requirementskillset);
        }

        // POST: /RequirementSkillSet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RequirementSkillSetId,RequirementId,SkillSetId,IsMandatory")] RequirementSkillSet requirementskillset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(requirementskillset).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.RequirementId = new SelectList(db.Requirements, "RequirementId", "JobNumber", requirementskillset.RequirementId);
                ViewBag.SkillSetId = new SelectList(db.SkillSets, "SkillSetId", "Name", requirementskillset.SkillSetId);
                return View(requirementskillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RequirementSkillSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RequirementSkillSet requirementskillset = db.RequirementSkillSets.Find(id);
            if (requirementskillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(requirementskillset);
        }

        // POST: /RequirementSkillSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                RequirementSkillSet requirementskillset = db.RequirementSkillSets.Find(id);
                db.RequirementSkillSets.Remove(requirementskillset);
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

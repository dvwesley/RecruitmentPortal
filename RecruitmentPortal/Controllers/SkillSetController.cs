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
    public class SkillSetController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /SkillSet/
        public ActionResult Index()
        {
            return View(db.SkillSets.ToList());
        }

        // GET: /SkillSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            SkillSet skillset = db.SkillSets.Find(id);
            if (skillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(skillset);
        }

        // GET: /SkillSet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SkillSet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SkillSetId,Name")] SkillSet skillset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    skillset.Type = 1;
                    db.SkillSets.Add(skillset);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(skillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /SkillSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            SkillSet skillset = db.SkillSets.Find(id);
            if (skillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(skillset);
        }

        // POST: /SkillSet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SkillSetId,Name,Type")] SkillSet skillset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(skillset).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(skillset);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /SkillSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            SkillSet skillset = db.SkillSets.Find(id);
            if (skillset == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(skillset);
        }

        // POST: /SkillSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SkillSet skillset = db.SkillSets.Find(id);
                db.SkillSets.Remove(skillset);
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
        public JsonResult GetSkillSetList(string name)
        {
            IEnumerable<SelectListItem> skillsetList = SkillSet.GetList(name);
            return Json(new
            {
                iTotalRecords = skillsetList.Count(),
                iTotalDisplayRecords = skillsetList.Count(),
                aaData = skillsetList
            },
                JsonRequestBehavior.AllowGet);
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

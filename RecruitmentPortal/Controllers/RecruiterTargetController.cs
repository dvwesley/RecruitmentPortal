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
    public class RecruiterTargetController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /RecruiterTarget/
        public ActionResult Index(int recruiterId)
        {
            try
            {
                ViewBag.RecruiterId = recruiterId;
                var recruitertargets = db.RecruiterTargets.Include(r => r.CommissionType).Where(r => r.RecruiterId == recruiterId);
                var recruiter = db.Recruiters.Find(recruiterId);
                ViewBag.DocumentTitle = "Target list for - " + recruiter.AspNetUser.FirstName + " " + recruiter.AspNetUser.LastName;
                return View(recruitertargets.ToList());
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RecruiterTarget/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RecruiterTarget recruitertarget = db.RecruiterTargets.Find(id);
            if (recruitertarget == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(recruitertarget);
        }

        // GET: /RecruiterTarget/Create
        public ActionResult Create(int recruiterId)
        {
            try
            {
                ViewBag.CommissionTypes = new SelectList(db.CommissionTypes, "CommissionTypeId", "Name");
                ViewBag.Years = new Utility().Years;
                ViewBag.TargetTypes = new Utility().TargetType;
                var target = new RecruiterTarget();
                target.RecruiterId = recruiterId;
                target.TargetDate = DateTime.Now;
                return View(target);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /RecruiterTarget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RecruiterTargetId,RecruiterId,Year,TargetDate,TargetType,TargetCount,TargetReached,NoofResumesContributed,CommissionAccrued,CommissionPaid,H1BContributed,W2Placement,CTCPlacement,C1099Placement,CitizenPlacement,CTHPlacement,ProActiveHiring,Profitability,CommissionTypeId")] RecruiterTarget recruitertarget)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (recruitertarget.TargetType == Int32.Parse(new Utility().TargetType.Where(x => x.Text == "Weekly").FirstOrDefault().Value))
                    {
                        var jan1 = new DateTime(recruitertarget.Year, 1, 1);
                        var startOfFirstWeek = jan1.AddDays(1 - (int)(jan1.DayOfWeek));
                        var weeks =
                            Enumerable
                                .Range(0, 54)
                                .Select(i => new
                                {
                                    weekStart = startOfFirstWeek.AddDays(i * 7)
                                })
                                .TakeWhile(x => x.weekStart.Year <= jan1.Year)
                                .Select(x => new
                                {
                                    x.weekStart,
                                    weekFinish = x.weekStart.AddDays(4)
                                })
                                .SkipWhile(x => x.weekFinish < jan1.AddDays(1))
                                .Select((x, i) => new
                                {
                                    x.weekStart,
                                    x.weekFinish,
                                    weekNum = i + 1
                                });
                        var recruiterTargets = weeks.Select(r => new RecruiterTarget
                        {
                            Year = recruitertarget.Year,
                            TargetCount = recruitertarget.TargetCount,
                            TargetDate = r.weekStart,
                            TargetType = recruitertarget.TargetType,
                            CommissionTypeId = recruitertarget.CommissionTypeId,
                            CreatedBy = User.Identity.GetUserId(),
                            CreatedTimestamp = DateTime.Now,
                            RecruiterId = recruitertarget.RecruiterId
                        });
                        db.RecruiterTargets.AddRange(recruiterTargets);
                    }
                    else if (recruitertarget.TargetType == Int32.Parse(new Utility().TargetType.Where(x => x.Text == "Monthly").FirstOrDefault().Value))
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            var thisTarget = new RecruiterTarget
                            {
                                Year = recruitertarget.Year,
                                TargetCount = recruitertarget.TargetCount,
                                TargetDate = new DateTime(recruitertarget.Year, i, 1),
                                TargetType = recruitertarget.TargetType,
                                CommissionTypeId = recruitertarget.CommissionTypeId,
                                CreatedBy = User.Identity.GetUserId(),
                                CreatedTimestamp = DateTime.Now,
                                RecruiterId = recruitertarget.RecruiterId
                            };
                            db.RecruiterTargets.Add(thisTarget);
                        }
                    }
                    else if (recruitertarget.TargetType == Int32.Parse(new Utility().TargetType.Where(x => x.Text == "Quarterly").FirstOrDefault().Value))
                    {
                        var thisyeardate = new DateTime(recruitertarget.Year, 1, 1);
                        var dates = Enumerable.Range(0, 4).Select(i => new { quarters = thisyeardate.AddMonths(i * 3) });
                        var recruiterTargets = dates.Select(r => new RecruiterTarget
                        {
                            Year = recruitertarget.Year,
                            TargetCount = recruitertarget.TargetCount,
                            TargetDate = r.quarters,
                            TargetType = recruitertarget.TargetType,
                            CommissionTypeId = recruitertarget.CommissionTypeId,
                            CreatedBy = User.Identity.GetUserId(),
                            CreatedTimestamp = DateTime.Now,
                            RecruiterId = recruitertarget.RecruiterId
                        });
                        db.RecruiterTargets.AddRange(recruiterTargets);

                    }
                    else if (recruitertarget.TargetType == Int32.Parse(new Utility().TargetType.Where(x => x.Text == "Yearly").FirstOrDefault().Value))
                    {
                        var thisTarget = new RecruiterTarget
                        {
                            Year = recruitertarget.Year,
                            TargetCount = recruitertarget.TargetCount,
                            TargetDate = recruitertarget.TargetDate,
                            TargetType = recruitertarget.TargetType,
                            CommissionTypeId = recruitertarget.CommissionTypeId,
                            CreatedBy = User.Identity.GetUserId(),
                            CreatedTimestamp = DateTime.Now,
                            RecruiterId = recruitertarget.RecruiterId
                        };
                        db.RecruiterTargets.Add(thisTarget);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", new { recruiterId = recruitertarget.RecruiterId });
                }

                ViewBag.CommissionTypes = new SelectList(db.CommissionTypes, "CommissionTypeId", "Name");
                ViewBag.Years = new Utility().Years;
                ViewBag.TargetTypes = new Utility().TargetType;
                return View(recruitertarget);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RecruiterTarget/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RecruiterTarget recruitertarget = db.RecruiterTargets.Find(id);
            if (recruitertarget == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            try
            {
                ViewBag.CommissionTypes = new SelectList(db.CommissionTypes, "CommissionTypeId", "Name");
                ViewBag.Years = new Utility().Years;
                ViewBag.TargetTypes = new Utility().TargetType;
                return View(recruitertarget);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /RecruiterTarget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RecruiterTargetId,RecruiterId,Year,TargetDate,TargetType,TargetCount,TargetReached,NoofResumesContributed,CommissionAccrued,CommissionPaid,H1BContributed,W2Placement,CTCPlacement,C1099Placement,CitizenPlacement,CTHPlacement,ProActiveHiring,Profitability,CommissionTypeId")] RecruiterTarget recruitertarget)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    recruitertarget.CreatedBy = User.Identity.GetUserId();
                    recruitertarget.CreatedTimestamp = DateTime.Now;
                    recruitertarget.UpdatedBy = User.Identity.GetUserId();
                    recruitertarget.UpdatedTimestamp = DateTime.Now;

                    db.Entry(recruitertarget).State = EntityState.Modified;
                    db.Entry(recruitertarget).Property(r => r.CreatedBy).IsModified = false;
                    db.Entry(recruitertarget).Property(r => r.CreatedTimestamp).IsModified = false;

                    db.SaveChanges();
                    return RedirectToAction("Index", new { recruiterId = recruitertarget.RecruiterId });
                }
                ViewBag.CommissionTypes = new SelectList(db.CommissionTypes, "CommissionTypeId", "Name");
                ViewBag.Years = new Utility().Years;
                ViewBag.TargetTypes = new Utility().TargetType;
                return View(recruitertarget);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /RecruiterTarget/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            RecruiterTarget recruitertarget = db.RecruiterTargets.Find(id);
            if (recruitertarget == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(recruitertarget);
        }

        // POST: /RecruiterTarget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                RecruiterTarget recruitertarget = db.RecruiterTargets.Find(id);
                db.RecruiterTargets.Remove(recruitertarget);
                db.SaveChanges();
                return RedirectToAction("Index", new { recruiterId = recruitertarget.RecruiterId });
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

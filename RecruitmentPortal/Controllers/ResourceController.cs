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
    public class ResourceController : Controller
    {
        private RPEntities db = new RPEntities();

        // GET: /Resource/
        public ActionResult Index()
        {
            var resources = db.Resources.Include(r => r.Client).Include(r => r.Referrer).Include(r => r.ResourceType).Include(r => r.Vendor).Include(r => r.Vendor1);
            return View(resources.ToList());
        }

        // GET: /Resource/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            ResourceProfileViewModel resourceProfile = ResourceProfileViewModel.Initialize(id.Value);            
            if (resourceProfile == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(resourceProfile);
        }

        // GET: /Resource/Create
        public ActionResult Create()
        {            
            ViewBag.ResourceTypes = new SelectList(db.ResourceTypes.OrderBy(r => r.Name), "ResourceTypeId", "Name");         
            return View();
        }

        // POST: /Resource/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,MiddleInitial,Sex,ResourceTypeId,DOB,PhoneNo,Email")] ResourceViewModel model)
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
                        PhoneNo = model.PhoneNo,
                        CreatedTimestamp = DateTime.Now,
                        Active = true
                    };
                    TempData["User"] = user;
                    TempData["resourcemodel"] = model;
                    return RedirectToAction("CreateOrUpdate", "Account", new { returnAction = "Create1", returnController = "Resource", userId = true });
                }
                ViewBag.ResourceTypes = new SelectList(db.ResourceTypes.OrderBy(r => r.Name), "ResourceTypeId", "Name");
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
                var model = TempData["resourcemodel"] as ResourceViewModel;
                var resource = new Resource
                {
                    Sex = model.Sex,
                    ResourceTypeId = model.ResourceTypeId,
                    DOB = model.DOB,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedTime = DateTime.Now,
                    UserId = userId
                };
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Edit/" + resource.ResourceId);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Resource/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            try
            {
                var model = new ResourceViewModel()
                {
                    ResourceId = resource.ResourceId,
                    FirstName = resource.AspNetUser.FirstName,
                    LastName = resource.AspNetUser.LastName,
                    MiddleInitial = resource.AspNetUser.MiddleInitial,
                    Address1 = resource.AspNetUser.Address1,
                    Address2 = resource.AspNetUser.Address2,
                    City = resource.AspNetUser.City,
                    State = resource.AspNetUser.State,
                    Zipcode = resource.AspNetUser.Zipcode,
                    PhoneNo = resource.AspNetUser.PhoneNo,
                    Email = resource.AspNetUser.UserName,
                    Sex = resource.Sex,
                    DOB = resource.DOB,
                    ResourceTypeId = resource.ResourceTypeId,
                    PhoneNo2 = resource.PhoneNo2,
                    Email2 = resource.Email2,
                    LinkedInProfile = resource.LinkedInProfile,
                    SkypeId = resource.SkypeId,
                    YearsOfExperience = resource.YearsOfExperience,
                    PersonalInterview = resource.PersonalInterview,
                    HourlyRate = resource.HourlyRate,
                    Salary = resource.Salary,
                    CTC = resource.CTC,
                    SwitchLaneInterviewed = resource.SwitchLaneInterviewed,
                    NoofClientInterviews = resource.NoofClientInterviews,
                    DomainExpertise = resource.DomainExpertise,
                    LocationPreference = resource.LocationPreference,
                    BackgroundCheck = resource.BackgroundCheck,
                    EducationCertificates = resource.EducationCertificates,
                    VisaCopy = resource.VisaCopy,
                    PassPortCopy = resource.PassPortCopy,
                    LicenseCopy = resource.LicenseCopy,
                    PhotoId = resource.PhotoId,
                    I9Form = resource.I9Form,
                    GreenCardDocuments = resource.GreenCardDocuments,
                    PayStubs = resource.PayStubs,
                    W2Copy = resource.W2Copy,
                    MaritalStatus = resource.MaritalStatus,
                    NoofKids = resource.NoofKids,
                    Anniversary = resource.Anniversary,
                    LastMeetingDate = resource.LastMeetingDate,
                    NextMeetingDate = resource.NextMeetingDate,
                    TrainingAreas = resource.TrainingAreas,
                    ClientId = resource.ClientId,
                    SpouseDOB = resource.SpouseDOB,
                    Kid1DOB = resource.Kid1DOB,
                    Kid2DOB = resource.Kid2DOB,
                    ReferrerId = resource.ReferrerId,
                    ReferralAmountDue = resource.ReferralAmountDue,
                    ReferredConsultantId = resource.ReferredConsultantId,
                    Tier1VendorId = resource.Tier1VendorId,
                    Tier2VendorId = resource.Tier2VendorId,
                    USExperience = resource.USExperience,
                    VisaExpirationDate = resource.VisaExpirationDate,
                    EAD = resource.EAD,
                    OPT = resource.OPT,
                    NDA = resource.NDA,
                    Source = resource.Source,
                    Availability = resource.Availability,
                    BlackListed = resource.BlackListed,
                    PreviousNotes = resource.Notes,
                    Qualification = resource.Qualification,
                    ResidentialStatus = resource.ResidentialStatus,
                    VisaType = resource.VisaType,
                    Ref1CompanyName = resource.Ref1CompanyName,
                    Ref1Name = resource.Ref1Name,
                    Ref1Designation = resource.Ref1Designation,
                    Ref1Email = resource.Ref1Email,
                    Ref1Phone = resource.Ref1Phone,
                    Ref2CompanyName = resource.Ref2CompanyName,
                    Ref2Name = resource.Ref2Name,
                    Ref2Designation = resource.Ref2Designation,
                    Ref2Email = resource.Ref2Email,
                    Ref2Phone = resource.Ref2Phone,
                    Ref3CompanyName = resource.Ref3CompanyName,
                    Ref3Name = resource.Ref3Name,
                    Ref3Designation = resource.Ref3Designation,
                    Ref3Email = resource.Ref3Email,
                    Ref3Phone = resource.Ref3Phone,
                    Medical = resource.Medical,
                    Vision = resource.Vision,
                    Dental = resource.Dental,
                    Relocation = resource.Relocation,
                    FourZeroOneK = resource.FourZeroOneK,
                    STD = resource.STD,
                    LTD = resource.LTD,
                    LifeInsurance = resource.LifeInsurance
                };

                var resources = db.Resources.Where(r => r.ResourceId != id.Value).ToList().Select(r => new
                {
                    ResourceId = r.ResourceId,
                    ResourceName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });

                var referrers = db.Referrers.ToList().Select(r => new
                {
                    ReferrerId = r.ReferrerId,
                    ReferrerName = r.FirstName + " " + r.LastName
                });

                ViewBag.ResourceTypes = new SelectList(db.ResourceTypes, "ResourceTypeId", "Name");
                ViewBag.Referrers = new SelectList(referrers, "ReferrerId", "ReferrerName");
                ViewBag.Resources = new SelectList(resources, "ResourceId", "ResourceName");
                ViewBag.Tier1Vendors = new SelectList(db.Vendors, "VendorId", "Name");
                ViewBag.Tier2Vendors = new SelectList(db.Vendors, "VendorId", "Name");
                ViewBag.Clients = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.States = new Utility().States;
                ViewBag.VisaTypes = new Utility().VisaType;
                ViewBag.Availabilities = new Utility().Availability;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // POST: /Resource/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResourceId,FirstName,LastName,MiddleInitial,Sex,DOB,Address1,Address2,City,State,ZipCode,ResourceTypeId,PhoneNo,PhoneNo2,Email,Email2,LinkedInProfile,SkypeId,YearsOfExperience,PersonalInterview,HourlyRate,Salary,SwitchLaneInterviewed,NoofClientInterviews,DomainExpertise,LocationPreference,BackgroundCheck,MaritalStatus,NoofKids,Anniversary,LastMeetingDate,NextMeetingDate,TrainingAreas,ClientId,ProjectEndDate,SpouseDOB,Kid1DOB,Kid2DOB,ReferrerId,ReferralAmountDue,ReferredConsultantId,Tier1VendorId,Tier2VendorId,USExperience,VisaExpirationDate,Source,Availability,BlackListed,Notes,Qualification,ResidentialStatus,VisaType,Ref1CompanyName,Ref1Name,Ref1Email,Ref1Phone,Ref1Designation,Ref2CompanyName,Ref2Name,Ref2Email,Ref2Phone,Ref2Designation,Ref3CompanyName,Ref3Name,Ref3Email,Ref3Phone,Ref3Designation,Medical,Vision,Dental,Relocation,FourZeroOneK,STD,LTD,LifeInsurance")] ResourceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = db.AspNetUsers.Find(User.Identity.GetUserId());
                    Resource resource = db.Resources.Find(model.ResourceId);
                    resource.Sex = model.Sex;
                    resource.DOB = model.DOB;
                    resource.ResourceTypeId = model.ResourceTypeId;
                    resource.PhoneNo2 = model.PhoneNo2;
                    resource.Email2 = model.Email2;
                    resource.LinkedInProfile = model.LinkedInProfile;
                    resource.SkypeId = model.SkypeId;
                    resource.YearsOfExperience = model.YearsOfExperience;
                    resource.PersonalInterview = model.PersonalInterview;
                    resource.HourlyRate = model.HourlyRate;
                    resource.Salary = model.Salary;
                    resource.SwitchLaneInterviewed = model.SwitchLaneInterviewed;
                    resource.NoofClientInterviews = model.NoofClientInterviews;
                    resource.DomainExpertise = model.DomainExpertise;
                    resource.LocationPreference = model.LocationPreference;
                    resource.BackgroundCheck = model.BackgroundCheck;
                    resource.MaritalStatus = model.MaritalStatus;
                    resource.NoofKids = model.NoofKids;
                    resource.Anniversary = model.Anniversary;
                    resource.LastMeetingDate = model.LastMeetingDate;
                    resource.NextMeetingDate = model.NextMeetingDate;
                    resource.TrainingAreas = model.TrainingAreas;
                    resource.ClientId = model.ClientId;
                    resource.SpouseDOB = model.SpouseDOB;
                    resource.Kid1DOB = model.Kid1DOB;
                    resource.Kid2DOB = model.Kid2DOB;
                    resource.ReferrerId = model.ReferrerId;
                    resource.ReferralAmountDue = model.ReferralAmountDue;
                    resource.ReferredConsultantId = model.ReferredConsultantId;
                    resource.Tier1VendorId = model.Tier1VendorId;
                    resource.Tier2VendorId = model.Tier2VendorId;
                    resource.USExperience = model.USExperience;
                    resource.VisaExpirationDate = model.VisaExpirationDate;
                    resource.Source = model.Source;
                    resource.Availability = model.Availability;
                    resource.BlackListed = model.BlackListed;
                    resource.Qualification = model.Qualification;
                    resource.ResidentialStatus = model.ResidentialStatus;
                    resource.VisaType = model.VisaType;
                    resource.Ref1CompanyName = model.Ref1CompanyName;
                    resource.Ref1Name = model.Ref1Name;
                    resource.Ref1Designation = model.Ref1Designation;
                    resource.Ref1Email = model.Ref1Email;
                    resource.Ref1Phone = model.Ref1Phone;
                    resource.Ref2CompanyName = model.Ref2CompanyName;
                    resource.Ref2Name = model.Ref2Name;
                    resource.Ref2Designation = model.Ref2Designation;
                    resource.Ref2Email = model.Ref2Email;
                    resource.Ref2Phone = model.Ref2Phone;
                    resource.Ref3CompanyName = model.Ref3CompanyName;
                    resource.Ref3Name = model.Ref3Name;
                    resource.Ref3Designation = model.Ref3Designation;
                    resource.Ref3Email = model.Ref3Email;
                    resource.Ref3Phone = model.Ref3Phone;
                    resource.Medical = model.Medical;
                    resource.Vision = model.Vision;
                    resource.Dental = model.Dental;
                    resource.Relocation = model.Relocation;
                    resource.FourZeroOneK = model.FourZeroOneK;
                    resource.STD = model.STD;
                    resource.LTD = model.LTD;
                    resource.LifeInsurance = model.LifeInsurance;
                    resource.Notes = resource.Notes + "<br />" + model.Notes + " - " + currentUser.FirstName + " " + currentUser.LastName + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

                    db.Entry(resource).State = EntityState.Modified;
                    db.Entry(resource).Property(x => x.CreatedBy).IsModified = false;
                    db.Entry(resource).Property(x => x.CreatedTime).IsModified = false;
                    resource.UpdatedBy = User.Identity.GetUserId();
                    resource.UpdatedTime = DateTime.Now;
                    db.SaveChanges();

                    var user = new ApplicationUser
                    {
                        Id = resource.UserId,
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
                    return RedirectToAction("CreateOrUpdate", "Account", new { returnAction = "Index", returnController = "Resource", userId = false });
                }

                var resources = db.Resources.Where(r => r.ResourceId != model.ResourceId).ToList().Select(r => new
                {
                    ResourceId = r.ResourceId,
                    ResourceName = r.AspNetUser.FirstName + " " + r.AspNetUser.LastName
                });
                var referrers = db.Referrers.ToList().Select(r => new
                {
                    ReferrerId = r.ReferrerId,
                    ReferrerName = r.FirstName + " " + r.LastName
                });

                ViewBag.Referrers = new SelectList(referrers, "ReferrerId", "ReferrerName");
                ViewBag.Resources = new SelectList(resources, "ResourceId", "ResourceName");
                ViewBag.ResourceTypes = new SelectList(db.ResourceTypes, "ResourceTypeId", "Name");
                ViewBag.Tier1Vendors = new SelectList(db.Vendors, "VendorId", "Name");
                ViewBag.Tier2Vendors = new SelectList(db.Vendors, "VendorId", "Name");
                ViewBag.Clients = new SelectList(db.Clients, "ClientId", "Name");
                ViewBag.States = new Utility().States;
                ViewBag.VisaTypes = new Utility().VisaType;
                ViewBag.Availabilities = new Utility().Availability;
                return View(model);
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }

        // GET: /Resource/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(resource);
        }

        // POST: /Resource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Resource resource = db.Resources.Find(id);
                db.Resources.Remove(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
                return RedirectToAction("AppError", "Error");
            }
        }        

        public ActionResult UpdateChecklist(int resourceId, int documentTypeId, bool flag)
        {
            try
            {
                //var resource = db.Resources.Find(resourceId);
                var resource = new Resource { ResourceId = resourceId };
                string modifiedProperty = "";
                switch (documentTypeId)
                {                    
                    case ((int)DocumentTypes.EducationalCertificates):
                        resource.EducationCertificates = flag;
                        modifiedProperty = "EducationCertificates";
                        break;
                    case ((int)DocumentTypes.GreenCard):
                        resource.GreenCardDocuments = flag;
                        modifiedProperty = "GreenCardDocuments";
                        break;
                    case ((int)DocumentTypes.I9Form):
                        resource.I9Form = flag;
                        modifiedProperty = "I9Form";
                        break;                 
                    case ((int)DocumentTypes.InterviewFeedback):
                        resource.PersonalInterview = flag;
                        modifiedProperty = "PersonalInterview";
                        break;                    
                    case ((int)DocumentTypes.License):
                        resource.LicenseCopy = flag;
                        modifiedProperty = "LicenseCopy";
                        break;
                    case ((int)DocumentTypes.PassPort):
                        resource.PassPortCopy = flag;
                        modifiedProperty = "PassPortCopy";
                        break;
                    case ((int)DocumentTypes.Paystub):
                        resource.PayStubs = flag;
                        modifiedProperty = "PayStubs";
                        break;
                    case ((int)DocumentTypes.PhotoIdentity):
                        resource.PhotoId = flag;
                        modifiedProperty = "PhotoId";
                        break;                    
                    case ((int)DocumentTypes.Visa):
                        resource.VisaCopy = flag;
                        modifiedProperty = "VisaCopy";
                        break;
                    case ((int)DocumentTypes.W2):
                        resource.W2Copy = flag;
                        modifiedProperty = "W2Copy";
                        break;
                    case ((int)DocumentTypes.EAD):
                        resource.EAD = flag;
                        modifiedProperty = "EAD";
                        break;
                    case ((int)DocumentTypes.OPT):
                        resource.OPT = flag;
                        modifiedProperty = "OPT";
                        break;
                    case ((int)DocumentTypes.NDA):
                        resource.NDA = flag;
                        modifiedProperty = "NDA";
                        break;
                    default:
                        break;
                }
                if(modifiedProperty.Length > 0)
                {
                    resource.UpdatedBy = User.Identity.GetUserId();
                    resource.UpdatedTime = DateTime.Now;
                    db.Resources.Attach(resource);
                    db.Entry(resource).Property(x => x.UpdatedBy).IsModified = true;
                    db.Entry(resource).Property(x => x.UpdatedTime).IsModified = true;
                    db.Entry(resource).Property(modifiedProperty).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }                
                return RedirectToAction("Index", "ResourceDocument", new { resourceId = resource.ResourceId });
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

        [HttpGet]
        public JsonResult GetResourceList(string name)
        {
            IEnumerable<SuggestedResourceModel> resourceList = Resource.GetList(name);
            return Json(new
            {
                iTotalRecords = resourceList.Count(),
                iTotalDisplayRecords = resourceList.Count(),
                aaData = resourceList
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

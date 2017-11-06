using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Configuration;
using System.Data.Entity;
using System.Web.Mvc;
using RecruitmentPortal.Common;
using RecruitmentPortal.Extensions;

namespace RecruitmentPortal.Models
{
    [MetadataType(typeof(ResourceMetadata))]
    public partial class Resource
    {
        public string ResidentialStatusDisplay
        {
            get
            {
                if(ResidentialStatus == "V")
                {
                    return "Visa";
                }
                else if (ResidentialStatus == "G")
                {
                    return "Green Card";
                }
                else if (ResidentialStatus == "C")
                {
                    return "Citizen";
                }
                return string.Empty;
            }
        }

        public string GenderDisplay
        {
            get
            {
                if (ResidentialStatus == "F")
                {
                    return "Female";
                }
                else if (ResidentialStatus == "M")
                {
                    return "Male";
                }                
                return string.Empty;
            }
        }

        public string CityStateZipDisplay
        {
            get
            {
                StringBuilder returnValue = new StringBuilder();
                if (AspNetUser.City != null && AspNetUser.City.Length > 0)
                {
                    returnValue.Append(AspNetUser.City);                
                }
                if (AspNetUser.State != null && AspNetUser.State.Length > 0)
                {
                    returnValue.Append((returnValue.Length > 0 ? ", " : "") + AspNetUser.State);
                }
                if (AspNetUser.Zipcode != null && AspNetUser.Zipcode.Length > 0)
                {
                    returnValue.Append((returnValue.Length > 0 ? " - " : "") + AspNetUser.Zipcode);
                }                
                return returnValue.ToString();
            }
        }

        public string PhoneDisplay
        {
            get
            {
                StringBuilder returnValue = new StringBuilder();
                if (AspNetUser.PhoneNo != null && AspNetUser.PhoneNo.Length > 0)
                {
                    returnValue.Append(AspNetUser.PhoneNo);                
                }                
                if (PhoneNo2 != null && PhoneNo2.Length > 0)
                {
                    returnValue.Append((returnValue.Length > 0 ? ", " : "") + PhoneNo2);
                }                
                return returnValue.ToString();
            }
        }

        [Display(Name = "Consultant Name")]
        public string ResourceName
        {
            get
            {
                return AspNetUser.FirstName + " " + AspNetUser.LastName;
            }
        }

        public static Resource Initialize(int resourceId)
        {
            Resource resource = null;
            using (RPEntities db = new RPEntities())
            {
                resource = db.Resources.Include(r => r.ResourceDocuments.Select(d => d.DocumentType)).Include(t => t.ResourceType).Include(v => v.Vendor).Include(v => v.Vendor1)
                    .Include(c => c.Client).Include(u => u.AspNetUser).Include(s => s.ResourceSkillSets.Select(n => n.SkillSet)).Where(r => r.ResourceId == resourceId).FirstOrDefault();
            }
            return resource;
        }

        public static IEnumerable<SuggestedResourceModel> GetList(string name)
        {            
            List<SuggestedResourceModel> resources = null;
            try
            {         
                using (RPEntities db = new RPEntities())
                {
                    resources = db.Resources.Where(u => u.AspNetUser.FirstName.Contains(name) || u.AspNetUser.LastName.Contains(name))
                        .Select(g => new SuggestedResourceModel
                        {
                            ResourceId = g.ResourceId,
                            Name = g.AspNetUser.FirstName + " " + g.AspNetUser.LastName,                            
                            Availability = g.Availability,
                            ProjectEndDate = g.ProjectEndDate
                        }).ToList();
                }
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
            }
            return resources;
        }

        public static IEnumerable<SuggestedResourceModel> GetResources(int requirementId)
        {
            using (RPEntities db = new RPEntities())
            {
                var requirementSkillSetIds = db.RequirementSkillSets.Where(r => r.RequirementId == requirementId).Select(s => s.SkillSetId).ToList();
                var resources = db.Resources.Include(r => r.AspNetUser).Where(r => r.ResourceSkillSets.Any(s => requirementSkillSetIds.Contains(s.SkillSetId)))
                    .GroupBy(x => new { x.ResourceId, x.AspNetUser.FirstName, x.AspNetUser.LastName, x.Availability, x.ProjectEndDate })
                    .Select(g => new SuggestedResourceModel
                    {
                        ResourceId = g.Key.ResourceId,
                        Name = g.Key.FirstName + " " + g.Key.LastName,                        
                        Availability = g.Key.Availability,
                        ProjectEndDate = g.Key.ProjectEndDate
                    });
                return resources.ToList();
            }
        }
    }

    [MetadataType(typeof(ResourceDocumentMetadata))]
    public partial class ResourceDocument
    {
        public string DocumentPath
        {
            get
            {                
                string docType = ((DocumentTypes)DocumentTypeId).ToString();
                return ConfigurationManager.AppSettings[docType];
                /*
                switch (DocumentTypeId)
                {
                    case ((int)DocumentTypes.Contracts):
                        docPath = ConfigurationManager.AppSettings["Contracts"];
                        break;
                    case ((int)DocumentTypes.EducationalCertificates):
                        docPath = ConfigurationManager.AppSettings["EducationalCertificates"];
                        break;
                    case ((int)DocumentTypes.GreenCard):
                        docPath = ConfigurationManager.AppSettings["GreenCard"];
                        break;
                    case ((int)DocumentTypes.I9Form):
                        docPath = ConfigurationManager.AppSettings["I9Form"];
                        break;
                    case ((int)DocumentTypes.InsuranceCertificates):
                        docPath = ConfigurationManager.AppSettings["InsuranceCertificates"];
                        break;
                    case ((int)DocumentTypes.InterviewFeedback):
                        docPath = ConfigurationManager.AppSettings["InterviewFeedback"];
                        break;
                    case ((int)DocumentTypes.InterviewQnsExcerciseFiles):
                        docPath = ConfigurationManager.AppSettings["InterviewQnsExcerciseFiles"];
                        break;
                    case ((int)DocumentTypes.InterviewVideo):
                        docPath = ConfigurationManager.AppSettings["InterviewVideo"];
                        break;
                    case ((int)DocumentTypes.Invoices):
                        docPath = ConfigurationManager.AppSettings["Invoices"];
                        break;
                    case ((int)DocumentTypes.License):
                        docPath = ConfigurationManager.AppSettings["License"];
                        break;
                    case ((int)DocumentTypes.PassPort):
                        docPath = ConfigurationManager.AppSettings["PassPort"];
                        break;
                    case ((int)DocumentTypes.Paystub):
                        docPath = ConfigurationManager.AppSettings["Paystub"];
                        break;
                    case ((int)DocumentTypes.PhotoIdentity):
                        docPath = ConfigurationManager.AppSettings["PhotoIdentity"];
                        break;
                    case ((int)DocumentTypes.ProfilePicture):
                        docPath = ConfigurationManager.AppSettings["ProfilePicture"];
                        break;
                    case ((int)DocumentTypes.Resume):
                        docPath = ConfigurationManager.AppSettings["Resume"];
                        break;
                    case ((int)DocumentTypes.Visa):
                        docPath = ConfigurationManager.AppSettings["Visa"];
                        break;
                    case ((int)DocumentTypes.W2Copy):
                        docPath = ConfigurationManager.AppSettings["W2Copy"];
                        break;
                    default:
                        break;
                }
                return docPath;
                 */
            }
        }        
    }

    [MetadataType(typeof(ResourceSkillSetMetadata))]
    public partial class ResourceSkillSet
    {        
    }

    [MetadataType(typeof(InterviewMetadata))]
    public partial class Interview
    {
        public string InterviewTypeDisplay
        {
            get
            {
                if (InterviewType == 1)
                {
                    return "SwitchLane";
                }
                else if (InterviewType == 2)
                {
                    return "Client";
                }
                return string.Empty;
            }
        }

        public string InterviewStatusDisplay
        {
            get
            {
                return new Utility().InterviewStatus.Where(s => s.Value == Status).FirstOrDefault().Text;
            }
        }

        public static IEnumerable<InterviewViewModel> GetInterviews(int resourceId)
        {            
            using (RPEntities db = new RPEntities())
            {
                var interviews = db.Interviews.Include(i => i.InterviewDetails).Where(i => i.ResourceId == resourceId).Select(x => new InterviewViewModel
                {                    
                    InterviewId = x.InterviewId,
                    InterviewDate = x.StartTime,
                    InterviewType = x.InterviewType,
                    InterviewStatus = x.Status,
                    InterviewerName = x.Interviewer.AspNetUser.FirstName + " " + x.Interviewer.AspNetUser.LastName,
                    InterviewerId = x.InterviewerId.Value,
                    AverageRating = x.InterviewDetails.Where(d => d.InterviewId == x.InterviewId).Select(r => r.Rating).Average()
                });
                return interviews.ToList();
            }            
        }
    }

    [MetadataType(typeof(InterviewDocumentMetadata))]
    public partial class InterviewDocument
    {
        public string DocumentPath
        {
            get
            {
                string docType = ((DocumentTypes)DocumentTypeId).ToString();
                return ConfigurationManager.AppSettings[docType];     
            }
        }
    }

    [MetadataType(typeof(InterviewDetailMetadata))]
    public partial class InterviewDetail
    {
    }

    [MetadataType(typeof(InterviewerMetadata))]
    public partial class Interviewer
    {        
    }

    [MetadataType(typeof(ClientMetadata))]
    public partial class Client
    { 
        public string BlackListedDisplay
        {
            get
            {
                return BlackListed == true ? "Yes" : "No";
            }
        }
    }

    [MetadataType(typeof(ClientDocumentMetadata))]
    public partial class ClientDocument
    {
        public string DocumentPath
        {
            get
            {
                string docType = ((DocumentTypes)DocumentTypeId).ToString();
                return ConfigurationManager.AppSettings[docType];
            }
        }
    }

    [MetadataType(typeof(VendorMetadata))]
    public partial class Vendor
    {
        public string BlackListedDisplay
        {
            get
            {
                return BlackListed == true ? "Yes" : "No";
            }
        }

        public string W9Display
        {
            get
            {
                return W9 == true ? "Yes" : "No";
            }
        }

        public string ContractDisplay
        {
            get
            {
                return Contracts == true ? "Yes" : "No";
            }
        }

        public string InsuranceCertificatesDisplay
        {
            get
            {
                return InsuranceCertificates == true ? "Yes" : "No";
            }
        }
    }

    [MetadataType(typeof(VendorDocumentMetadata))]
    public partial class VendorDocument
    {
        public string DocumentPath
        {
            get
            {
                string docType = ((DocumentTypes)DocumentTypeId).ToString();
                return ConfigurationManager.AppSettings[docType];
            }
        }
    }

    [MetadataType(typeof(RequirementMetadata))]
    public partial class Requirement
    {
        [Display(Name = "No of Positions")]
        [Range(1,100)]
        public int NoofPositions { get; set; }

        public string PriorityDisplay
        {
            get
            {
                return new Utility().Priority.Find(p => p.Value == Priority.ToString()).Text;
            }
        }

        public string StatusDisplay
        {
            get
            {
                return new Utility().RequirementStatus.Find(p => p.Value == Status.ToString()).Text;
            }
        }

        [Display(Name = "Skill Set")]
        public String SkillSetCSV
        {
            get
            {
                StringBuilder skillsetCSV = new StringBuilder();
                if (RequirementSkillSets != null && RequirementSkillSets.Count() > 0)
                {
                    foreach (RequirementSkillSet skillset in RequirementSkillSets)
                    {
                        skillsetCSV.Append(skillset.SkillSet.Name + ", ");
                    }
                    return skillsetCSV.ToString().Substring(0, skillsetCSV.Length - 2);
                }
                return skillsetCSV.ToString();
            }
        }

        public String MandatorySkillSetCSV
        {
            get
            {
                StringBuilder skillsetCSV = new StringBuilder();
                if (RequirementSkillSets != null && RequirementSkillSets.Where(s => s.IsMandatory == true).Count() > 0)
                {
                    foreach (RequirementSkillSet skillset in RequirementSkillSets.Where(s => s.IsMandatory == true))
                    {
                        skillsetCSV.Append("," + skillset.SkillSetId + ",");
                    }                    
                }
                return skillsetCSV.ToString();
            }
        }

        public Requirement Copy()
        {
            var requirement = new Requirement();
            requirement.JobNumber = JobNumber;
            requirement.JobTitle = JobTitle;
            requirement.Priority = Priority;
            requirement.Status = Status;
            requirement.Tier1ClientId = Tier1ClientId;
            requirement.Tier2ClientId = Tier2ClientId;
            requirement.Location = Location;
            requirement.Duration = Duration;
            requirement.HourlyBuyRate = HourlyBuyRate;
            requirement.HourlyBillingRate = HourlyBillingRate;
            requirement.JobTypeId = JobTypeId;
            requirement.OneTimeFee = OneTimeFee;            
            requirement.RecruiterId = RecruiterId;
            requirement.AccountManagerId = AccountManagerId;
            requirement.Description = Description;
            requirement.NoofPositions = NoofPositions;
            return requirement;
        }

        public static IEnumerable<RequirementDisplayModel> GetRequirements(int resourceId)
        {
            using (RPEntities db = new RPEntities())
            {
                var resourceSkillSetIds = db.ResourceSkillSets.Where(r => r.ResourceId == resourceId).Select(s => s.SkillSetId).ToList();
                var requirements = db.Requirements.Include(r => r.Client).Where(r => r.RequirementSkillSets.Any(s => resourceSkillSetIds.Contains(s.SkillSetId)) && r.Status == 1)
                    .GroupBy(x => new { x.JobNumber, x.JobTitle, x.Client.Name, x.Description })
                    .Select(g => new RequirementDisplayModel
                    {
                        Client = g.Key.Name,
                        JobNumber = g.Key.JobNumber,
                        JobTitle = g.Key.JobTitle,
                        Description = g.Key.Description,
                        RequirementId = g.Min(r => r.RequirementId)
                    });
                return requirements.ToList();
            }
        }
    }

    public partial class SkillSet
    {
        public static IEnumerable<SelectListItem> GetList(string name)
        {
            List<SelectListItem> skillsets = new List<SelectListItem>();
            try
            {
                SelectListItem skillset = null;
                using (RPEntities db = new RPEntities())
                {
                    var skillsetsFromDB = db.SkillSets.Where(u => u.Name.Contains(name)).ToList();
                    {
                        foreach (SkillSet thisSkillSet in skillsetsFromDB)
                        {
                            skillset = new SelectListItem();
                            skillset.Value = thisSkillSet.SkillSetId.ToString();
                            skillset.Text = thisSkillSet.Name;
                            skillset.Selected = false;
                            skillsets.Add(skillset);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.LogException(exp);
            }
            return skillsets;
        }
    }

    [MetadataType(typeof(RequirementDetailMetadata))]   
    public partial class RequirementDetail
    { 
        [Display(Name = "Referred to Client")]
        public string ReferredToClientDisplay
        {
            get
            {
                return ReferredToClient ? "Yes" : "No";
            }
        }

        [Display(Name = "Candidate Selected")]
        public string CandidateSelectedDisplay
        {
            get
            {
                return CandidateSelected ? "Yes" : "No";
            }
        }
    }

    public partial class Recruiter
    {        
    }    

    [MetadataType(typeof(RecruiterTargetMetadata))]
    public partial class RecruiterTarget
    {
        public string TargetTypeDisplay
        {
            get
            {
                return new Utility().TargetType.Where(x => x.Value == TargetType.ToString()).FirstOrDefault().Text;
            }
        }
    }

    [MetadataType(typeof(SkillSetMetadata))]
    public partial class SkillSet
    { }

    [MetadataType(typeof(ReferrerMetadata))]
    public partial class Referrer
    { }

    [MetadataType(typeof(CommissionTypeMetadata))]
    public partial class CommissionType
    {
        public string IsPercentDisplay
        {
            get
            {
                return IsPercentage ? "Yes" : "No";
            }
        }        
    }

    [MetadataType(typeof(DocumentTypeMetadata))]
    public partial class DocumentType
    {
        [Display(Name = "Display Group")]
        public string DocumentGroupDisplay
        {
            get
            {
                return EnumHelper<DocumentGroup>.GetDisplayValue((Models.DocumentGroup) DocumentGroup);
            }
        }
    }
}
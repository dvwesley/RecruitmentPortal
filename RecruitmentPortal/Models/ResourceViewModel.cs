using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentPortal.Models
{
    public class ResourceProfileViewModel
    {
        public Resource Resource { get; set; }
        public String ProfilePictureLink { get; set; }
        public IEnumerable<InterviewViewModel> Interviews { get; set; }
        public IEnumerable<RequirementDisplayModel> Requirements { get; set; }

        public static ResourceProfileViewModel Initialize(int resourceId)
        {
            ResourceProfileViewModel resourceProfile = new ResourceProfileViewModel();
            resourceProfile.Resource = Resource.Initialize(resourceId);
            var resoureDoc = resourceProfile.Resource.ResourceDocuments.Where(d => d.DocumentTypeId == (int)DocumentTypes.ProfilePicture).
                OrderByDescending(d => d.UploadedTimestamp).FirstOrDefault();
            if(resoureDoc != null)
            {
                resourceProfile.ProfilePictureLink = resoureDoc.FilePath;
            }            
            if (resourceProfile.ProfilePictureLink == null || resourceProfile.ProfilePictureLink.Length == 0)
            {
                //set default path.
                resourceProfile.ProfilePictureLink = "/Images/default.png";
            }
            resourceProfile.Interviews = Interview.GetInterviews(resourceProfile.Resource.ResourceId);
            resourceProfile.Requirements = Requirement.GetRequirements(resourceProfile.Resource.ResourceId);
            return resourceProfile;
        }
    } 

    public class ResourceViewModel
    {
        public int ResourceId { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleInitial { get; set; }

        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip.")]
        public string Zipcode { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DOB is required.")]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sex is required.")]
        public string Sex { get; set; }

        [Display(Name = "Resource Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Resource Type is required.")]
        public int ResourceTypeId { get; set; }

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNo2 { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email2 { get; set; }

        [StringLength(50)]
        public string LinkedInProfile { get; set; }

        [StringLength(50)]
        public string SkypeId { get; set; }

        [StringLength(100)]
        public string Availability { get; set; }

        [Display(Name = "Years of Experience")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Years of Experience.")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> YearsOfExperience { get; set; }

        [Display(Name = "US Experience")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid US Experience.")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<double> USExperience { get; set; }

        [Display(Name = "Domain Expertise")]
        [StringLength(500)]
        public string DomainExpertise { get; set; }

        [Display(Name = "Spouse DOB")]
        public Nullable<System.DateTime> SpouseDOB { get; set; }

        [Display(Name = "Kid1 DOB")]
        public Nullable<System.DateTime> Kid1DOB { get; set; }

        [Display(Name = "Kid2 DOB")]
        public Nullable<System.DateTime> Kid2DOB { get; set; }

        [Display(Name = "No of Kids")]
        [Range(0, 10, ErrorMessage = "Invalid data for No of Kids.")]
        public Nullable<int> NoofKids { get; set; }

        public Nullable<DateTime> Anniversary { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Referred by Consultant")]
        public Nullable<int> ReferredConsultantId { get; set; }

        [Display(Name = "Referred By")]
        public Nullable<int> ReferrerId { get; set; }

        [Display(Name = "Referral Amount")]
        public Nullable<decimal> ReferralAmountDue { get; set; }

        [StringLength(50)]
        public string Qualification { get; set; }

        [StringLength(100)]
        public string LocationPreference { get; set; } 

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Hourly Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> HourlyRate { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Salary.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Yearly Salary")]
        public Nullable<decimal> Salary { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for CTC.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> CTC { get; set; }

        [StringLength(100)]
        public string Source { get; set; }

        [Range(0000, 9999, ErrorMessage = "Invalid SSN.")]
        public Nullable<int> SSN { get; set; }

        [Display(Name = "Background Check")]
        public bool BackgroundCheck { get; set; }

        [Display(Name = "Blacklisted")]
        public bool BlackListed { get; set; }

        [Display(Name = "Personal Interview")]
        public bool PersonalInterview { get; set; }

        [Display(Name = "SwitchLane Interviewed")]
        public bool SwitchLaneInterviewed { get; set; }

        [Display(Name = "No of Client Interview")]
        public Nullable<int> NoofClientInterviews { get; set; }

        [Display(Name = "Education Certificates")]
        public bool EducationCertificates { get; set; }

        [Display(Name = "Visa")]
        public bool VisaCopy { get; set; }

        [Display(Name = "Passport")]
        public bool PassPortCopy { get; set; }

        [Display(Name = "Drivers License")]
        public bool LicenseCopy { get; set; }

        [Display(Name = "Photo Id")]
        public bool PhotoId { get; set; }

        [Display(Name = "I-9 Form")]
        public bool I9Form { get; set; }

        [Display(Name = "Green Card Documents")]
        public bool GreenCardDocuments { get; set; }

        [Display(Name = "Pay Stubs")]
        public bool PayStubs { get; set; }

        [Display(Name = "W2")]
        public bool W2Copy { get; set; }        

        [Display(Name = "Visa Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VisaExpirationDate { get; set; }

        [Display(Name = "Last Meeting Date")]
        public Nullable<System.DateTime> LastMeetingDate { get; set; }

        [Display(Name = "Next Meeting Date")]
        public Nullable<System.DateTime> NextMeetingDate { get; set; }

        [Display(Name = "Tier1 Vendor")]
        public Nullable<int> Tier1VendorId { get; set; }

        [Display(Name = "Tier2 Vendor")]
        public Nullable<int> Tier2VendorId { get; set; }

        [Display(Name = "Project End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ProjectEndDate { get; set; }

        [Display(Name = "Client")]
        public Nullable<int> ClientId { get; set; }
        
        public string TrainingAreas { get; set; }

        public bool EAD { get; set; }
        public bool OPT { get; set; }
        
        public bool NDA { get; set; }
        public string Notes { get; set; }
        public string ResidentialStatus { get; set; }

        public string PreviousNotes { get; set; }

        [Display(Name = "Visa Type")]
        [StringLength(10)]
        public string VisaType { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(100)]
        public string Ref1CompanyName { get; set; }

        [Display(Name = "Name of the Person")]
        [StringLength(100)]
        public string Ref1Name { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        public string Ref1Email { get; set; }

        [Display(Name = "Phone")]
        [StringLength(15)]
        public string Ref1Phone { get; set; }

        [Display(Name = "Designation")]
        [StringLength(50)]
        public string Ref1Designation { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(100)]
        public string Ref2CompanyName { get; set; }

        [Display(Name = "Name of the Person")]
        [StringLength(100)]
        public string Ref2Name { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        public string Ref2Email { get; set; }

        [Display(Name = "Phone")]
        [StringLength(15)]
        public string Ref2Phone { get; set; }

        [Display(Name = "Designation")]
        [StringLength(50)]
        public string Ref2Designation { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(100)]
        public string Ref3CompanyName { get; set; }

        [Display(Name = "Name of the Person")]
        [StringLength(100)]
        public string Ref3Name { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        public string Ref3Email { get; set; }

        [Display(Name = "Phone")]
        [StringLength(15)]
        public string Ref3Phone { get; set; }

        [Display(Name = "Designation")]
        [StringLength(50)]
        public string Ref3Designation { get; set; }

        public bool Medical { get; set; }
        public bool Vision { get; set; }
        public bool Dental { get; set; }
        public bool Relocation { get; set; }

        [Display(Name = "401K")]
        public bool FourZeroOneK { get; set; }
        public bool STD { get; set; }
        public bool LTD { get; set; }

        [Display(Name = "Life Insurance")]
        public bool LifeInsurance { get; set; }
    }

    public class ResourceOnBoardModel
    {
        public int ResourceId { get; set; }

        public int RequirementDetailId { get; set; }

        public int RecruiterId { get; set; }

        public int RequirementId { get; set; }

        public int? ProjectDetailId { get; set; }

        [Display(Name = "Project Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Project Start Date is required.")]
        public Nullable<System.DateTime> ProjectStartDate { get; set; }

        [Display(Name = "Project End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Project End Date is required.")]
        public Nullable<System.DateTime> ProjectEndDate { get; set; }

        [Display(Name = "Placement Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Placement Date is required.")]
        public Nullable<System.DateTime> PlacementDate { get; set; }

        [Display(Name = "Final Rate")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Final Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Final Rate is required.")]
        public Nullable<decimal> FinalRate { get; set; }

        [Display(Name = "Buy Rate")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Buy Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> BuyRate { get; set; }

        [Display(Name = "Billing Rate")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Billing Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> BillingRate { get; set; }

    }

    public class SuggestedResourceModel
    {
        public int ResourceId { get; set; }

        public string Name { get; set; }        

        public bool ConsultantList { get; set; }

        public string Availability { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectEndDate { get; set; }
    }
}
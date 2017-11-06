using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecruitmentPortal.Models
{
    public class ResourceMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "DOB is required.")]
        public Nullable<System.DateTime> DOB;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sex is required.")]
        public string Sex;

        [Display(Name = "Resource Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Resource Type is required.")]
        public int ResourceTypeId;     

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]        
        public string PhoneNo2;        

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email2;

        [StringLength(50)]
        public string LinkedInProfile;

        [StringLength(50)]
        public string SkypeId;

        [StringLength(100)]
        public string Availability;
        
        [Display(Name = "Years of Experience")]                
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Years of Experience.")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> YearsOfExperience;

        [Display(Name = "US Experience")]                
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid US Experience.")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<double> USExperience;

        [Display(Name = "Domain Expertise")]        
        [StringLength(500)]
        public string DomainExpertise;

        [Display(Name = "Spouse DOB")]
        public Nullable<System.DateTime> SpouseDOB;

        [Display(Name = "Kid1 DOB")]
        public Nullable<System.DateTime> Kid1DOB;

        [Display(Name = "Kid2 DOB")]
        public Nullable<System.DateTime> Kid2DOB;

        [Display(Name = "No of Kids")]
        [Range(0, 10, ErrorMessage = "Invalid data for No of Kids.")]
        public Nullable<int> NoofKids;

        [Display(Name="Marital Status")]
        public string MaritalStatus;

        [Display(Name = "Referred by Consultant")]
        public Nullable<int> ReferredConsultantId;

        [Display(Name = "Referred By")]
        public Nullable<int> ReferrerId;

        [Display(Name = "Referral Amount")]
        public Nullable<decimal> ReferralAmountDue;

        [StringLength(50)]
        public string Qualification;

        [StringLength(100)]
        public string LocationPreference;        
                
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Hourly Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hourly Rate")]
        public Nullable<decimal> HourlyRate;

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Salary.")]        
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Salary;

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for CTC.")]        
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> CTC;

        [StringLength(100)]
        public string Source;
                
        [Range(0000, 9999, ErrorMessage = "Invalid SSN.")]
        public Nullable<int> SSN;

        [Display(Name = "Background Check")]
        public bool BackgroundCheck;

        [Display(Name = "Blacklisted")]
        public bool BlackListed;        

        [Display(Name = "Personal Interview")]
        public bool PersonalInterview;

        [Display(Name = "SwitchLane Interviewed")]
        public bool SwitchLaneInterviewed;

        [Display(Name = "No of Client Interview")]
        public Nullable<int> NoofClientInterviews;

        [Display(Name = "Education Certificates")]
        public bool EducationCertificates;

        [Display(Name = "Visa")]
        public bool VisaCopy;

        [Display(Name = "Passport")]
        public bool PassPortCopy;

        [Display(Name = "Driver's License")]
        public bool LicenseCopy;

        [Display(Name = "Photo Id")]
        public bool PhotoId;

        [Display(Name = "I-9 Form")]
        public bool I9Form;

        [Display(Name = "Green Card Documents")]
        public bool GreenCardDocuments;

        [Display(Name = "Pay Stubs")]
        public bool PayStubs;

        [Display(Name = "W2")]
        public bool W2Copy;        

        [Display(Name = "Visa Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VisaExpirationDate;

        [Display(Name = "Last Meeting Date")]
        public Nullable<System.DateTime> LastMeetingDate;

        [Display(Name = "Next Meeting Date")]
        public Nullable<System.DateTime> NextMeetingDate;

        [Display(Name = "Tier1 Vendor")]
        public Nullable<int> Tier1VendorId;

        [Display(Name = "Tier2 Vendor")]
        public Nullable<int> Tier2VendorId;

        [Display(Name = "Project End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ProjectEndDate;

        [Display(Name = "Client")]
        public Nullable<int> ClientId;

        [Display(Name = "401k")]
        public bool FourZeroOneK;

        [Display(Name = "Life Insurance")]
        public bool LifeInsurance;

        [Display(Name = "Residential Status")]
        public string ResidentialStatus;
        
        [Display(Name = "Visa Type")]
        public string VisaType;
    }

    public class ResourceDocumentMetadata
    {
        [Display(Name = "Document Type")]
        public int DocumentTypeId;

        [Display(Name = "File Name")]
        public string FileName;

        [Required(AllowEmptyStrings=false, ErrorMessage="Description is required.")]
        public string Description;
    }

    public class ResourceSkillSetMetadata
    {
        [Display(Name = "Skill Set")]
        public int SkillSetId;

        [Display(Name = "Self Rating")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Self Rating is required.")]
        public Nullable<int> SelfRating;

        [Display(Name = "Self Comments")]
        public string SelfComments;

        [Display(Name = "Recruiter Rating")]        
        public Nullable<int> RecruiterRating;

        [Display(Name = "Recruiter Comments")]
        public string RecruiterComments;
    }

    public class InterviewMetadata
    {
        [Display(Name = "Resource")]        
        public int ResourceId;

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNo;

        [StringLength(500)]
        public string MeetingLink;

        [StringLength(500)]
        public string Feedback;

        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required.")]
        public string Status;
                
        [Display(Name = "Interviewer")]
        public Nullable<int> InterviewerId;
                
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Fee.")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]       
        public Nullable<decimal> Fee;

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public System.DateTime StartTime;

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public System.DateTime EndTime;

        [Display(Name = "Requirement")]        
        public Nullable<int> RequirementId;

        [Display(Name = "Client")]
        public Nullable<int> ClientId;
        
        [Display(Name = "Interview Type")]
        public int InterviewType;
    }

    public class InterviewDocumentMetadata
    {
        [Display(Name = "Document Type")]
        public int DocumentTypeId;

        [Display(Name = "File Name")]
        public string FileName;
    }

    public class InterviewDetailMetadata
    {
        [Display(Name = "Skill Set")]
        public int SkillSetId;
    }

    public class InterviewerMetadata
    {
        [Display(Name = "Hourly Rate")]
        public Nullable<decimal> HourlyRate;

        [Display(Name = "Fixed Fee")]
        public Nullable<decimal> FixedFee;        
    }

    public class ClientMetadata
    {
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Phone No")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No is required.")]
        public string PhoneNo;

        [StringLength(100)]
        [Display(Name = "Billing Contact")]
        public string BillingContact;

        [Display(Name = "Black Listed")]
        public bool BlackListed;

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        public string Email;

        [StringLength(250)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string Name;

        [StringLength(50)]
        public string Account;

        [StringLength(50)]
        public string Address1;

        [StringLength(50)]
        public string Address2;

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required.")]
        public string City;

        [Required(AllowEmptyStrings = false, ErrorMessage = "State is required.")]
        public string State;

        [StringLength(50)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip.")]
        public string ZipCode;
    }

    public class ClientDocumentMetadata
    {
        [Display(Name = "Document Type")]
        public int DocumentTypeId;

        [Display(Name = "File Name")]
        public string FileName;
    }

    public class VendorMetadata
    {
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Phone No")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No is required.")]
        public string PhoneNo;

        [StringLength(100)]
        [Display(Name = "Billing Contact")]
        public string BillingContact;

        [Display(Name = "Black Listed")]
        public bool BlackListed;

        [Display(Name = "Insurance Certificates")]
        public bool InsuranceCertificates;

        [StringLength(250)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string Name;
        
        [StringLength(50)]
        public string Address1;

        [StringLength(50)]
        public string Address2;

        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required.")]
        public string City;

        [Required(AllowEmptyStrings = false, ErrorMessage = "State is required.")]
        public string State;

        [StringLength(50)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip.")]
        public string ZipCode;
    }

    public class VendorDocumentMetadata
    {
        [Display(Name = "Document Type")]
        public int DocumentTypeId;

        [Display(Name = "File Name")]
        public string FileName;
    }

    public class RequirementMetadata
    {
        [StringLength(10)]
        [Display(Name = "Job Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Job Number is required.")]
        public string JobNumber;

        [StringLength(500)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required.")]
        public string Description;

        [StringLength(100)]
        public string Location;

        [Display(Name = "Tier1 Client")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tier1 Client is required.")]
        public Nullable<int> Tier1ClientId;

        [Display(Name = "Tier2 Client")]
        public Nullable<int> Tier2ClientId;

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Hourly Buy Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> HourlyBuyRate;

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for Hourly Billing Rate.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> HourlyBillingRate;
        
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount for One Time Fee.")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> OneTimeFee;

        [StringLength(250)]
        public string ReasonNotClosed;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required.")]
        public Nullable<int> Status;

        [Display(Name = "Recruiter")]
        public Nullable<int> RecruiterId;

        [Display(Name = "Account Manager")]
        public Nullable<int> AccountManagerId;

        [Display(Name = "Job Type")]
        public int JobTypeId;

        [Display(Name = "Job Title")]
        [StringLength(100)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Job Title is required.")]
        public string JobTitle;
    }

    public class RequirementDetailMetadata
    {
        [Display(Name = "Referred By")]
        public int RecruiterId;

        [Display(Name = "Consultant")]
        public Nullable<int> ResourceId;

        [Display(Name = "Referred To Client")]
        public bool ReferredToClient;

        [Display(Name = "Is Candidate Selected")]
        public bool CandidateSelected;       
        
    }

    public class RecruiterTargetMetadata
    {
        [Display(Name = "Commission Type")]
        public int CommissionTypeId;

        [Display(Name = "Target Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TargetDate;

        [Display(Name = "Target Type")]
        public int TargetType;

        [Display(Name = "Target Count")]
        public int TargetCount;
    }

    public class SkillSetMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name;
    }

    public class ReferrerMetadata
    {
        [Display(Name = "First Name")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required.")]
        public string FirstName;

        [Display(Name = "Last Name")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required.")]
        public string LastName;

        [Display(Name = "Middle Initial")]
        [StringLength(50)]
        public string MiddleInitial;

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        public string Email;

        [Display(Name = "Phone No")]
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No is required.")]
        public string PhoneNo;
    }

    public class CommissionTypeMetadata
    {
        [StringLength(100)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string Name;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
        public decimal Value;

        [Display(Name = "Is Percent")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public bool IsPercentage;
    }

    public class DocumentTypeMetadata
    {
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string Name;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Document Type is required.")]
        public Nullable<int> DocumentGroup;         
    }
}
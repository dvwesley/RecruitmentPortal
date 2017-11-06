using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using RecruitmentPortal.Extensions;

namespace RecruitmentPortal.Models
{
    public class Utility
    {
        public List<SelectListItem> States = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };

        public List<SelectListItem> Hours = new List<SelectListItem>()
        {   
              new SelectListItem() { Text="0", Value="0"},
              new SelectListItem() { Text="1", Value="1"},
              new SelectListItem() { Text="2", Value="2"},
              new SelectListItem() { Text="3", Value="3"},
              new SelectListItem() { Text="4", Value="4"},
              new SelectListItem() { Text="5", Value="5"},
              new SelectListItem() { Text="6", Value="6"},
              new SelectListItem() { Text="7", Value="7"},
              new SelectListItem() { Text="8", Value="8"},
              new SelectListItem() { Text="9", Value="9"},
              new SelectListItem() { Text="10", Value="10"},
              new SelectListItem() { Text="11", Value="11"},
              new SelectListItem() { Text="12", Value="12"},
              new SelectListItem() { Text="13", Value="13"},
              new SelectListItem() { Text="14", Value="14"},
              new SelectListItem() { Text="15", Value="15"},
              new SelectListItem() { Text="16", Value="16"},
              new SelectListItem() { Text="17", Value="17"},
              new SelectListItem() { Text="18", Value="18"},
              new SelectListItem() { Text="19", Value="19"},
              new SelectListItem() { Text="20", Value="20"},
              new SelectListItem() { Text="21", Value="21"},
              new SelectListItem() { Text="22", Value="22"},
              new SelectListItem() { Text="23", Value="23"}
        };

        public List<SelectListItem> Minutes = new List<SelectListItem>()
        {   
              new SelectListItem() { Text="0", Value="0"},
              new SelectListItem() { Text="1", Value="1"},
              new SelectListItem() { Text="2", Value="2"},
              new SelectListItem() { Text="3", Value="3"},
              new SelectListItem() { Text="4", Value="4"},
              new SelectListItem() { Text="5", Value="5"},
              new SelectListItem() { Text="6", Value="6"},
              new SelectListItem() { Text="7", Value="7"},
              new SelectListItem() { Text="8", Value="8"},
              new SelectListItem() { Text="9", Value="9"},
              new SelectListItem() { Text="10", Value="10"},
              new SelectListItem() { Text="11", Value="11"},
              new SelectListItem() { Text="12", Value="12"},
              new SelectListItem() { Text="13", Value="13"},
              new SelectListItem() { Text="14", Value="14"},
              new SelectListItem() { Text="15", Value="15"},
              new SelectListItem() { Text="16", Value="16"},
              new SelectListItem() { Text="17", Value="17"},
              new SelectListItem() { Text="18", Value="18"},
              new SelectListItem() { Text="19", Value="19"},
              new SelectListItem() { Text="20", Value="20"},
              new SelectListItem() { Text="21", Value="21"},
              new SelectListItem() { Text="22", Value="22"},
              new SelectListItem() { Text="23", Value="23"},
              new SelectListItem() { Text="24", Value="24"},
              new SelectListItem() { Text="25", Value="25"},
              new SelectListItem() { Text="26", Value="26"},
              new SelectListItem() { Text="27", Value="27"},
              new SelectListItem() { Text="28", Value="28"},
              new SelectListItem() { Text="29", Value="29"},
              new SelectListItem() { Text="30", Value="30"},
              new SelectListItem() { Text="31", Value="31"},
              new SelectListItem() { Text="32", Value="32"},
              new SelectListItem() { Text="33", Value="33"},
              new SelectListItem() { Text="34", Value="34"},
              new SelectListItem() { Text="35", Value="35"},
              new SelectListItem() { Text="36", Value="36"},
              new SelectListItem() { Text="37", Value="37"},
              new SelectListItem() { Text="38", Value="38"},
              new SelectListItem() { Text="39", Value="39"},
              new SelectListItem() { Text="40", Value="40"},
              new SelectListItem() { Text="41", Value="41"},
              new SelectListItem() { Text="42", Value="42"},
              new SelectListItem() { Text="43", Value="43"},
              new SelectListItem() { Text="44", Value="44"},
              new SelectListItem() { Text="45", Value="45"},
              new SelectListItem() { Text="46", Value="46"},
              new SelectListItem() { Text="47", Value="47"},
              new SelectListItem() { Text="48", Value="48"},
              new SelectListItem() { Text="49", Value="49"},
              new SelectListItem() { Text="50", Value="50"},
              new SelectListItem() { Text="51", Value="51"},
              new SelectListItem() { Text="52", Value="52"},
              new SelectListItem() { Text="53", Value="53"},
              new SelectListItem() { Text="54", Value="54"},
              new SelectListItem() { Text="55", Value="55"},
              new SelectListItem() { Text="56", Value="56"},
              new SelectListItem() { Text="57", Value="57"},
              new SelectListItem() { Text="58", Value="58"},
              new SelectListItem() { Text="59", Value="59"}
        };

        public List<SelectListItem> InterviewStatus = new List<SelectListItem>()
        {   
            new SelectListItem() { Text="Cancelled", Value="C"},  
            new SelectListItem() { Text="Completed", Value="D"},              
            new SelectListItem() { Text="Scheduled", Value="S"}
        };

        public string[] DocumentExtensions = new[] { ".doc", ".docx", ".jpg", ".jpeg", ".gif", ".png", ".pdf", ".mp4" };

        public static List<SelectListItem> GetRatings()
        {
            List<SelectListItem> ratings = new List<SelectListItem>();
            SelectListItem item = null;
            for (int i = 1; i <= int.Parse(ConfigurationManager.AppSettings["RatingScale"]); i++)
            {
                item = new SelectListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                ratings.Add(item);
            }
            return ratings;
        }

        public List<SelectListItem> Priority = new List<SelectListItem>()
        {   
            new SelectListItem() { Text="Low", Value="1"},  
            new SelectListItem() { Text="Medium", Value="2"},
            new SelectListItem() { Text="High", Value="3"},
            new SelectListItem() { Text="Critical", Value="4"}
        };

        public List<SelectListItem> RequirementStatus = new List<SelectListItem>()
        {   
            new SelectListItem() { Text="New", Value="1"},  
            new SelectListItem() { Text="On Hold", Value="2"},  
            new SelectListItem() { Text="Cancelled", Value="3"},
            new SelectListItem() { Text="Fulfilled", Value="4"},
            new SelectListItem() { Text="Closed", Value="5"}            
        };

        public List<SelectListItem> JobType = new List<SelectListItem>()
        {   
            new SelectListItem() { Text="Consultant", Value="1"},  
            new SelectListItem() { Text="Employee", Value="2"},
            new SelectListItem() { Text="Part Time", Value="3"}            
        };

        public List<SelectListItem> Years = new List<SelectListItem>()
        {
            new SelectListItem() { Text = DateTime.Now.Year.ToString(), Value = DateTime.Now.Year.ToString() },
            new SelectListItem() { Text = DateTime.Now.AddYears(1).Year.ToString(), Value = DateTime.Now.AddYears(1).Year.ToString() },
            new SelectListItem() { Text = DateTime.Now.AddYears(2).Year.ToString(), Value = DateTime.Now.AddYears(2).Year.ToString() }
        };

        public List<SelectListItem> TargetType = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Weekly", Value = "1" },
            new SelectListItem() { Text = "Monthly", Value = "2" },
            new SelectListItem() { Text = "Quarterly", Value = "3" },
            new SelectListItem() { Text = "Yearly", Value = "4" }
        };

        public List<SelectListItem> VisaType = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "H1B", Value = "H1B" },
            new SelectListItem() { Text = "L1A", Value = "L1A" },
            new SelectListItem() { Text = "L1B", Value = "L1B" }                        
        };

        public List<SelectListItem> Availability = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Immediately", Value = "Immediately" },
            new SelectListItem() { Text = "2 weeks", Value = "2 weeks" },
            new SelectListItem() { Text = "1 month", Value = "1 month" },
            new SelectListItem() { Text = "3 months", Value = "3 months" },
            new SelectListItem() { Text = "6+ months", Value = "6+ months" },
            new SelectListItem() { Text = "Other", Value = "Other" },
        };

        public IEnumerable<SelectListItem> GetDocumentGroups()
        {
            var documentGroups = Enum.GetValues(typeof(DocumentGroup)).Cast<DocumentGroup>().Select(v => new SelectListItem
            {
                Text = EnumHelper<DocumentGroup>.GetDisplayValue(v) ,
                Value = ((int)v).ToString()
            });
            return documentGroups.OrderBy(d => d.Text);
        }
    }

    public enum DocumentTypes
    {
        Visa = 1,
        GreenCard = 2,
        PassPort = 3,
        License = 4,
        PhotoIdentity = 5,
        I9Form = 6,
        Paystub = 7,
        W2 = 8,
        EducationalCertificates = 9,
        Resume = 10,
        Invoices = 11,
        Contracts = 12,
        InsuranceCertificates = 13,
        InterviewVideo = 14,
        InterviewFeedback = 15,
        InterviewQnsExcerciseFiles = 16,
        ProfilePicture = 17,
        EAD = 18,
        Agreement = 19,
        W9 = 20,
        VendorInsuranceCertificates = 21,
        VendorContracts = 22,
        PurchaseOrder = 23,
        ImmigrationDocuments = 24,
        OPT = 25,
        NDA = 26
    }

    public enum DocumentGroup
    {
        [Display(Name = "Resource Document")]       
        ResourceDocument = 1,
        [Display(Name = "Interview Document")]       
        InterviewDocument = 2,
        [Display(Name = "Client Document")]       
        ClientDocument = 3,
        [Display(Name = "Vendor Document")]       
        VendorDocument = 4
    }    
}
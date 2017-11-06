using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace RecruitmentPortal.Models
{
    public class InterviewerViewModel
    {
        public int InterviewerId { get; set; }        

        [StringLength(50)]
        [Required (AllowEmptyStrings=false, ErrorMessage = "First Name is required.")]
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

        [Display(Name = "Hourly Rate")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> HourlyRate { get; set; }

        [Display(Name = "Fixed Fee")]
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> FixedFee { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Summary is required.")]
        public string Summary { get; set; }

        [Display(Name = "Available on Sundays")]
        public bool AvailableonSundays { get; set; }

        [Display(Name = "Available on Mondays")]
        public bool AvailableonMondays { get; set; }

        [Display(Name = "Available on Tuesdays")]
        public bool AvailableonTuesdays { get; set; }

        [Display(Name = "Available on Wednesdays")]
        public bool AvailableonWednesdays { get; set; }

        [Display(Name = "Available on Thursdays")]
        public bool AvailableonThursdays { get; set; }

        [Display(Name = "Available on Fridays")]
        public bool AvailableonFridays { get; set; }

        [Display(Name = "Available on Saturdays")]
        public bool AvailabaleonSaturdays { get; set; }

        [Display(Name = "Availability")]
        public string Availability
        {
            get
            {
                StringBuilder available = new StringBuilder();
                available.Append(AvailableonMondays ? " Mon" : "");
                available.Append(AvailableonTuesdays ? " Tue" : "");
                available.Append(AvailableonWednesdays ? " Wed" : "");
                available.Append(AvailableonThursdays ? " Thu" : "");
                available.Append(AvailableonFridays ? " Fri" : "");
                available.Append(AvailabaleonSaturdays ? " Sat" : "");
                available.Append(AvailableonSundays ? " Sun" : "");
                return available.ToString();
            }
        }

        public string Name 
        { 
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string CityStateZipDisplay
        {
            get
            {
                StringBuilder returnValue = new StringBuilder();
                if (City != null && City.Length > 0)
                {
                    returnValue.Append(City);
                }
                if (State != null && State.Length > 0)
                {
                    returnValue.Append((returnValue.Length > 0 ? ", " : "") + State);
                }
                if (Zipcode != null && Zipcode.Length > 0)
                {
                    returnValue.Append((returnValue.Length > 0 ? " - " : "") + Zipcode);
                }
                return returnValue.ToString();
            }
        }

        public string SelectedSkillSetCSV { get; set; }

        public IEnumerable<Interview> Interviews { get; set; }

        public IEnumerable<InterviewerSkillSet> InterviewerSkillSets { get; set; }
    }
}
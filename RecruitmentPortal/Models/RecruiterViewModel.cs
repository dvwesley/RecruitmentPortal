using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentPortal.Models
{
    public class RecruiterViewModel
    {
        public int RecruiterId { get; set; }

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

        [StringLength(100)]
        public string Specialty { get; set; }

        [StringLength(250)]
        public string AreasOfImprovment { get; set; }

        [StringLength(250)]
        public string CareerPath { get; set; }

        [StringLength(250)]
        public string Communities { get; set; }
                
        [DisplayFormat(DataFormatString = "${0:n2}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Salary { get; set; }

        [Display(Name = "Manager")]
        public Int32? ManagerId { get; set; }

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

        /*
        public static RecruiterViewModel Initialize()
        {
            RecruiterViewModel model = new RecruiterViewModel();
            model.RecruiterTarget = new RecruiterTarget();
            return model;
        }
         */ 
    }
}
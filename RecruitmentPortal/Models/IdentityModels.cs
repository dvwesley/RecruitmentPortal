using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace RecruitmentPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
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
        public string Zipcode { get; set; }

        [StringLength(50)]
        public string PhoneNo { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public DateTime? UpdatedTimestamp{ get; set; }

        public DateTime? LastLoginDate { get; set; }

        public Int32? NoofLogins { get; set; }

        public Int32? NoofAttempts { get; set; }                       
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }
        
    }
}
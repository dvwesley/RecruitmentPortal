namespace RecruitmentPortal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using RecruitmentPortal.Models;
    using RecruitmentPortal.Common;

    internal sealed class Configuration : DbMigrationsConfiguration<RecruitmentPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RecruitmentPortal.Models.ApplicationDbContext";
        }

        protected override void Seed(RecruitmentPortal.Models.ApplicationDbContext context)
        {            
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string[] roles = new[] { "Admin", "Recruiter", "Recruit Manager", "Client", "Vendor", "Consultant", "Interviewer" };
            IdentityResult roleResult;
            // Check to see if Role Exists, if not create it
            foreach (string roleName in roles)
            {
                if (!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));                    
                }
            }

            var user = UserManager.FindByName("superadmin@gmail.com");
            if (user == null)
            {
                var suser = new ApplicationUser
                {
                    UserName = "superadmin@gmail.com",
                    FirstName = "super",
                    LastName = "admin",
                    Active = true,
                    CreatedTimestamp = DateTime.Now
                };
                var userValidator = UserManager.UserValidator as UserValidator<ApplicationUser>;
                userValidator.AllowOnlyAlphanumericUserNames = false;
                IdentityResult result = UserManager.Create(suser, "rptest");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(suser.Id, "Admin");
                }
            }
        }
    }
}

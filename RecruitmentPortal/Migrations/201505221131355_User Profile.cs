namespace RecruitmentPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "MiddleInitial", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Address1", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Address2", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "State", c => c.String(maxLength: 2));
            AddColumn("dbo.AspNetUsers", "Zipcode", c => c.String(maxLength: 10));
            AddColumn("dbo.AspNetUsers", "PhoneNo", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "CreatedTimestamp", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "UpdatedTimestamp", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastLoginDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "NoofLogins", c => c.Int());
            AddColumn("dbo.AspNetUsers", "NoofAttempts", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NoofAttempts");
            DropColumn("dbo.AspNetUsers", "NoofLogins");
            DropColumn("dbo.AspNetUsers", "LastLoginDate");
            DropColumn("dbo.AspNetUsers", "UpdatedTimestamp");
            DropColumn("dbo.AspNetUsers", "CreatedTimestamp");
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "PhoneNo");
            DropColumn("dbo.AspNetUsers", "Zipcode");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address2");
            DropColumn("dbo.AspNetUsers", "Address1");
            DropColumn("dbo.AspNetUsers", "MiddleInitial");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}

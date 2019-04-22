namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VolunteerDone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VolunteerDoctors", "IsDoctorDone", c => c.String());
            AddColumn("dbo.Volunteers", "IsVolunteerDone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Volunteers", "IsVolunteerDone");
            DropColumn("dbo.VolunteerDoctors", "IsDoctorDone");
        }
    }
}

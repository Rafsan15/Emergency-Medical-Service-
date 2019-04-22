namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VolunteerLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VolunteerDoctors", "CurrentLocation", c => c.String());
            AddColumn("dbo.VolunteerDoctors", "Area", c => c.String());
            AddColumn("dbo.Volunteers", "Area", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Volunteers", "Area");
            DropColumn("dbo.VolunteerDoctors", "Area");
            DropColumn("dbo.VolunteerDoctors", "CurrentLocation");
        }
    }
}

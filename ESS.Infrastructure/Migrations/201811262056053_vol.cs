namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VolunteerDoctors", "WorkShopStatus");
            DropColumn("dbo.Volunteers", "WorkShopStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Volunteers", "WorkShopStatus", c => c.String());
            AddColumn("dbo.VolunteerDoctors", "WorkShopStatus", c => c.String());
        }
    }
}

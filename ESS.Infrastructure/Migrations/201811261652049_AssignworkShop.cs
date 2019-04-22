namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignworkShop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "WorkShopId", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "IsWorkShop", c => c.String());
            AddColumn("dbo.VolunteerDoctors", "WorkShopStatus", c => c.String());
            AddColumn("dbo.Volunteers", "WorkShopStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Volunteers", "WorkShopStatus");
            DropColumn("dbo.VolunteerDoctors", "WorkShopStatus");
            DropColumn("dbo.Notifications", "IsWorkShop");
            DropColumn("dbo.Notifications", "WorkShopId");
        }
    }
}

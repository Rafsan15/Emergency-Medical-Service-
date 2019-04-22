namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkshopTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkShops", "WorkShopDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkShops", "WorkShopDate");
        }
    }
}

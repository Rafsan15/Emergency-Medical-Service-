namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkshopDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkShops", "WorkShopTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkShops", "WorkShopTime", c => c.DateTime(nullable: false));
        }
    }
}

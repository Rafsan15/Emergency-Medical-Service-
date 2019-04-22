namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workShopUserisActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkShops", "IsFinish", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkShops", "IsFinish");
        }
    }
}

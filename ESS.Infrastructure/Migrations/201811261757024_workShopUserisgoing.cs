namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workShopUserisgoing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignWorkShops", "IsGoing", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignWorkShops", "IsGoing");
        }
    }
}

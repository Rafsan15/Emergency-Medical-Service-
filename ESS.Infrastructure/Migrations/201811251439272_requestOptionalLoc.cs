namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requestOptionalLoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestForServices", "OptionalLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestForServices", "OptionalLocation");
        }
    }
}

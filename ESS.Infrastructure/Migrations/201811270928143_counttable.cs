namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class counttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestCounts",
                c => new
                    {
                        CountId = c.Int(nullable: false, identity: true),
                        PreviousCount = c.Int(nullable: false),
                        NewCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RequestCounts");
        }
    }
}

namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Workshop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignWorkShops",
                c => new
                    {
                        AssignWorkShopId = c.Int(nullable: false, identity: true),
                        WorkShopId = c.Int(nullable: false),
                        VolunteerId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignWorkShopId);
            
            CreateTable(
                "dbo.WorkShops",
                c => new
                    {
                        WorkShopId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Venue = c.String(),
                        Details = c.String(),
                        Phone = c.Int(nullable: false),
                        WorkShopTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WorkShopId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkShops");
            DropTable("dbo.AssignWorkShops");
        }
    }
}

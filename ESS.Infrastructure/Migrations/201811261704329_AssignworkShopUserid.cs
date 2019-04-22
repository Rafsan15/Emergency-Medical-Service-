namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignworkShopUserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignWorkShops", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.AssignWorkShops", "VolunteerId");
            DropColumn("dbo.AssignWorkShops", "DoctorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssignWorkShops", "DoctorId", c => c.Int(nullable: false));
            AddColumn("dbo.AssignWorkShops", "VolunteerId", c => c.Int(nullable: false));
            DropColumn("dbo.AssignWorkShops", "UserId");
        }
    }
}

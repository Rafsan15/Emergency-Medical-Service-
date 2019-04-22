namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsDeliver", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsDeliver");
        }
    }
}

namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userGender : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Gender", c => c.String());
        }
    }
}

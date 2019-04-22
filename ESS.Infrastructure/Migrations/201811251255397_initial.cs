namespace ESS.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedRequests",
                c => new
                    {
                        AssignedId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        VolunteerId = c.Int(nullable: false),
                        VolunteerDoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignedId)
                .ForeignKey("dbo.RequestForServices", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.RequestForServices",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Phone = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        RequesTime = c.DateTime(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        IsFinish = c.String(),
                        IsFinishDoctor = c.String(),
                    })
                .PrimaryKey(t => t.RequestId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        CardNumber = c.String(),
                        DonationDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.DonationId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Designation = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 255),
                        Password = c.String(),
                        Address = c.String(),
                        Phone = c.String(maxLength: 25),
                        Gender = c.String(),
                        Photo = c.String(),
                        UserType = c.String(),
                        DOB = c.DateTime(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Phone, unique: true);
            
            CreateTable(
                "dbo.VolunteerDoctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        SpecialDomain = c.String(),
                        HospitalName = c.String(),
                        IsActive = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerId = c.Int(nullable: false, identity: true),
                        IsActive = c.String(),
                        IsApprove = c.String(),
                        JobDetails = c.String(),
                        CurrentLocation = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VolunteerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "UserId", "dbo.Users");
            DropForeignKey("dbo.AssignedRequests", "RequestId", "dbo.RequestForServices");
            DropIndex("dbo.Users", new[] { "Phone" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.AssignedRequests", new[] { "RequestId" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.VolunteerDoctors");
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.Donations");
            DropTable("dbo.RequestForServices");
            DropTable("dbo.AssignedRequests");
        }
    }
}

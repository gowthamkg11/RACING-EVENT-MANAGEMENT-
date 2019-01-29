namespace NUS.TheAmagingRace.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NEWUpadate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventDescription = c.String(),
                        EventCountry = c.String(nullable: false),
                        EventCity = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                        CreatedBy = c.String(),
                        LastModifiedBy = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.PitStop",
                c => new
                    {
                        PitStopID = c.Int(nullable: false, identity: true),
                        PitStopName = c.String(nullable: false),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        SequenceNumber = c.Int(nullable: false),
                        Address = c.String(),
                        CreatedBy = c.String(),
                        LastModifiedBy = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Event_EventID = c.Int(nullable: false),
                        Staff_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PitStopID)
                .ForeignKey("dbo.Event", t => t.Event_EventID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Staff_Id)
                .Index(t => t.Event_EventID)
                .Index(t => t.Staff_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DisplayName = c.String(),
                        ImagePath = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Distance = c.String(),
                        Time = c.String(),
                        Position = c.Int(nullable: false),
                        NextPitStop = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        CreatedBy = c.String(),
                        LastModifiedBy = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Event_EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Event", t => t.Event_EventID, cascadeDelete: true)
                .Index(t => t.Event_EventID);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Distance = c.String(),
                        TimeCovered = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedBy = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Team", "Event_EventID", "dbo.Event");
            DropForeignKey("dbo.PitStop", "Staff_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PitStop", "Event_EventID", "dbo.Event");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Team", new[] { "Event_EventID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PitStop", new[] { "Staff_Id" });
            DropIndex("dbo.PitStop", new[] { "Event_EventID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Location");
            DropTable("dbo.Team");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PitStop");
            DropTable("dbo.Event");
        }
    }
}

namespace NUS.TheAmagingRace.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberName = c.String(),
                        Event_EventID = c.Int(),
                        Team_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Event", t => t.Event_EventID)
                .ForeignKey("dbo.Team", t => t.Team_TeamID)
                .Index(t => t.Event_EventID)
                .Index(t => t.Team_TeamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Member", "Team_TeamID", "dbo.Team");
            DropForeignKey("dbo.Member", "Event_EventID", "dbo.Event");
            DropIndex("dbo.Member", new[] { "Team_TeamID" });
            DropIndex("dbo.Member", new[] { "Event_EventID" });
            DropTable("dbo.Member");
        }
    }
}

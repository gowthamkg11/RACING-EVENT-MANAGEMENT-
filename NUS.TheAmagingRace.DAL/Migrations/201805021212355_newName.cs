namespace NUS.TheAmagingRace.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Member", "Team_TeamID", "dbo.Team");
            DropIndex("dbo.Member", new[] { "Team_TeamID" });
            AlterColumn("dbo.Member", "Team_TeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.Member", "Team_TeamID");
            AddForeignKey("dbo.Member", "Team_TeamID", "dbo.Team", "TeamID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Member", "Team_TeamID", "dbo.Team");
            DropIndex("dbo.Member", new[] { "Team_TeamID" });
            AlterColumn("dbo.Member", "Team_TeamID", c => c.Int());
            CreateIndex("dbo.Member", "Team_TeamID");
            AddForeignKey("dbo.Member", "Team_TeamID", "dbo.Team", "TeamID");
        }
    }
}

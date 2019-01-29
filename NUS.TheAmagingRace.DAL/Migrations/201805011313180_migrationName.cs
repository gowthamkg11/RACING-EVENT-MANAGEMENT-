namespace NUS.TheAmagingRace.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isAssigned");
        }
    }
}

namespace NUS.TheAmagingRace.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NUS.TheAmagingRace.DAL.TARDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NUS.TheAmagingRace.DAL.TARDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            Event v = context.Events.Where(m => m.EventID == 1).FirstOrDefault();

            Team teamA = new Team { TeamName = "US Peters", Event = v };
            Team teamB = new Team { TeamName = "Dubai Queens", Event = v };



            context.Teams.AddOrUpdate(teamA);

            context.Teams.AddOrUpdate(teamB);

        }
    }
}

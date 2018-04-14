using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace NUS.TheAmagingRace.DAL
{
    public class TARDBContext : IdentityDbContext<TARUser>
    {

        public TARDBContext()
        : base("TARDBConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<TARDBContext>(new TestDataInitializer());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PitStop> PitStops { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            //modelBuilder.Entity<TARUser>()
            // .Property(c => c.Id)
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TARUser>().Property(f => f.CreatedAt).HasColumnType("datetime2");
            modelBuilder.Entity<TARUser>().Property(f => f.LastModifiedAt).HasColumnType("datetime2");

            //modelBuilder.Entity<Event>()
            //    .Property(c => c.EventID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Event>().Property(f => f.CreatedAt).HasColumnType("datetime2");
            modelBuilder.Entity<Event>().Property(f => f.LastModifiedAt).HasColumnType("datetime2");

            //modelBuilder.Entity<Team>()
            //   .Property(c => c.TeamID)
            //   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Team>().Property(f => f.CreatedAt).HasColumnType("datetime2");
            modelBuilder.Entity<Team>().Property(f => f.LastModifiedAt).HasColumnType("datetime2");


            //modelBuilder.Entity<Location>()
            //    .Property(c => c.LocationId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TARUser>().Property(f => f.CreatedAt).HasColumnType("datetime2");
            modelBuilder.Entity<TARUser>().Property(f => f.LastModifiedAt).HasColumnType("datetime2");

            //modelBuilder.Entity<PitStop>()
            //    .Property(c => c.PitStopID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PitStop>().Property(f => f.CreatedAt).HasColumnType("datetime2");
            modelBuilder.Entity<PitStop>().Property(f => f.LastModifiedAt).HasColumnType("datetime2");


            base.OnModelCreating(modelBuilder);



        }
        public override int SaveChanges()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.Now;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedAt = now;
                added.LastModifiedAt = now;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.LastModifiedAt = now;
            }

            return base.SaveChanges();
        }

        private string DatabaseGenerated(object identity)
        {
            throw new NotImplementedException();
        }

        public static TARDBContext Create()
        {
            return new TARDBContext();
        }


    }
}

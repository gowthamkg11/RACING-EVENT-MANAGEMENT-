using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace NUS.TheAmagingRace.EF
{
    public class TARDBContext: IdentityDbContext<TARUser>
    {
        public TARDBContext()
        : base("TARDBConnection")
        {
            Database.SetInitializer<TARDBContext>(new TestDataInitializer());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<PitStop> PitStops { get; set; }
        public DbSet<Location> Locations { get; set; }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
          
         
            //modelBuilder.Entity<TARUser>().Map(c =>
            //{
            //    c.ToTable("User");
            //    c.Property(p => p.Id).HasColumnName("UserId");
            //    c.Properties(p => new
            //    {
            //        p.AccessFailedCount,
            //        p.Email,
            //        p.EmailConfirmed,
            //        p.PasswordHash,
            //        p.PhoneNumber,
            //        p.PhoneNumberConfirmed,
            //        p.TwoFactorEnabled,
            //        p.SecurityStamp,
            //        p.LockoutEnabled,
            //        p.LockoutEndDateUtc,
            //        p.UserName,
            //        p.IsStaff
            //    });
            //}).HasKey(c => c.Id);
            //modelBuilder.Entity<TARUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            //modelBuilder.Entity<TARUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            //modelBuilder.Entity<TARUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

           
            base.OnModelCreating(modelBuilder);



        }
        public static TARDBContext Create()
        {
            return new TARDBContext();
        }
        

    }
}

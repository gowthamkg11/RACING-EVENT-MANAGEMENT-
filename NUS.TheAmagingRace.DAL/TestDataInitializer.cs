using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUS.TheAmagingRace.DAL
{
    public class TestDataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TARDBContext>
    {
        protected  async override void Seed(TARDBContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Staff" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Member" };

                manager.Create(role);
            }
            if (!context.Users.Any(r => r.UserName == "Admin@Admin.com"))
            {
                var store = new UserStore<TARUser>(context);
                var manager = new UserManager<TARUser>(store);
                var user = new TARUser { UserName = "Admin@Admin.com", Email = "Admin@Admin.com", DisplayName = "Admin", ImagePath = "~/Content/Images/Empty_pic.png" };
                var result= manager.Create(user, "Admin@123");

            }
            if (!context.Users.Any(r => r.UserName == "Staff@Staff.com"))
            {
                var store = new UserStore<TARUser>(context);
                var manager = new UserManager<TARUser>(store);
                var user = new TARUser { UserName = "Staff@Staff.com", Email = "Staff@Staff.com", DisplayName = "Staff", ImagePath = "~/Content/Images/Empty_pic.png" };
                var result = manager.Create(user, "Staff@123");

            }
            if (!context.Users.Any(r => r.UserName == "Member@Member.com"))
            {
                var store = new UserStore<TARUser>(context);
                var manager = new UserManager<TARUser>(store);
                var user = new TARUser { UserName = "Member@Member.com", Email = "Member@Member.com",DisplayName="Member",ImagePath= "~/Content/Images/Empty_pic.png" };
                var result = manager.Create(user, "Member@123");

            }
        

            context.SaveChanges();

            if(context.Users.Any(r => r.UserName == "Admin@Admin.com"))
            {
                var store = new UserStore<TARUser>(context);
                var manager = new UserManager<TARUser>(store);
                var user = context.Users.FirstOrDefault(r => r.UserName == "Admin@Admin.com");
                var role = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                user.Roles.Add(new IdentityUserRole() { UserId = user.Id, RoleId = role.Id });

            }
            if (context.Users.Any(r => r.UserName == "Staff@Staff.com"))
            {
                var store = new UserStore<TARUser>(context);
                var manager = new UserManager<TARUser>(store);
                var user = context.Users.FirstOrDefault(r => r.UserName == "Staff@Staff.com");
                var role = context.Roles.FirstOrDefault(r => r.Name == "Staff");
                user.Roles.Add(new IdentityUserRole() { UserId = user.Id, RoleId = role.Id });

            }
            if (context.Users.Any(r => r.UserName == "Member@Member.com"))
            {
                var store = new UserStore<TARUser>(context);
                var manager = new UserManager<TARUser>(store);
                var user = context.Users.FirstOrDefault(r => r.UserName == "Member@Member.com");
                var role = context.Roles.FirstOrDefault(r => r.Name == "Member");
                user.Roles.Add(new IdentityUserRole() { UserId = user.Id, RoleId = role.Id });

            }
            context.SaveChanges();


        }
    }
}

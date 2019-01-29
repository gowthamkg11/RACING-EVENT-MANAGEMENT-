using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.BusinessModels;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    [HandleError()]
    public class TeamController : Controller
    {
        /*<summary>
           Business Access Layer(BAL) initialization
       </summary>*/
       
        private TARDBContext db = new TARDBContext();
        private MemberBAL memberBAL = new MemberBAL();

        TeamBAL teamBAL = null;
        public TeamController()
        {
            teamBAL = new TeamBAL();
        }
        public TeamController(TeamBAL teamBAL)
        {
            this.teamBAL = teamBAL;
        }

        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadTeams(string EventID)
        {
           return PartialView(teamBAL.GetTeams(int.Parse(EventID)));
        }
        [HttpGet]
        public ActionResult CreateTeam(int EventID)
        {
            TeamViewModel viewModel = new TeamViewModel();
            viewModel.EventViewModel = teamBAL.GetEventById(EventID);
            return PartialView(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeam(TeamViewModel viewModel)
        {
           
            viewModel.EventViewModel = teamBAL.GetEventById(viewModel.EventViewModel.EventID);

            teamBAL.SaveTeam(viewModel);
            return PartialView(viewModel);
        }


        public ActionResult CreateMember(int teamId, int eventId)
        {
            Session["teamIdForMember"] = teamId;
            Session["eventIdForMember"] = eventId;

            List<SelectListItem> ListOfUsers = new List<SelectListItem>();

            var getRole = (from r in db.Roles where r.Name.Contains("Member") select r).FirstOrDefault();
            var getMemberUsers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(getRole.Id) && x.isAssigned==false).ToList();
            foreach (var item in getMemberUsers)
            {
                ListOfUsers.Add(new SelectListItem() { Text = item.UserName, Value = item.UserName });
            }
            ViewBag.MemberList = ListOfUsers;

            
            return PartialView("_createMember");
        }

        public ActionResult getMember(int teamId)
        {
            //int teamId = Convert.ToInt32(Session["teamIdForMember"]);
            int eventId = Convert.ToInt32(Session["eventId"]);

            List<Member> members = memberBAL.getMembers(teamId, eventId);

            return PartialView("_MemberList", members);

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddMember(String email)
        {
            int teamId = Convert.ToInt32(Session["teamIdForMember"]);
            int eventId = Convert.ToInt32(Session["eventIdForMember"]);

            //String email = tarUserViewModel.Email;

            List<Member> members = memberBAL.AddMembers(email, teamId, eventId);

            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = await UserManager.FindByNameAsync(email);
            var userId = user.Id;

            TARUser currentUser = UserManager.FindById(userId);

            currentUser.isAssigned = true;
            db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();

            return PartialView("_MemberList", members);

        }

        public async System.Threading.Tasks.Task<ActionResult> DeleteMember(int memberId)
        {
            int eventId = Convert.ToInt32(Session["eventId"]);

            Member singleMember = memberBAL.GetMemberName(memberId);
            int teamId = singleMember.Team.TeamID;
            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = await UserManager.FindByNameAsync(singleMember.MemberName);
            var userId = user.Id;

            TARUser currentUser = UserManager.FindById(userId);

            currentUser.isAssigned = false;
            db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();

            memberBAL.deleteMember(memberId);
            List<Member> members = memberBAL.getMembers(teamId, eventId);

            return PartialView("_MemberList", members);

        }

        public async System.Threading.Tasks.Task<ActionResult> DeleteTeam(int teamId)
        {
            int eventId = Convert.ToInt32(Session["eventId"]);

            List<Member> members = memberBAL.getMembersByTeam(teamId);

            for(int i=0; i<members.Count(); i++)
            {
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = await UserManager.FindByNameAsync(members[i].MemberName);
                var userId = user.Id;

                TARUser currentUser = UserManager.FindById(userId);

                currentUser.isAssigned = false;
                db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
            }
            teamBAL.DeleteTeam(teamId);

            return PartialView("LoadTeams", teamBAL.GetTeams(eventId));

        }
    }
}
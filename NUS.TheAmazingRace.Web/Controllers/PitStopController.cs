using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmazingRace.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NUS.TheAmazingRace.Web.Controllers
{
    [Authorize(Roles = "Staff, Admin")]
    public class PitStopController : Controller
    {
        /*<summary>
		    Business Access Layer(BAL) initialization
		</summary>*/
        private PitStopBAL pitStopBAL = new PitStopBAL();
        private EventBAL eventBAL = new EventBAL();
        private TARDBContext db = new TARDBContext();
       

        /*<summary>
          * Fetched the current event details based on event ID
          * calls pItStopBAL to fet pitStops for the event
            </summary>
            <returns>
            View of the List of pitStops
       </returns>*/
        /// <param name="EventID"></param>
        public ActionResult Index(int EventID = 0)
        {
            Session["eventId"] = EventID;
            Event currentEvent = eventBAL.GetSelectedEvent(EventID);
            return PartialView("_Index", pitStopBAL.getPitStopOfEvent(EventID));
        }



        /*<summary>
          * Fetches PitStops from PitStopBAL based on event ID
            </summary>
            <returns>
            Json object of the List of pitStops
        </returns>*/
        public JsonResult GetPitStopData()
        {
            int EventID = Convert.ToInt32(Session["eventId"]);
            return Json(pitStopBAL.getPitStopOfEvent(EventID), JsonRequestBehavior.AllowGet);
        }



        /*<summary>
          * Fetches PitStops from PitStopBAL based on PitStop ID
            </summary>
            <returns>
            Json object of the pitStops details
        </returns>*/
        public JsonResult GetPitStop()
        {
            int pitStopId = Convert.ToInt32(Session["pitStopId"]);
            return Json(pitStopBAL.getPitStopOfPitStopId(pitStopId), JsonRequestBehavior.AllowGet);
        }



        /*<summary>
          * creates pitStop
            </summary>
            <returns>
            dialog View of the pitStop creation
        </returns>*/
        public ActionResult CreatePitStop()
        {
            TARDBContext userList = new TARDBContext();
            List<SelectListItem> ListOfUsers = new List<SelectListItem>();

            var getRole = (from r in db.Roles where r.Name.Contains("Staff") select r).FirstOrDefault();
            var getStaffUsers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(getRole.Id)).ToList();
            foreach (var item in getStaffUsers)
            {
                ListOfUsers.Add(new SelectListItem() { Text = item.UserName, Value = item.UserName });
            }
            ViewBag.StaffList = ListOfUsers;

            return PartialView("_CreatePitStop");
        }



        /*<summary>
           * Edit pitStop
            </summary>
            <returns>
            dialog View of the pitStop Edit along with pitStop details
        </returns>*/
        /// <param name="pitStopId"></param>
        public ActionResult EditPitStops(int pitStopId)
        {

            TARDBContext userList = new TARDBContext();
            List<SelectListItem> ListOfUsers = new List<SelectListItem>();
            var getRole = (from r in db.Roles where r.Name.Contains("Staff") select r).FirstOrDefault();
            var getStaffUsers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(getRole.Id)).ToList();

            foreach (var item in getStaffUsers)
            {
                ListOfUsers.Add(new SelectListItem() { Text = item.UserName, Value = item.UserName });
            }

            ViewBag.StaffList = ListOfUsers;
            Session["pitStopId"] = pitStopId;
            return PartialView("_EditPitStop", pitStopBAL.GetSelectedPitStop(pitStopId));
        }



        /*<summary>
          * Creates new pitStop
        </summary>
        <returns>
          view of the list of PitStops
        </returns>*/
        /// <param name="pitStop"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> AddPitStop(PitStop pitStop)
        {
            UserManager<TARUser> UserManager = new UserManager<TARUser>(new UserStore<TARUser>(new TARDBContext()));
            List<PitStop> pitStops = new List<PitStop>();
            int eventId = Convert.ToInt32(Session["eventId"]);
            String currentUser = User.Identity.GetUserName();

            var user = await UserManager.FindByNameAsync(pitStop.Staff.UserName);
            var userId = user.Id;
            db.PitStops.Add(pitStop);
            pitStops = pitStopBAL.CreatePitStopList(pitStop, currentUser, eventId, userId);

            List<SelectListItem> ListOfUsers = new List<SelectListItem>();
            var getRole = (from r in db.Roles where r.Name.Contains("Staff") select r).FirstOrDefault();
            var getStaffUsers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(getRole.Id)).ToList();

            foreach (var item in getStaffUsers)
            {
                ListOfUsers.Add(new SelectListItem() { Text = item.UserName, Value = item.UserName });
            }

            ViewBag.StaffList = ListOfUsers;
            Response.StatusCode = 202;
            return PartialView("_Index", pitStops);
        }



        /*<summary>
          * Delete pitStop
        </summary>
        <returns>
          dialog View of the pitStop Delete
        </returns>*/
        /// <param name="pitStopId"></param>
        public ActionResult DeletePitStop(int pitStopId)
        {
            int eventID = Convert.ToInt32(Session["eventId"]);
            List<PitStop> pitStop = pitStopBAL.DeletePitStopfromList(pitStopId, eventID);
            return PartialView("_Index", pitStop);
        }



        /*<summary>
             Fetch pitStop details fron pitStopBAL
             </summary>
            /// <param name="pitStopId"></param>
            <returns>View of the pitStop details
       </returns>*/
        public ActionResult PitstopDetails(int pitStopId)
        {

            return PartialView("_PitStopDetails", pitStopBAL.GetSelectedPitStop(pitStopId));
        }
    }

}
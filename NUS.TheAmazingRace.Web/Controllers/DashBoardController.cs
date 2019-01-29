using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    /*<summary>
       This is the Controller class for Dashboard,
       This is authorized to all users.
   </summary>*/
    public class DashBoardController : Controller
    {
        /*<summary>
		   Business Access Layer(BAL) initialization
		</summary>*/

        private EventBAL eventBAL = new EventBAL();
        private PitStopBAL pitStopBAL = new PitStopBAL();
        private TeamBAL teamBAL = new TeamBAL();
        private Event eventObj = new Event();


        /*<summary>
             Hub controller initialization with team Locations
       </summary>*/
        private HubController hubController = new HubController();
        List<TeamLocation> teamLocations = new List<TeamLocation>();



        /*<summary>
         * Retrieves the entire dashboard with partial views
         </summary>
         <returns>
         the whole dashboard View
         </returns>*/
        public ActionResult Index()
        {
            return View();
        }


        /*<summary>
          * Shows the list of current events
         </summary>
         <returns>
          View of the List of Current Events
          </returns>*/


        public PartialViewResult CurrentEvent()
        {
            return PartialView("_CurrentEvent", eventBAL.CurrentEvent());
        }


        /*<summary>
          * Shows the list of Upcoming events
           </summary>
          <returns>
          View of the List of Upcoming Events
          </returns>*/
        public PartialViewResult UpcomingEvent()
        {
            return PartialView("_UpcomingEvent", eventBAL.UpcomingEvents());
        }



        /*<summary>
          * Stores event ID in Session on click of the event.
          * retrives PitStops from DB based on eventId
         </summary>
         <returns>
          Json Object of PitsStop
         </returns>*/
        public JsonResult GetPitStopData()
        {
            int EventID = Convert.ToInt32(Session["eventId"]);
            return Json(pitStopBAL.getPitStopOfEvent(EventID), JsonRequestBehavior.AllowGet);
        }



        /*<summary>
          * Shows PitStops in Map.
         </summary>
         <returns>
          View of the List of PitStops in Map
        </returns>*/
        /// <param name="eventId"></param>
        public ActionResult ShowEventData(int eventId)
        {
            Session["eventId"] = eventId;
            return PartialView("_MapView");
        }



        /*<summary>
          * Shows the teams list and their progress, registered for the selected event
         </summary>
         <returns>
          View of the List of teams
         </returns>*/
        /// <param name="EventID"></param>
        public PartialViewResult CurrentEventBoard(int EventID)
        {
            return PartialView("_LeaderBoard", teamBAL.GetTeamByEvent(EventID));
        }



        /*<summary>
          * Web API call and SignalR setUp
         </summary>
         <returns>
          Json data of StaffLoaction to track staff.
         </returns>*/
        /// <param name="number"></param>
        public async Task<JsonResult> GetLocations(int number)
        {
            string Baseurl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
            
            int EventID = Convert.ToInt32(Session["eventId"]);
            List<TeamLocation> teamInfo = new List<TeamLocation>();
            List<StaffLocation> staffInfo = new List<StaffLocation>();
            TeamLocation teamLocation = new TeamLocation();
            StaffLocation staffLocation = new StaffLocation();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                //HttpResponseMessage Res = await client.GetAsync("api/values/" + number);
                String StaffLocation = await client.GetStringAsync("api/staff/" + number);
                String MemberLocation = await client.GetStringAsync("api/values/" + number);
                //Checking the response is successful or not which is sent using HttpClient  
                if (MemberLocation != null)
                {
                    JArray teamJson = JArray.Parse(MemberLocation);
                    //JArray staffJson = JArray.Parse(StaffLocation);
                    for(int i=0; i<3;i++)
                    {
                        TeamLocation teamLoc = JsonConvert.DeserializeObject<TeamLocation>(teamJson[i].ToString());
                        //staffLocation = JsonConvert.DeserializeObject<StaffLocation>(staffJson[i].ToString());
                        teamLocations.Add(teamLoc);
                        //staffInfo.Add(staffLocation);
                    }
                     teamBAL.updateTeamLocation(teamLocations, EventID);
                }
                else
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
                //    //Deserializing the response recieved from web api and storing into the Employee list  

                hubController.CallSignalR(EventID);

                return Json(StaffLocation, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
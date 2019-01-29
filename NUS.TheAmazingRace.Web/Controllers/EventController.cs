using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmazingRace.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace NUS.TheAmazingRace.Web.Controllers
{
	/*<summary>
	This is the Controller class for events,
	the Event management CRUD operation is present here,
	This is only authorized to Staff and Admin
	</summary>*/

	[Authorize(Roles = "Staff, Admin")]

	public class EventController : Controller
	{
		/*<summary>
		Business Access Layer(BAL) initialization
		</summary>*/

		private EventBAL eventBAL = new EventBAL();
		private PitStopBAL pitStopBAL = new PitStopBAL();
		private List<PitStop> pitStop = new List<PitStop>();



		/*<summary>
		Returns the Event Management Tab with Event partial view and
		Pitstop Partial view
		</summary>
		<returns>View of the List of Events,pitstops and teams</returns>*/

		[HttpGet]
		public ActionResult Index()
		{
		   
			return View();
		}

		

		/*<summary>
		this method returns the Event List sorted with Latest Date to the partialView
		</summary>
		<returns>PartialView of the List of Events</returns>*/

		public PartialViewResult EventsList()
		{
			List<Event> events = eventBAL.GetEventList();

			return PartialView("_EventsList",events.ToList());
		}



		/*<summary>
		 * This method is used to Show the list of Events created
		 </summary>*/
	   ///<param name="eventModel">On the submission of create and edit view of Event,
	   /// The Event Model Object is posted here and saved to the Database through BAL</param>
	   /* <returns>PartialView of the List of Events with new update Model </returns> */

		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventsList(Event eventModel)
		{
			//in case of editing an existing Event
			if (eventModel.EventID > 0)
			{
				Event selectedEvent = eventBAL.GetEditingValues(eventModel.EventID);
				//if no image is uploaded keep existing image
				if (eventModel.ImageFile == null)
				{
					eventModel.ImagePath = selectedEvent.ImagePath;
				}
				//if an image is uploaded by user
				else
				{
					string fileName = Path.GetFileNameWithoutExtension(eventModel.ImageFile.FileName);
					string extension = Path.GetExtension(eventModel.ImageFile.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					eventModel.ImagePath = "~/Content/Images/" + fileName;
					fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
					eventModel.ImageFile.SaveAs(fileName);
				}
			}
			//if a new event is created
			else
			{
				//If no image is uploaded,Use default image
				if (eventModel.ImageFile == null)
				{
					eventModel.ImagePath = "~/Content/Images/Empty_Event_Icon.png";
				}
				//if an image is uploaded add to database
				else
				{
					string fileName = Path.GetFileNameWithoutExtension(eventModel.ImageFile.FileName);
					string extension = Path.GetExtension(eventModel.ImageFile.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					eventModel.ImagePath = "~/Content/Images/" + fileName;
					fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
					eventModel.ImageFile.SaveAs(fileName);
				}
			}
			string currentUser = User.Identity.GetUserName();

			return PartialView("_EventsList", eventBAL.EditEventList(eventModel, currentUser).ToList());
		}



		/*<summary>
		 This method is used to search the event
		</summary>*/
		///<param name="searchString"> This is the search string typed by user to search an event</param>        
	   /* <returns>PartialView of the List of searched Events  </returns> */

		[HttpGet]
		public ActionResult Search(string searchString)
		{

			return PartialView("_EventsList", eventBAL.SearchEvent(searchString).ToList());

		}



		/*<summary>
		 This method is used to Create an event
		</summary>*/
		///<param name="eventModel"> Event Object on form submission,
		///when Adding an event in the View</param>
		/*<returns>PartialView of the List of  Events present  </returns> */

		public ActionResult CreateEvent(Event eventModel)
		{
		  
			return PartialView("_CreateEvent");
		}



		/*<summary>
		 This method is used to delete an event 
		</summary>*/
		///<param name="eventId"> Event id the selection of Event to delete </param>
		/*<returns>PartialView of the List of Updated Events after delete </returns> */

		public ActionResult DeleteEvent(int eventId)
		{
			
			return PartialView("_EventsList", eventBAL.DeleteEventfromList(eventId));
		}



		/*<summary>
		 This method is used to View details of an event 
		</summary>*/
		///<param name="eventId"> Event id the selection of Event to delete </param>
		/*<returns>PartialView of the details of the selected Event</returns> */

		public ActionResult EventDetails(int eventId)
		{

			return PartialView("_EventDetails",eventBAL.GetSelectedEvent(eventId));
		}



		/*<summary>
		This method is used to Edit an event 
	   </summary>*/
		///<param name="eventId"> Event id the selection of Event to edit </param>
		/*<returns>This returns the already present values
		 of event to be edited to the view</returns> */

		public ActionResult EditEvent(int eventId)
		{
						
			return PartialView("_EditEvent", eventBAL.GetEditingValues(eventId));

		}



		/*<summary>
	   This method is used to search an event
	  </summary>*/
		///<param name="search"> text to be searched </param>
		/*<returns>this returns the JSON result to the AJAX call of the search text
		</returns> */

		public JsonResult GetEvents(string search)
		{

			return Json(eventBAL.SearchEvent(search), JsonRequestBehavior.AllowGet);

		}
	}
}
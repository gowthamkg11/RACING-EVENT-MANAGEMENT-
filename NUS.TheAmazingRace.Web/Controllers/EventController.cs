using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmazingRace.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace NUS.TheAmazingRace.Web.Controllers
{
    [Authorize]

    public class EventController : Controller
    {
      
       
        private EventBAL eventBAL = new EventBAL();
        private PitStopBAL pitStopBAL = new PitStopBAL();
        private EventManagement eventManagement = new EventManagement();
        private List<PitStop> pitStop = new List<PitStop>();

        [HttpGet]
        public ActionResult Index()
        {
            List<Event> events = eventBAL.GetEventList();
            List<PitStop> pitStops = pitStopBAL.GetPitStopList();
            eventManagement.PitStops = pitStop;
            eventManagement.Events = events;
            return View(eventManagement);
        }


        [HttpPost]
        public ActionResult Index(Event eventModel)
        {
            string currentUser= User.Identity.GetUserName();
            eventManagement.Events = eventBAL.EditEventList(eventModel, currentUser);
            eventManagement.PitStops = pitStop;
            return View(eventManagement);
        }

        
        public ActionResult LoadPitStops(int eventID)
        {
            eventManagement.PitStops=pitStopBAL.getPitStopOfEvent(eventID);
            eventManagement.Events = eventBAL.GetEventList();

            return View("Index",eventManagement);
        }


        [HttpPost]
        public ActionResult Search(string searchString)
        {
            return View("Index",eventBAL.SearchEvent(searchString));
        }


        public ActionResult CreateEvent(Event eventModel)
        {
          
            return PartialView("_CreateEvent");
        }

        public JsonResult DeleteEvent(int eventId)
        {
            bool result = eventBAL.DeleteEventfromList(eventId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventDetails(int eventId)
        {

            return PartialView("_EventDetails",eventBAL.GetSelectedEvent(eventId));
        }

        public ActionResult EditEvent(int eventId)
        {
                        
            return PartialView("_EditEvent", eventBAL.GetEditingValues(eventId));

        }

        public JsonResult GetEvents(string search)
        {

            return Json(eventBAL.SearchEvent(search), JsonRequestBehavior.AllowGet);
        }


    }
}
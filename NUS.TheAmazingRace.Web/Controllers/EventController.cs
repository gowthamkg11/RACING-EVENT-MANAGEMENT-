using NUS.TheAmagingRace.DAL;
using NUS.TheAmazingRace.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    [Authorize]

    public class EventController : Controller
    {
        // GET: Event
        private TARDBContext db = new TARDBContext();
        private Event events = new Event();


        [HttpGet]
        public ActionResult Index()
        {

            return View(db.Events.ToList());
        }
        [HttpPost]
        public ActionResult Index(Event eventModel)
        {
            if (eventModel.EventID > 0)
            {
                Event editEvents = db.Events.SingleOrDefault(x => x.EventID == eventModel.EventID);
                editEvents.EventName = eventModel.EventName;
                editEvents.EventDescription = eventModel.EventDescription;
                editEvents.EventCountry = eventModel.EventCountry;
                editEvents.EventCity = eventModel.EventCity;
                editEvents.StartDate = eventModel.StartDate;
                editEvents.EndDate = eventModel.EndDate;
                editEvents.TotalPitStops = eventModel.TotalPitStops;
                editEvents.TotalTeams = eventModel.TotalTeams;
                db.SaveChanges();
            }
            else
            {
                db.Events.Add(eventModel);
                db.SaveChanges();
            }
           return View(db.Events.ToList());
        }


        public ActionResult CreateEvent(Event eventModel)
        {
          
            return PartialView("_CreateEvent");
        }

        public JsonResult DeleteEvent(int eventId)
        {
            bool result = false;
            var events = db.Events.Find(eventId);
            db.Events.Remove(events);
            db.SaveChanges();
            result = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditEvent(int eventId)
        {

            Event editEvents = db.Events.SingleOrDefault(x => x.EventID == eventId);
            events.EventID = editEvents.EventID;
            events.EventName = editEvents.EventName;
            events.EventDescription = editEvents.EventDescription;
            events.EventCountry = editEvents.EventCountry;
            events.EventCity = editEvents.EventCity;
            events.StartDate = editEvents.StartDate;
            events.EndDate = editEvents.EndDate;
            events.TotalPitStops = editEvents.TotalPitStops;
            events.TotalTeams = editEvents.TotalTeams;
            return PartialView("_EditEvent", events);

        }


    }
}
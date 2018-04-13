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

        public ActionResult Index()
        {

            return View(db.Events.ToList());
        }


        public ActionResult CreateEvent()
        {
            return View();
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
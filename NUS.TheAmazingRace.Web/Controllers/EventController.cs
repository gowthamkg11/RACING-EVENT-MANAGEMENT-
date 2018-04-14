using NUS.TheAmagingRace.BAL;
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
      
       
        private EventBAL eventBAL = new EventBAL();



        [HttpGet]
        public ActionResult Index()
        {
            return View(eventBAL.GetEventList());
        }


        [HttpPost]
        public ActionResult Index(Event eventModel)
        {
           return View(eventBAL.EditEventList(eventModel));
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


    }
}
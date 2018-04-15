using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmazingRace.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;


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
            
           string currentUser= User.Identity.GetUserName();
           return View(eventBAL.EditEventList(eventModel,currentUser));
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
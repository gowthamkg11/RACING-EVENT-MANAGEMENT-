using Microsoft.AspNet.Identity;
using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    public class PitStopController : Controller
    {
        private PitStopBAL pitStopBAL = new PitStopBAL();
        private EventBAL eventBAL = new EventBAL();
        private EventManagement eventManagement = new EventManagement();
        // GET: PitStop
        public ActionResult Index(int EventID=0)
        {
          
                return PartialView("_Index", pitStopBAL.getPitStopOfEvent(EventID));
            
          
        }

        public JsonResult GetPitStopData(int EventID)
        {

            return Json(pitStopBAL.getPitStopOfEvent(EventID),JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreatePitStop()
        {
           return PartialView("_CreatePitStop");
        }

        //[HttpPost]
        //public ActionResult AddPitStop(PitStop pitStop)
        //{
        //    String currentUser = User.Identity.GetUserName();
        //    eventManagement.PitStops = pitStopBAL.CreatePitStopList(pitStop, currentUser);
        //    eventManagement.Events = eventBAL.GetEventList();
        //    return View("~/Views/Event/Index.cshtml", eventManagement);
        //}
    }
}
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
        // GET: PitStop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePitStop(PitStop pitStop)
        {
            String currentUser = User.Identity.GetUserName();
            pitStopBAL.CreatePitStopList(pitStop, currentUser);
            return PartialView();
        }
    }
}
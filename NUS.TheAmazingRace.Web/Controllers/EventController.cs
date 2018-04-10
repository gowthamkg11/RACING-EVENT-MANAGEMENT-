using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class EventController : Controller
    {
        // GET: Event
        [ActionName("EventLists")]
        public ActionResult Index()
        {
            return View();
        }


    }
}
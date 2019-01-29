using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmagingRace.DAL.Reporsitory;
using NUS.TheAmagingRace.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        IMemberBAL memberBAL = null;
        public MemberController()
        {
            memberBAL = new MemberBAL();
        }
        protected MemberController(IMemberBAL memberBAL)
        {
            this.memberBAL = memberBAL;
        }


        /*<summary>
		 This method is used to Member details
         GET: Member
        </summary>*/

        public ActionResult Index()
        {


            return View();
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /*<summary>
		 This method is used to create a member
         GET: Member
        </summary>*/

        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult LoadAdministrators()
        {
            var Administrators = memberBAL.GetAllTARAdministrators();
            return PartialView(Administrators);
        }

        [HttpGet]
        public ActionResult LoadMembers()
        {
            var members = memberBAL.GetAllMember();
            return PartialView(members);
        }

        [HttpGet]
        public ActionResult LoadStaff()
        {
            var Staff = memberBAL.GetAllStaff();
            return PartialView(Staff);
        }


        [HttpPost]
        public ActionResult UpdateToAdmin(string userID)
        {
            memberBAL.MoveMembertoAdmin(userID);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateToStaff(string userID)
        {
            memberBAL.MoveMembertoStaff(userID);
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult RemoveasStaff(string userID)
        {
            memberBAL.RemoveMemberasStaff(userID);
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult RemoveasAdmin(string userID)
        {
            memberBAL.RemoveMemberasAdmin(userID);
            return new EmptyResult();
        }
    }
}

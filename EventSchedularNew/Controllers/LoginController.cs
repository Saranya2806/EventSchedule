using EventSchedularNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSchedularNew.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        MeetingBusiness objBus = new MeetingBusiness();
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(MeetingLogin mtl)
        {
            if (ModelState.IsValid)
            {
                var res = objBus.Login(mtl);
                if (res != null)
                {

                    if (mtl.UserID == res.UserID)
                    {
                        if (mtl.PassWord == res.PassWord)
                            TempData["msg"] = "Valid";
                        else
                            TempData["msg"] = "Password Invalid.";
                    }
                    else
                        TempData["msg"] = "UserName Invalid.";
                }
                else
                    TempData["msg"] = "UserName or Password Invalid.";
            }
            else
                TempData["msg"] = "UserName or Password Invalid.";

            if (TempData["msg"].ToString() == "Valid")
                return RedirectToAction("SelectSchedule", "Home");
            else
                return RedirectToAction("UserLogin");
        }
        public ActionResult logOut ()
        {
           return RedirectToAction ("UserLogin");
        }
       
    }
}
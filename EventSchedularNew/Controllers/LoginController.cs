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
            var res = objBus.Login(mtl);
            if(mtl.UserID==res.UserID)
            {
                if(mtl.PassWord==res.PassWord)
                {
                    return RedirectToAction("SelectSchedule", "Home");
                }
                else
                {
                    return RedirectToAction("UserLogin");
                }
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }
       
    }
}
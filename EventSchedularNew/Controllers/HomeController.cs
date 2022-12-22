using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSchedularNew.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index1()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using (ConferenceHallEntities3 obj = new ConferenceHallEntities3())
            {
                var events = obj.NewEventsDatas.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }        
        }
    }
}
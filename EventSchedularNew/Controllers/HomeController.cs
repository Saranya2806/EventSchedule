using EventSchedularNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSchedularNew.Controllers
{
    public class HomeController : Controller
    {
        MeetingBusiness ObjBus = new MeetingBusiness();
        // GET: Home
        public ActionResult Index1(string employeeId,string employeeNme)
        {
            ViewData["empId"] = employeeId;
            ViewData["empName"] = employeeNme;
            return View();
        }
        public ActionResult SelectSchedule()
        {
            
            var res = new MeetingLogin();
            res.SelectPlant = ObjBus.Plant();
            res.SelectPlant.Add(new SelectListItem() { Value = "0", Text = "----Select----" });
            return View(res);
        }
        public JsonResult SELUnit(int plantID)
        {
            var resunit = ObjBus.Unit(plantID);
            return Json(resunit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SELDept(int plantid,int unitid)
        {
            var resdept = ObjBus.Dept(plantid, unitid);
            return Json(resdept, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SELStaff(int plantid,int unitid,int deptid,int empmode)
        {
            var resstaff = ObjBus.Staff(plantid, unitid, deptid, empmode);
            return Json(resstaff, JsonRequestBehavior.AllowGet);
        }
       

        public JsonResult GetEvents()
        {
            using (ConferenceHallEntities3 obj = new ConferenceHallEntities3())
            {
                var events = obj.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }        
        }
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using(ConferenceHallEntities3 obj=new ConferenceHallEntities3())
            {
                if(e.id >0)
                {
                    var c = obj.Events.Where(a => a.id == e.id).FirstOrDefault();
                    if(c!=null)
                    {
                        c.subject = e.subject;
                        c.start_date =Convert.ToDateTime(e.start_date);
                        c.end_date =Convert.ToDateTime(e.end_date);
                        c.Description = e.Description;
                        c.IsFullDay = e.IsFullDay;
                        c.Themecolor = e.Themecolor;
                        c.empID = e.empID;
                        c.empName = e.empName;

                    }
                                     
                }
                else
                {
                    if (e.end_date.Equals(DateTime.MinValue))
                        e.end_date = null;

                        obj.Events.Add(e);
                }
                obj.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

     [HttpPost]
        public JsonResult DeleteEvents(int eventID)
        {
            var status = false;
            using (ConferenceHallEntities3 del = new ConferenceHallEntities3()) 
            {
                var c = del.Events.Where(a => a.id == eventID).FirstOrDefault();
                if(c!=null)
                {
                    del.Events.Remove(c);
                    del.SaveChanges();
                    status = true;

                }
            }
            return new JsonResult { Data = new { status = status } };
        }
       

    }
}
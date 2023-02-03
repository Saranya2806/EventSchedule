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
        public ActionResult Calender(int empid, string EmpName, string RoomID, string RoomName)
        {
            ViewData["empId"] = empid;
            ViewData["empName"] = EmpName;
            ViewData["roomID"] = RoomID;
            ViewData["roomName"] = RoomName;
            return View();
        }
        public ActionResult SelectSchedule()
        {
            
            var res = new MeetingLogin();
            res.SelectPlant = ObjBus.Plant();
            //res.SelectPlant.Insert(new SelectListItem() { Value = "0", Text = "--Select--" });
            res.SelectPlant.Insert(0, new SelectListItem { Value = "0", Text = "--Select" });
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
        public ActionResult EventTable()
        {
            var resEvents = ObjBus.getEvents();
            return View(resEvents);
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
        public JsonResult SaveEvent(Event eve)
        {
            var status = false;
            using(ConferenceHallEntities3 obj=new ConferenceHallEntities3())
            {
                if(eve.id >0)
                {
                    var eventData = obj.Events.Where(a => a.id == eve.id).FirstOrDefault();
                    if(eventData != null)
                    {
                        eventData.subject = eve.subject;
                        eventData.start_date =Convert.ToDateTime(eve.start_date);
                        eventData.end_date =Convert.ToDateTime(eve.end_date);
                        eventData.Description = eve.Description;
                        eventData.IsFullDay = eve.IsFullDay;
                        eventData.Themecolor = eve.Themecolor;
                        eventData.empID = eve.empID;
                        eventData.empName = eve.empName;
                        eventData.RoomId = eve.RoomId;

                    }
                                     
                }
                else
                {
                    if (eve.end_date.Equals(DateTime.MinValue))
                        eve.end_date = null;

                        obj.Events.Add(eve);
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

        public JsonResult updateEvent(string eventID, string eventStatus)
        {
            var updateResult = ObjBus.eventUpdate(eventID, eventStatus);
            return Json(updateResult, JsonRequestBehavior.AllowGet);
        }


    }
}
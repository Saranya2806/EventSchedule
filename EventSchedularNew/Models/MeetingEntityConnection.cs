using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSchedularNew.Models
{
    public class MeetingEntityConnection
    {

        public MeetingLogin UserLoginDetails(MeetingLogin mt)
        {
            MeetingLogin objmt = new MeetingLogin();
            try
            {   
                using (ConferenceHallEntities3 objcom = new ConferenceHallEntities3())
                {

                    objmt = objcom.UserLogins.Where(a => a.EmployeeUserID == mt.UserID && a.LoginPassword == mt.PassWord)
                        .Join(objcom.EmployeeMasters, u => u.EmployeeUserID, e => e.EMPLOYEEID, (UId, EID) =>
                           new  MeetingLogin
                           {
                              UserID=UId.EmployeeUserID,
                              PassWord=UId.LoginPassword,
                              EmpName=EID.EMPLOYEENAME
                           }).FirstOrDefault();
                        //.
                        //.Select(e => new MeetingLogin { UserID = e.EmployeeUserID, PassWord = e.LoginPassword,EmpName=}).FirstOrDefault();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString(), "UserLoginDetails");
            }
            return objmt;
        }

        public List<SelectListItem> Selplant()
        {
            var obj = new List<SelectListItem>();
            try
            {
                using (ConferenceHallEntities3 objsel = new ConferenceHallEntities3())
                {
                    obj = objsel.PlantMasters.Select(a => new SelectListItem { Text = a.PlantName, Value = a.PlantID.ToString() }).ToList();                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString(), "Selplant");
            }
            return obj;
        }
        public List<SelectListItem> SelUnit(int plantID)
        {
            var obj = new List<SelectListItem>();
            try
            {
                using (ConferenceHallEntities3 objUni = new ConferenceHallEntities3())
                {
                   
                    obj = objUni.UnitMasters.Where(a => a.PlantID == plantID).Select(e => new SelectListItem { Text = e.UnitName, Value = e.UnitID.ToString() }).ToList();
                   obj.Insert(0, new SelectListItem { Value = "0",Text="--Select--" });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString(), "SelUnit");
            }
            return obj;
        }
        public List<SelectListItem> SelDept(int plantid, int unitid)
        {
            var obj = new List<SelectListItem>();
            try
            {
                using(ConferenceHallEntities3 objDept=new ConferenceHallEntities3())
                {
                    //obj = objDept.DepartmentMasters.Where(a => a.UnitID == unitid && a.PlantID == plantid).Join(objDept.UnitMasters)
                    //    .Select(e => new SelectListItem { Text = e.DepartmentName, Value = e.DepartmentID.ToString() }).ToList();
                    


                    obj = (from dm in objDept.DepartmentMasters
                           join um in objDept.UnitMasters on dm.UnitID equals um.UnitID
                           join pm in objDept.PlantMasters on um.PlantID equals pm.PlantID
                           where pm.PlantID == plantid && um.UnitID == unitid
                           select new SelectListItem
                           {
                               Value=dm.DepartmentID.ToString(),
                               Text=dm.DepartmentName                          
                           }).ToList();
                    obj.Insert(0, new SelectListItem { Value = "0", Text = "--Select--" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString(), "SelDept");
            }
            return obj;
        }

        public StaffRoom SelStaff(int plantid, int unitid, int deptid,int empmode)
        {
            StaffRoom objSR = new StaffRoom();
            var objStaff = new List<SelectListItem>();
            var objRoom = new List<SelectListItem>();
            try
            {
                using(ConferenceHallEntities3 objsta =new ConferenceHallEntities3())
                {
                    objStaff = (from em in objsta.EmployeeMasters
                           join dm in objsta.DepartmentMasters on em.DpartMentID equals dm.DepartmentID
                           join um in objsta.UnitMasters on dm.UnitID equals um.UnitID
                           join pm in objsta.PlantMasters on um.PlantID equals pm.PlantID
                           where pm.PlantID == plantid && um.UnitID == unitid && dm.DepartmentID == deptid && em.EmployeeMode == empmode.ToString()
                           select new SelectListItem
                           {
                               Value=em.EMPLOYEEID.ToString(),
                               Text=em.EMPLOYEENAME                                                           
                           }).ToList();
                    objStaff.Insert(0, new SelectListItem { Value = "0", Text = "--Select--" });
                    objSR.lstStaff = objStaff;
                    objRoom = (from cr in objsta.ConferenceRoomMasters
                                join um in objsta.UnitMasters on cr.UnitID equals um.UnitID
                                join pm in objsta.PlantMasters on um.PlantID equals pm.PlantID
                                where pm.PlantID == plantid && um.UnitID == unitid  
                                select new SelectListItem
                                {
                                    Value = cr.RoomID.ToString(),
                                    Text = cr.RoomName
                                }).ToList();
                    objRoom.Insert(0, new SelectListItem { Value = "0", Text = "--Select--" });
                    objSR.lstRoom = objRoom;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message, ToString(), "SelStaff");
            }
            return objSR;

        }

        public List<Events> getEventApprovedDetails()
        {
            var lstobj =new List<Events>();
            try
            {
                using(ConferenceHallEntities3 objEve=new ConferenceHallEntities3 ())
                {
                    lstobj = (from ev in objEve.Events
                              join em in objEve.EmployeeMasters on ev.empID equals em.EMPLOYEEID
                              join cm in objEve.ConferenceRoomMasters on ev.RoomId equals cm.RoomID
                              select new Events
                              {
                                  id=ev.id,
                                  EmpID = ev.empID,
                                  EmpName = em.EMPLOYEENAME,
                                  start_date = ev.start_date,
                                  end_date = ev.end_date,
                                  RoomID=ev.RoomId,
                                  BookingHall = cm.RoomName,
                                  subject=ev.subject,
                                  IsFullDay = ev.IsFullDay,
                                  Status = ev.EventStatus
                              }).ToList();
                }
             
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message, ToString(), "getEventApprovedDetails");
            }
            return lstobj;
        }

        public int UpdateEvent(string eventId, string status)
        {
            int result = 0;
            int eveID = Convert.ToInt32(eventId.ToString());
            try
            {
                using (ConferenceHallEntities3 objUpdate = new ConferenceHallEntities3())
                {
                    objUpdate.Events.Where(a => a.id == eveID).ToList().ForEach(i => i.EventStatus = status);
                    objUpdate.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                result = 0;
            }
            return result;
        }

    }
}

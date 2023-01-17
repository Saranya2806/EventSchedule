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
                        .Select(e => new MeetingLogin { UserID = e.EmployeeUserID, PassWord = e.LoginPassword }).FirstOrDefault();
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

        public List<SelectListItem> SelStaff(int plantid, int unitid, int deptid,int empmode)
        {
            var obj = new List<SelectListItem>();
            try
            {
                using(ConferenceHallEntities3 objsta =new ConferenceHallEntities3())
                {
                    obj = (from em in objsta.EmployeeMasters
                           join dm in objsta.DepartmentMasters on em.DpartMentID equals dm.DepartmentID
                           join um in objsta.UnitMasters on dm.UnitID equals um.UnitID
                           join pm in objsta.PlantMasters on um.PlantID equals pm.PlantID
                           where pm.PlantID == plantid && um.UnitID == unitid && dm.DepartmentID == deptid && em.EmployeeMode == empmode.ToString()
                           select new SelectListItem
                           {
                               Value=em.EMPLOYEEID.ToString(),
                               Text=em.EMPLOYEENAME                                                           
                           }).ToList();
                    obj.Insert(0, new SelectListItem { Value = "0", Text = "--Select--" });
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message, ToString(), "SelStaff");
            }
            return obj;

        }
    }
}

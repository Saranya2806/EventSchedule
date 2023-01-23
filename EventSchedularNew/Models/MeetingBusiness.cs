using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSchedularNew.Models
{
    public class MeetingBusiness
    {
        MeetingEntityConnection objEntity = new MeetingEntityConnection();
        public MeetingLogin Login(MeetingLogin mtl)
        {
            var result = objEntity.UserLoginDetails(mtl);
            return result;
        }
        public List<SelectListItem>Plant()
        {
            var result = objEntity.Selplant();
            return result.ToList();
        }
        public List<SelectListItem>Unit(int plantID)
        {
            var res = objEntity.SelUnit(plantID);
            return res.ToList();
        }
        public List<SelectListItem>Dept(int plantid,int unitid)
        {
            var res = objEntity.SelDept(plantid, unitid);
            return res.ToList();
        }
        public StaffRoom Staff(int plantid,int unitid,int deptid,int empmode)
        {
            var res = objEntity.SelStaff(plantid, unitid, deptid, empmode);
            return res;
        }
        public List<Events> getEvents()
        {
            var eveRes = objEntity.getEventApprovedDetails();
            return eveRes.ToList();
        }
            
    }
}
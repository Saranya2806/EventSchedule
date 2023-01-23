using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventSchedularNew.Models
{
    public class StaffRoom
    {
        public List<SelectListItem> lstStaff { get; set; }
        public List<SelectListItem> lstRoom { get; set; }
    }
}
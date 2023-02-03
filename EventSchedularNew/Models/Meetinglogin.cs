using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EventSchedularNew.Models
{
   
        public class MeetingLogin
        {
            [ScaffoldColumn(false)]
            [Required(ErrorMessage = "User ID Invalid")]
            public int? UserID { get; set; }

        [Required(ErrorMessage = "Inavlid Password")]
        //[RegularExpression("/(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8}/g", ErrorMessage = "Password must meet requirements")]
           
            public string PassWord { get; set; }
        public int PlantID { get; set; }
        public string PlantName { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string EmpName { get; set; }
             
            public List<SelectListItem> SelectPlant { get; set; }


    }
   
}
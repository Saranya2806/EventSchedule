using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventSchedularNew.Models
{
    public class Events
    {
        public int id { get; set; }
        public string text { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public string SourceId { get; set; }
        public string TargetId { get; set; }
        public string subject { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsFullDay { get; set; }
        public string Themecolor { get; set; }
    }
}
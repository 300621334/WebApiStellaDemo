using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyDemo.Models
{
    public class InteractView
    {
        public int InteractId { get; set; }
        public string Customer { get; set; }
        public string Agent { get; set; }
        //public string channel { get; set; }
        //public string country { get; set; }
        public string surveySent { get; set; } //0 or 1
        public string uuid { get; set; }
    }
}
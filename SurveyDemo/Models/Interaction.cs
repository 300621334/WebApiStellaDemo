using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyDemo.Models
{
    public class Interaction
    {
        public int contactID { get; set; }
        public int custID { get; set; }
        public int empID { get; set; }
        public string channel { get; set; }
        public string country { get; set; }
        public int surveySent { get; set; } //0 or 1
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyDemo.Models
{
    public class CustView
    {
        public int interactId { get; set; }
        public int custID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
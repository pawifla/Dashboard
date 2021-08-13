using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthForkDamnDashboard.App_Code
{
    public class DataClass
    {
        public string maxDate { get; set; }
        public string minDate { get; set; }

        public string measDate { get; set; }

        public int measMonth { get; set; }

        public double elevation { get; set; }

        public double upstream { get; set; }
        public double downstream { get; set; }
        public string file { get; set; }
        public int year { get; set; }
        public int id { get; set; }
        public int daysInMonth { get; set; }


    }
    public class ipDataClass
    {
        public string ipAddress { get; set; }
        public int portNumber { get; set; }
        public bool connStatus { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class testpackagesupplyview
    {
        public int supply_id { get; set; }
        public int pkg_id { get; set; }
        public string name { get; set; }
        public string lot { get; set; }
        public DateTime expiry_date { get; set; }
        public string lab_code { get; set; }
        public string analysis { get; set; }
        public int hours_tolerance_start { get; set; }
        public int hours_tolerance_end { get; set; }
    }
}

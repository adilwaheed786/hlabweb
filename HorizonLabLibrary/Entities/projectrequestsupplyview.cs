using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class projectrequestsupplyview
    {        
        public int id { get; set; }
        public int supply_id { get; set; }
        public string name { get; set; }
        public string lot { get; set; }
        public DateTime expiry_date { get; set; }
        public int hours_tolerance_start { get; set; }
        public int hours_tolerance_end { get; set; }
        public int proj_form_id { get; set; }
        public DateTime? incubation_date_time_in { get; set; }
        public DateTime? incubation_date_time_out { get; set; }
        public string incubation_temp { get; set; }
    }
}

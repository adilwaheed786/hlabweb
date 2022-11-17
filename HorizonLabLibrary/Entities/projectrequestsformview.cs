using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class projectrequestsformview
    {
        public int id { get; set; }
        public int form_id { get; set; }
        public DateTime? incubation_date_time_in { get; set; }
        public DateTime? incubation_date_time_out { get; set; }
        public string incubation_temp { get; set; }
        public bool is_rush { get; set; }
        public DateTime? date_created { get; set; }
        public string created_by { get; set; }
        public int project_id { get; set; }
        public int rec_count { get; set; }
        public string project { get; set; }
        public string hl_code { get; set; }
        public bool is_condition_met { get; set; }
    }
}

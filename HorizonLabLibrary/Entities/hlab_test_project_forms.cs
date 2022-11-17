using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
   public class hlab_test_project_forms
    {
        [Required, Key]
        public int id { get; set; }
        public int form_id { get; set; }
        public int project_id { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? incubation_date_time_in { get; set; }
        public DateTime? incubation_date_time_out { get; set; }
        public string incubation_temp { get; set; }
        public string created_by { get; set; }
        public bool is_rush { get; set; }
        public bool is_condition_met { get; set; }
    }
}

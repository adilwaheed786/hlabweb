using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_order_logs
    {
        [Required, Key]
        public int order_id { get; set; }
        public int? customer_id { get; set; }
        public DateTime? order_date { get; set; }
        public DateTime? incubation_date_time_in { get; set; }
        public DateTime? incubation_date_time_out { get; set; }
        public string incubation_temp { get; set; }
        public string received_by { get; set; }        
        public bool proc_status { get; set; }
        public decimal total_amount { get; set; }
        public decimal tax { get; set; }
        public bool record_status { get; set; }        
        public bool is_rush { get; set; }        
        public bool is_condition_met { get; set; }        
    }
}

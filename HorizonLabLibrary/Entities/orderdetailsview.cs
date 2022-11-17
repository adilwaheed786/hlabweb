using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class orderdetailsview
    {
        public DateTime? order_date { get; set; }               
        public DateTime? test_date { get; set; }
        public DateTime? collect_datetime { get; set; }

        public int order_item_id { get; set; }
        public int order_id { get; set; }
        public int? customer_id { get; set; }
        public int? trans_id { get; set; }
        public int? pkg_id { get; set; }
        public int? pkg_class_id { get; set; }
        public int gst_number { get; set; }
        public int assigned_coupon { get; set; }
        public int gen_coupon { get; set; }
        public int form_id { get; set; }
        public int email_template_id { get; set; }
        public bool proc_status { get; set; }

        public string received_by { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string pkg_class { get; set; }
        public string lab_code { get; set; }
        public string analysis { get; set; }
        public string description { get; set; }
        public string hl_code_prefix { get; set; }
        public string hl_code { get; set; }
        public string form_name { get; set; }
        public string idnty_location { get; set; }
        
        public decimal amount { get; set; }
        public decimal price { get; set; }       
    }
}

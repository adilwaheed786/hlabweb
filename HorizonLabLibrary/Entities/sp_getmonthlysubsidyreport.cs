using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class sp_getmonthlysubsidyreport
    {
        [Required, Key]
        public int result_id { get; set; }
        public int trans_id { get; set; }
        public int customer_id { get; set; }
        public string project { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string idnty_location { get; set; }
        public string sample_type { get; set; }
        public string sample_legal_loc { get; set; }
        public string town { get; set; }
        public string municipality { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string utm_x { get; set; }
        public string utm_y { get; set; }
        public string zone { get; set; }
        public string unit_of_measurement { get; set; }
        public string result { get; set; }
        public string submtd_by { get; set; }
        public DateTime? rcv_date { get; set; }
        public DateTime? collect_datetime { get; set; }
        public DateTime? test_date { get; set; }
        public DateTime? work_date { get; set; }
        public int? assigned_coupon { get; set; }
        public int? gen_coupon { get; set; }
        public bool status { get; set; }
        public int class_id { get; set; }
        public string pkg_class { get; set; }
        public string param_name { get; set; }
        public int param_id{ get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class sp_getsemipublicreport
    {
        [Required, Key]
        public int result_id { get; set; }
        public int trans_id { get; set; }
        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string env_distr { get; set; }
        public string dw_officer { get; set; }
        public string dw_phone { get; set; }
        public string com_code { get; set; }
        public int test_pkg_id { get; set; }
        public string lab_code { get; set; }
        public int sample_type_id { get; set; }
        public string sample_type { get; set; }
        public DateTime? rcv_date { get; set; }
        public DateTime? test_date { get; set; }
        public DateTime? collect_datetime { get; set; }
        public string idnty_name { get; set; }
        public string idnty_location { get; set; }
        public string sample_legal_loc { get; set; }
        public string town { get; set; }
        public string submtd_by { get; set; }
        public bool status { get; set; }
        public int class_id { get; set; }
        public int param_id { get; set; }
        public string pkg_class { get; set; }
        public string result { get; set; }
        public string param_name { get; set; }
    }
}

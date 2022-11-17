using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class horizonlabcustomerview
    {
        public int customer_id { get; set; }
        public string producer_number { get; set; }
        public string company_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string street { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        //public string location { get; set; }
        public int city_id { get; set; }
        public int? coupon { get; set; }
        public int? coupon_log_id { get; set; }
        public DateTime? coupon_issued_date { get; set; }
        public string city { get; set; }
        public int province_id { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string fax { get; set; }
        public bool status { get; set; }
        public bool is_public { get; set; }
        public bool is_semi_public { get; set; }
        public bool is_approve_financing { get; set; }
        public bool is_realestate { get; set; }
        public string env_distr { get; set; }
        public string dw_officer { get; set; }
        public string dw_phone { get; set; }
        public string com_code { get; set; }
        public DateTime? date_registered { get; set; }
        public int gst_number { get; set; }
        public string customer_name {
            get {
                return string.Format("{0} {1}", first_name, last_name);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class sp_gethorizonlabtransactiondetails
    {
        [Key]
        public int trans_id { get; set; }
        public int? test_pkg_id { get; set; }
        public string lab_code { get; set; }
        public decimal? price {get;set;}
        public bool? is_paid { get; set; }
        public int? invoice_id { get; set; }
        public DateTime? collect_datetime { get; set; }
	    public string submtd_by { get; set; }
        public DateTime? submtd_datetime  { get; set; }
	    public int? rcv_by_id { get; set; }
        public string receiver { get; set; }
        public DateTime?  rcv_date { get; set; }
        public DateTime? test_date { get; set; }
	    public bool is_flood_sample { get; set; }
	    public string notes { get; set; }
	    public int? customer_id { get; set; }        
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public string postal_code { get; set; }
        public int? city_id { get; set; }
        public string city { get; set; }
        public int? province_id { get; set; }
        public string province { get; set; }
        public bool? is_public { get; set; }
        public bool? is_approve_financing { get; set; }
        public bool? is_semi_public { get; set; }
        public DateTime? date_entered { get; set; }
        public DateTime? work_date { get; set; }
        public int? transaction_type_id { get; set; }
        public string transaction_type { get; set; }
        public int? report_type_id { get; set; }
        public string report_type { get; set; }
        public string temp { get; set; }
        public int? project_id { get; set; }
        public string project { get; set; }
        public string idnty_name { get; set; }
        public string idnty_location { get; set; }
        public int? sample_type_id { get; set; }
        public string sample_type { get; set; }
        public string sample_legal_loc { get; set; }
        public string town { get; set; }
        public int? rm_id { get; set; }
        public string  rural_municipality { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string utm_x { get; set; }
        public string utm_y { get; set; }
        public string zone { get; set; }
        public string scan_image_filename { get; set; }
        public string condition_comment { get; set; }
        public int? coupon_log_id { get; set; }
        public int? assigned_coupon { get; set; }
        public int? gen_coupon { get; set; }
        public int? class_id { get; set; }
        public string pkg_class { get; set; }
        public DateTime? coupon_issued_date { get; set; }  
        public bool? is_good_condition { get; set; }
        public bool existence { get; set; }
        public bool publish { get; set; }
        public int subsidyimage_id { get; set; }
        public bool is_condition_met { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class testtransactionsview
    {
        public testtransactionsview()
        {
            collect_datetime = null;
            submtd_datetime = null;
            rcv_date = null;
            test_date = null;
            date_entered = null;
            work_date = null;
        }

        public int trans_id { get; set; }

        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        public int test_pkg_id { get; set; }
        public string lab_code { get; set; }

        public int rcv_by_id { get; set; }
        public string receiver { get; set; }

        public int transaction_type_id { get; set; }
        public string transaction_type { get; set; }

        public int project_id { get; set; }
        public string project { get; set; }

        public int sample_type_id { get; set; }
        public string sample_type { get; set; }

        public int class_id { get; set; }
        public string pkg_class { get; set; }

        public DateTime? collect_datetime { get; set; }
        public string submtd_by { get; set; }
        public DateTime? submtd_datetime { get; set; }        
        public DateTime? rcv_date { get; set; }
        public DateTime? test_date { get; set; }
        public bool is_flood_sample { get; set; }        
        public DateTime? date_entered { get; set; }
        public DateTime? work_date { get; set; }        
        public int report_type_id { get; set; }
        public string idnty_name { get; set; }
        public string idnty_location { get; set; }        
        public string sample_legal_loc { get; set; }
        public string town { get; set; }
        public string hl_code { get; set; }
        public bool status { get; set; }
        public int assigned_coupon { get; set; }
        public int gen_coupon { get; set; }
        public string scan_image_filename { get; set; }
        public int subsidyimage_id { get; set; }
        public int email_template_id { get; set; }
        public bool? publish { get; set; }
    }
}

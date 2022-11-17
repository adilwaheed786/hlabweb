using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class test_transaction
    {
        public test_transaction()
        {
            collect_datetime_start = null;
            collect_datetime_end = null;
            submtd_datetime_start = null;
            submtd_datetime_end = null;
            rcv_date_start = null;
            rcv_date_end = null;
            test_date_start = null;
            test_date_end = null;
            date_entered_start = null;
            date_entered_end = null;
            work_date_start = null;
            work_date_end = null;
            project_date_start = null;
            project_date_end = null;
        }
        public bool is_paid { get; set; }
        public int? invoice_id { get; set; }
        public int trans_id { get; set; }

        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        public int test_pkg_id { get; set; }
        public string lab_code { get; set; }

        public int payment_type_id { get; set; }
        public string payment { get; set; }

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

        public DateTime? collect_datetime_start { get; set; }
        public DateTime? collect_datetime_end { get; set; }        
        public DateTime? submtd_datetime_start { get; set; }
        public DateTime? submtd_datetime_end { get; set; }
        public DateTime? rcv_date_start { get; set; }
        public DateTime? rcv_date_end { get; set; }
        public DateTime? test_date_start { get; set; }
        public DateTime? test_date_end { get; set; }              
        public DateTime? date_entered_start { get; set; }
        public DateTime? date_entered_end { get; set; }
        public DateTime? work_date_start { get; set; }
        public DateTime? work_date_end { get; set; }
        public DateTime? project_date_start { get; set; }
        public DateTime? project_date_end { get; set; }
        public string submtd_by { get; set; }
        public string filter { get; set; }
        public bool is_flood_sample { get; set; }
        public bool status { get; set; }
        public int report_type_id { get; set; }      
        public string idnty_name { get; set; }
        public string idnty_location { get; set; }        
        public string sample_legal_loc { get; set; }
        public string town { get; set; }
        public int assigned_coupon { get; set; }
        public int gen_coupon { get; set; }
        public bool? is_good_condition { get; set; }
    }
}

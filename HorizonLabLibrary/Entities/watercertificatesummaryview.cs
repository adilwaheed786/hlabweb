using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class watercertificatesummaryview
    {
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public int project_id { get; set; }
        public int test_pkg_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public DateTime order_date { get; set; }
        public DateTime? rcv_date { get; set; }
        public DateTime? submtd_datetime { get; set; }
        public DateTime? test_date { get; set; }
        public DateTime? min_work_date { get; set; }
        public DateTime? proj_date { get; set; }
        public string date_email_sent { get; set; }
        public string received_by { get; set; }
        public bool proc_status { get; set; }
        public decimal total_amount { get; set; }
        public decimal tax { get; set; }
        public bool record_status { get; set; }
        public string gst_number { get; set; }
        public string lab_code { get; set; }
        public int CountWithTrans { get; set; }
        public int email_template_id { get; set; }
        public int max_subsidy_img_id { get; set; }
        public int msa_horizonid { get; set; }
        public string gen_coupon { get; set; }
        public string scan_image_filename { get; set; }        
        public bool checkbox {
            get { return false; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class temporaryprojectrequestsview
    {
        public int id { get; set; }
        public int request_id { get; set; }
        public int test_pkg_id { get; set; }
        public int project_id { get; set; }
        public int payment_id { get; set; }
        public int received_by_id { get; set; }
        public string temp_fname { get; set; }
        public string  temp_lname { get; set; }
        public string sample_desc { get; set; }
        public string temperature { get; set; }
        public string hl_code_prefix { get; set; }
        public DateTime? date_insert { get; set; }
        public DateTime? collect_datetime { get; set; }
        public DateTime? rcv_date { get; set; }
        public DateTime? test_date { get; set; }
        public DateTime? submitted_datetime { get; set; }
        public int customer_id { get; set; }
        public int temp_customer_id { get; set; }
        public int proj_form_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company_name { get; set; }
        public string hl_code { get; set; }
        public string submitted_by { get; set; }
    }
}

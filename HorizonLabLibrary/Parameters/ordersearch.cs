using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class ordersearch
    {
        public string searchfilter { get; set; }
        public string searchvalue { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public int Project_id { get; set; }
        public DateTime? start_order_date { get; set; }
        public DateTime? end_order_date { get; set; }
        public DateTime? rcv_date_start { get; set; }
        public DateTime? rcv_date_end { get; set; }
        public DateTime? submtd_datetime_start { get; set; }
        public DateTime? submtd_datetime_end { get; set; }
        public DateTime? test_date_start { get; set; }
        public DateTime? test_date_end { get; set; }
        public DateTime? proj_date_start { get; set; }
        public DateTime? proj_date_end { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class RequestSessionParameter
    { 
        public string search_order { get; set; }
        public int request_id { get; set; }
        public int project_id { get; set; }
        public int request_item_id { get; set; }
        public int transaction_id { get; set; }
        public DateTime? request_date_start { get; set; }
        public DateTime? request_date_end { get; set; }
        public DateTime? rcv_date_start { get; set; }
        public DateTime? rcv_date_end { get; set; }
        public DateTime? submtd_datetime_start { get; set; }
        public DateTime? submtd_datetime_end { get; set; }
        public DateTime? test_date_start { get; set; }
        public DateTime? test_date_end { get; set; }
    }
}

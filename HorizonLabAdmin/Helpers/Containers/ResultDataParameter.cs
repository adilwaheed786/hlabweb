using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class ResultDataParameter
    {
        public string coliform_result { get; set; }
        public string ecoli_result { get; set; }
        public string collect_date { get; set; }
        public string collect_time { get; set; }
        public string received_date { get; set; }
        public string test_date { get; set; }
        public string report_date {get;set;}
    }
}

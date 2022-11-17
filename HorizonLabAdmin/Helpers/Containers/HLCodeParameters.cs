using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class HLCodeParameters
    {
        public string hl_code_prefix { get; set; }
        public int request_count_today { get; set; }
        public int customer_request_count { get; set; }
        public int customer_id { get; set; }
        public int package_class_id { get; set; }
        public int test_pkg_id { get; set; }
    }
}

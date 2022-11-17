using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class bulkrequest_params
    {
        public int row_count { get; set; }
        public int payment_id { get; set; }
        public int test_pkg_id { get; set; }
        public int project_id { get; set; }
        public DateTime date_request { get; set; }
    }
}

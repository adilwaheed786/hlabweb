using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class TransactionEmailParam
    {
        public sp_gethorizonlabtransactiondetails transactiondetails { get; set; }
        public hlab_email_templates email_template { get; set; }
        public string request_scheme { get; set; }
        public string reques_host { get; set; }
        public int requets_id { get; set; }
        public int test_pkg_id { get; set; }
        public List<string> subsidy_files { get; set; }
        public List<int> coupons  { get; set; }
    }
}

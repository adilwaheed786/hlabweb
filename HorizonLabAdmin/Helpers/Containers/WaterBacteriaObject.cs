using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class WaterBacteriaObject
    {
        public int? test_package_id { get; set; }
        public decimal? test_package_fee { get; set; }
        public int? customer_id { get; set; }
        public string temperature{ get; set; }
        public string location{ get; set; }
        public string legal_location{ get; set; }
        public string town{ get; set; }
        public string strdate{ get; set; }
        public string strtime{ get; set; }

        public string insert_result{ get; set; }
        public string payment{ get; set; }
        public string amount{ get; set; }
        public string payment_item_label { get; set; }
        public int request_id{ get; set; }
        public int transaction_id{ get; set; }

        public bool proceed_csv_process { get; set; }
        public WaterBacteriaCsvFile current_csv_row { get; set; }
        public WaterBacteriaCsvFile previous_csv_row { get; set; }
        public hlab_test_transactions watersample { get; set; }
        public TestPackageObject test_package_object { get; set; }
    }
}

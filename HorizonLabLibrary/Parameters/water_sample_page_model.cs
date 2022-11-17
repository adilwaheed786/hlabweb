using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class water_sample_page_model
    {
        public hlab_test_transactions hlab_test_transactions { get; set; }
        public hlab_invoice hlab_invoice { get; set; }
        public ordersummaryview orderview { get; set; }
        public orderdetailsview orderitemview { get; set; }
        public List<hlab_test_results> hlab_test_result_list { get; set; }
    }
}

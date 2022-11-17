using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class JsonTestRequestParametercs
    {
        public hlab_order_logs hlab_order_logs { get; set; }
        public hlab_order_items hlab_order_item { get; set; }
        public List<hlab_order_items> hlab_order_items { get; set; }
        public List<hlab_test_payments> hlab_test_payments { get; set; }
    }
}

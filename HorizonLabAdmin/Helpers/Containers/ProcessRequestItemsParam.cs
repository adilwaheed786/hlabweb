using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class ProcessRequestItemsParam
    {
        public List<hlab_order_items> request_item_list { get; set; }
        public int request_id { get; set; }
        public int customer_id { get; set; }
    }
}

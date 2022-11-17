using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class orderpaymentsview
    {
        public int payment_id { get; set; }
        public int order_id { get; set; }
        public int payment_type_id { get; set; }
        public decimal paid_amount { get; set; }
        public DateTime? payment_date { get; set; }
        public string payment { get; set; }
    }
}

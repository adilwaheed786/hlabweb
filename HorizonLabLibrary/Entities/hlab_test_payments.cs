using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_payments
    {
        [Required, Key]
        public int payment_id { get; set; }
        public int order_id { get; set; }
        public int payment_type_id { get; set; }
        public decimal paid_amount { get; set; }
        public DateTime payment_date { get; set; }
    }
}

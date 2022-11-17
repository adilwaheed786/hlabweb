using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_invoice
    {
        [Required, Key]
        public int invoice_id { get; set; }

        public DateTime? invoice_date { get; set; }
        public string paid_by { get; set; }
        public int? payment_option_id { get; set; }
        public int? payment_type_id { get; set; }
        public int? trans_id { get; set; }
        public decimal? paid_amount { get; set; }
        public DateTime? payment_date { get; set; }
    }
}

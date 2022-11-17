using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_order_items
    {
        [Required, Key]
        public int id { get; set; }
        public int? test_pkg_id { get; set; }
        public int? order_id { get; set; }
        public decimal? amount { get; set; }
        public decimal? price { get; set; }
        public int? trans_id { get; set; }
        public int? coupon { get; set; }
        public int? email_tpl_id { get; set; }
        public bool status { get; set; }
        public string hl_code { get; set; }
        public DateTime? date_email_sent { get; set; }
    }
}

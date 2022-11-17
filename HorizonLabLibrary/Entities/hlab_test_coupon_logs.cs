using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_coupon_logs
    {
        [Required, Key]
        public int coupon_log_id { get; set; }
        public DateTime? coupon_issued_date { get; set; }
        public int customer_id { get; set; }
        public int? coupon { get; set; }
        public int? trans_id { get; set; }
    }
}

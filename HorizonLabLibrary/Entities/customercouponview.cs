using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class customercouponview
    {
        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public int city_id { get; set; }
        public string city { get; set; }
        public int province_id { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public int coupon { get; set; }
        public DateTime coupon_issued_date { get; set; }
    }
}

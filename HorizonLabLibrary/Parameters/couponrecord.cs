using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class couponrecord
    {
        public int coupon { get; set; }
        public int customerid { get; set; }
        public DateTime issuedatestart { get; set; }
        public DateTime issuedateend { get; set; }
        public string lastname { get; set; }
    }
}

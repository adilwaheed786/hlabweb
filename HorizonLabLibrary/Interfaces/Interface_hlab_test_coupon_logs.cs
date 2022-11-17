using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_test_coupon_logs
    {
        bool LogCoupon(hlab_test_coupon_logs log);
        int? GenerateCoupon();
        hlab_test_coupon_logs RetrieveCouponLog(int coupon, int customerid);
        List<sp_getcustomercouponrecords> GetCustomerCouponRecord(couponrecord rec);
        bool RemoveCouponLog(int customerid, int coupon);
        bool UpdateCouponLog(hlab_test_coupon_logs log);
    }
}

using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabCouponLogRepository : Interface_hlab_test_coupon_logs
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabCouponLogRepository> _logger;

        public HlabCouponLogRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabCouponLogRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public int? GenerateCoupon()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_coupon_logs.Max(x => x.coupon) + 1;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return 0;
            }
        }

        public List<sp_getcustomercouponrecords> GetCustomerCouponRecord(couponrecord rec)
        {
            try
            {
                if (rec.coupon != 0 && rec.customerid == 0 && string.IsNullOrEmpty(rec.lastname)) return _hlab_Db_Context.sp_getcustomercouponrecords.FromSql($"sp_GetCustomerCouponRecordsByCoupon {rec.coupon}").ToList();
                if (rec.coupon == 0 && rec.customerid != 0 && string.IsNullOrEmpty(rec.lastname)) return _hlab_Db_Context.sp_getcustomercouponrecords.FromSql($"sp_GetCustomerCouponRecordsByCustomerID {rec.customerid}").ToList();
                if (rec.coupon == 0 && rec.customerid == 0 && !string.IsNullOrEmpty(rec.lastname)) return _hlab_Db_Context.sp_getcustomercouponrecords.FromSql($"sp_GetCustomerCouponRecordsByCustomerLastName '{rec.lastname}'").ToList();
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }            
        }

        public bool LogCoupon(hlab_test_coupon_logs log)
        {
            try
            {
                _hlab_Db_Context.hlab_test_coupon_logs.Add(log);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public hlab_test_coupon_logs RetrieveCouponLog(int coupon, int customerid)
        {
            try
            {
                return _hlab_Db_Context.hlab_test_coupon_logs.Where(X => X.coupon == coupon && X.customer_id == customerid).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool RemoveCouponLog(int customerid, int coupon)
        {
            try
            {
                hlab_test_coupon_logs log = new hlab_test_coupon_logs();

                log = _hlab_Db_Context.hlab_test_coupon_logs.Where(x => x.customer_id == customerid && x.coupon == coupon).First();
                _hlab_Db_Context.hlab_test_coupon_logs.Remove(log);
                _hlab_Db_Context.SaveChanges();

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool UpdateCouponLog(hlab_test_coupon_logs log)
        {
            try
            {
                _hlab_Db_Context.hlab_test_coupon_logs.Update(log);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;

            }
        }
    }
}

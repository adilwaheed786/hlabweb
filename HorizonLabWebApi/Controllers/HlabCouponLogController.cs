using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("coupon_log")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabCouponLogController : ControllerBase
    {
        private Interface_hlab_test_coupon_logs _hlabTestCouponLogs;
        private readonly ILogger<HlabTestTransactionsController> _logger;

        public HlabCouponLogController(
            Interface_hlab_test_coupon_logs hlabTestCouponLogs,
            ILogger<HlabTestTransactionsController> logger)
        {
            _hlabTestCouponLogs = hlabTestCouponLogs;
            _logger = logger;
        }

        [HttpGet("generatecoupon")]
        public int? generatecoupon()
        {
            return _hlabTestCouponLogs.GenerateCoupon();
        }

        [HttpGet("removecouponlog")]
        public bool removecouponlog(int customerid, int coupon)
        {
            return _hlabTestCouponLogs.RemoveCouponLog(customerid, coupon);
        }

        [HttpPost("logcoupon")]
        public ActionResult LogCoupon(hlab_test_coupon_logs log)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("LogCoupon : Not a valid model");
                bool result = _hlabTestCouponLogs.LogCoupon(log);
                if (result) return Ok();
                return BadRequest("LogCoupon : UpdateTestResult Error");
            }
            catch (Exception xc)
            {
                return BadRequest("LogCoupon Exception Error: " + xc);
            }
        }

        [HttpPost("getcouponrecords")]
        public List<sp_getcustomercouponrecords> GetCouponRecords(couponrecord rec)
        {
            try
            {
                return _hlabTestCouponLogs.GetCustomerCouponRecord(rec);
            }
            catch (Exception xc)
            {
                _logger.LogError("GetCouponRecords Error: " + xc.InnerException);
                return null;
            }
        }

        [HttpGet("getcouponlog")]
        public hlab_test_coupon_logs getcouponlog(int coupon, int customerid)
        {
            return _hlabTestCouponLogs.RetrieveCouponLog(coupon, customerid);
        }

        [HttpPost("updatecouponlog")]
        public bool UpdateCouponLog(hlab_test_coupon_logs log)
        {
            try
            {
                return _hlabTestCouponLogs.UpdateCouponLog(log);
            }
            catch (Exception xc)
            {
                return false;
            }
        }
    }
}
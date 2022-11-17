using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabCouponLogRepository : Interface_hlab_test_coupon_logs
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabCouponLogLibrary _hllCouponLogApi = new HorizonLabCouponLogLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabCouponLogRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public bool LogCoupon(hlab_test_coupon_logs log)
        {
            var result = _hllCouponLogApi.LogCoupon(log, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public int? GenerateCoupon()
        {
            return Convert.ToInt32(_hllCouponLogApi.GenerateCoupon(_webApibaseUrl, _hlabApiKey, _ApiHeader));
        }

        public hlab_test_coupon_logs RetrieveCouponLog(int coupon, int customerid)
        {
            var jsonList = _hllCouponLogApi.GetCouponLog(coupon, customerid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var couponlog = JsonConvert.DeserializeObject<hlab_test_coupon_logs>(jsonList);
            return couponlog;
        }

        public List<sp_getcustomercouponrecords> GetCustomerCouponRecord(couponrecord rec)
        {
            var jsonList = _hllCouponLogApi.GetCouponRecords(rec, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var couponlist = JsonConvert.DeserializeObject<List<sp_getcustomercouponrecords>>(jsonList);
            return couponlist;
        }

        public bool RemoveCouponLog(int customerid, int coupon)
        {
            return Convert.ToBoolean(_hllCouponLogApi.RemoveCouponLog(customerid, coupon, _webApibaseUrl, _hlabApiKey, _ApiHeader));
        }

        public bool UpdateCouponLog(hlab_test_coupon_logs log)
        {
            return Convert.ToBoolean(_hllCouponLogApi.UpdateCouponLog(log, _webApibaseUrl, _hlabApiKey, _ApiHeader));
        }
    }
}

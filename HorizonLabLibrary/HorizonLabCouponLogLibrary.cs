using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabCouponLogLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/coupon_log";

        public string GenerateCoupon(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/generatecoupon", ApiKey, ApiHeader);
        }

        public string LogCoupon(hlab_test_coupon_logs log, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(log);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/logcoupon/", ApiKey, ApiHeader);
        }

        public string UpdateCouponLog(hlab_test_coupon_logs log, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(log);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatecouponlog/", ApiKey, ApiHeader);
        }

        public string GetCouponRecords(couponrecord record, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(record);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getcouponrecords/", ApiKey, ApiHeader);
        }

        public string GetCouponLog(int coupon, int customerid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getcouponlog?coupon=" + coupon + "&customerid=" + customerid, ApiKey, ApiHeader);
        }

        public string RemoveCouponLog(int customerid, int coupon, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/removecouponlog?customerid=" + customerid + "&coupon=" + coupon, ApiKey, ApiHeader);
        }
    }
}

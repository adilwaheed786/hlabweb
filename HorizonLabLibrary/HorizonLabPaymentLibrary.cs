using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabPaymentLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_payment";

        public string AddPayment(hlab_test_payments payment, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(payment);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addpayment/", ApiKey, ApiHeader);
        }

        public string DeleteBulkPayment(hlab_test_payments payment, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletebulkpayment?orderid=" + orderid, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(payment);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/deletebulkpayment/", ApiKey, ApiHeader);

        }

        public string DeleteOnePayment(hlab_test_payments payment, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deleteonepayment?paymentid=" + paymentid, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(payment);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/deleteonepayment/", ApiKey, ApiHeader);
        }

        public string GetAllPayments(orderpaymentsview orderpayment, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getallpayments?orderid=" + orderid, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(orderpayment);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getallpayments/", ApiKey, ApiHeader);
        }

        public string GetPaymentInfo(orderpaymentsview orderpayment, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getpaymentinfo?paymentid=" + paymentid, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(orderpayment);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getpaymentinfo/", ApiKey, ApiHeader);
        }

        public string UpdatePayment(hlab_test_payments payment, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(payment);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatepayment/", ApiKey, ApiHeader);
        }

        public string GetTestPaymentOptions(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getpaymentoptionlist", ApiKey, ApiHeader);
        }

        public string GetTestPaymentTypes(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getpaymenttypelist", ApiKey, ApiHeader);
        }
    }
}

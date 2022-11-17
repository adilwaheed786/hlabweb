using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabTestPaymentRepository : Interface_hlab_test_payments
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabPaymentLibrary _hllPaymentLib = new HorizonLabPaymentLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestPaymentRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public bool AddPayment(hlab_test_payments payment)
        {
            var result = _hllPaymentLib.AddPayment(payment, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteBulkPayment(hlab_test_payments payment)
        {
            var result = _hllPaymentLib.DeleteBulkPayment(payment, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteOnePayment(hlab_test_payments payment)
        {
            var result = _hllPaymentLib.DeleteOnePayment(payment, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public IEnumerable<orderpaymentsview> GetAllPayments(orderpaymentsview orderpayment)
        {
            var jsonList = _hllPaymentLib.GetAllPayments(orderpayment, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var paymentlist = JsonConvert.DeserializeObject<List<orderpaymentsview>>(jsonList);
            return paymentlist;
        }

        public orderpaymentsview GetPaymentInfo(orderpaymentsview orderpayment)
        {
            var jsonList = _hllPaymentLib.GetPaymentInfo(orderpayment, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var payment = JsonConvert.DeserializeObject<orderpaymentsview>(jsonList);
            return payment;
        }

        public bool UpdatePayment(hlab_test_payments payment)
        {
            var result = _hllPaymentLib.UpdatePayment(payment, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public IEnumerable<hlab_test_payment_options> GetAllPaymentOptions()
        {
            var jsonList = _hllPaymentLib.GetTestPaymentOptions( _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var options = JsonConvert.DeserializeObject<List<hlab_test_payment_options>>(jsonList);
            return options;
        }

        public IEnumerable<hlab_test_payment_types> GetAllPaymentTypes()
        {
            var jsonList = _hllPaymentLib.GetTestPaymentTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var types = JsonConvert.DeserializeObject<List<hlab_test_payment_types>>(jsonList);
            return types;
        }
    }
}

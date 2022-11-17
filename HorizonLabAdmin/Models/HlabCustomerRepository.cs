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
    public class HlabCustomerRepository : Interface_hlab_customers
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private HorizonLabLibrary.HorizonLabCustomerLibrary _hllCustomerLib = new HorizonLabLibrary.HorizonLabCustomerLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabCustomerRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public IEnumerable<horizonlabcustomerview> GetAllCustomers(horizonlabcustomerview htr)
        {
            var jsonList = _hllCustomerLib.GetAllCustomers(htr, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var customerList = JsonConvert.DeserializeObject<List<horizonlabcustomerview>>(jsonList);
            return customerList;
        }

        public IEnumerable<hlab_customer_phone> GetCustomerPhone(hlab_customers customer)
        {
            var jsonList = _hllCustomerLib.GetCustomerPhones(customer, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var phoneList = JsonConvert.DeserializeObject<List<hlab_customer_phone>>(jsonList);
            return phoneList;
        }

        public horizonlabcustomerview GetCustomersDetails(hlab_customers customer)
        {
            var jsonObj = _hllCustomerLib.GetCustomerDetails(customer, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var details = JsonConvert.DeserializeObject<horizonlabcustomerview>(jsonObj);
            return details;
        }

        public int AddCustomer(customerparameters htc)
        {
            var result = _hllCustomerLib.AddNewCustomer(htc, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            return Convert.ToInt32(result);
        }

        public bool UpdateCustomer(customerparameters htc)
        {
            var result = _hllCustomerLib.UpdateCustomer(htc, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteCustomer(hlab_customers customer)
        {
            try
            {
                var jsonList = _hllCustomerLib.DeactivateCustomer(customer, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                return true;
            }
            catch(Exception exc)
            {
                return false;
            }
        }

        public bool CheckIfEmailAssigned(customerparameters customer)
        {
            string result = "";
            result = _hllCustomerLib.CheckEmailIfAssigned(customer, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (result == "true")
            {
                return true;
            }
            else if(result == "false")
            {
                return false;
            }
            return true; //will return true if errors are encountered
        }

        public IEnumerable<hlab_customer_email> GetCustomerEmail(hlab_customers customer)
        {
            var jsonList = _hllCustomerLib.GetCustomerEmails(customer, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var emailList = JsonConvert.DeserializeObject<List<hlab_customer_email>>(jsonList);
            return emailList;
        }

        public List<horizonlabcustomerview> ListCustomersDetails(hlab_customers customer)
        {
            var jsonList = _hllCustomerLib.GetCustomerDetailsByObject(customer, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var customerList = JsonConvert.DeserializeObject<List<horizonlabcustomerview>>(jsonList);
            return customerList;
        }
    }
}

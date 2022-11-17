using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabCustomerLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_customer";

        public string GetCustomerDetails(hlab_customers customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getcustomerdetails?customer_id=" + customer_id, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getcustomerdetails/", ApiKey, ApiHeader);
        }

        public string GetCustomerPhones(hlab_customers customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getcustomerphone?customer_id=" + customer_id, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getcustomerphone/", ApiKey, ApiHeader);

        }

        public string GetCustomerEmails(hlab_customers customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getcustomeremail?customer_id=" + customer_id, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getcustomeremail/", ApiKey, ApiHeader);
        }

        public string GetAllCustomers(horizonlabcustomerview customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getcustomerlist/", ApiKey, ApiHeader);
        }

        public string GetCustomerDetailsByObject(hlab_customers customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/getcustomerdetailsbyobject/", ApiKey, ApiHeader);
        }

        public string CheckEmailIfAssigned(customerparameters customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/isemailassigned/", ApiKey, ApiHeader);
        }

        public string AddNewCustomer(customerparameters customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/addnewcustomer/", ApiKey, ApiHeader);
        }

        public string UpdateCustomer(customerparameters customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatecustomer/", ApiKey, ApiHeader);
        }

        public string DeactivateCustomer(hlab_customers customer, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deactivatecustomer?customer_id=" + customer_id, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(customer);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/deactivatecustomer/", ApiKey, ApiHeader);
        }

        public string EmailDocuments(emaildetails emaildetails, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(emaildetails);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/emaildocs/", ApiKey, ApiHeader);
        }
    }
}

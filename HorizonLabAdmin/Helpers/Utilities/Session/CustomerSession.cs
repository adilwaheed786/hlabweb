using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities.Session
{
    public class CustomerSession: UserSession, ICustomerSession
    {
        private readonly ILogger<CustomerSession> _logger;
        private readonly string key_search_customer_id = "search_customerId";
        private readonly string key_search_customer_firstname = "search_customerfirstName";
        private readonly string key_search_customer_lastname = "search_customerlastName";
        private readonly string key_search_company = "search_company";
        private readonly string key_search_customer_address = "search_customer_address";
        private readonly string key_search_customer_status = "search_customer_status";
        private readonly string key_search_customer_email = "search_customer_email";

        public CustomerSession (IHttpContextAccessor _httpContextAccessor, ILogger<CustomerSession> logger) : base(_httpContextAccessor, logger)
        {
            _logger = logger;
        }

        public void SetSearchCustomerSessionInfo(hlab_customers customer)
        {
            SetIntSession(new IntSessionParameter { Key = key_search_customer_id, Value = customer.customer_id });
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_search_customer_firstname, Value = customer.first_name});
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_search_customer_lastname, Value = customer.last_name});
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_search_customer_address, Value = customer.street});
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_search_company, Value = customer.company_name});
            SetBooleanSessionWithNullValidation(new BooleanSessionParameter { Key = key_search_customer_status, Value = customer.status});
        }

        public int GetSearchCustomerId()
        {
            return GetSessionIntValue(key_search_customer_id);
        }

        public string GetSearchCustomerFirstName()
        {
            return GetSessionStringValue(key_search_customer_firstname);
        }

        public string GetSearchCustomerLastName()
        {
            return GetSessionStringValue(key_search_customer_lastname);
        }

        public bool IsSearchCustomerFirstNameHasValue()
        {
            return IsSessionStringHasValue(key_search_customer_firstname);
        }

        public bool IsSearchCustomerLastNameHasValue()
        {
            return IsSessionStringHasValue(key_search_customer_lastname);
        }

        public void SetSearchCustomerSessionEmail(string email)
        {
            SetStringSessionWithNullValidation(new StringSessionParameter { Key = key_search_customer_email, Value = email });
        }

        public bool GetBooleanCustomerRecordStatus()
        {
            return GetSessionBooleanValue(key_search_customer_status);
        }

        public string GetSearchCustomerAddress()
        {
            return GetSessionStringValue(key_search_customer_address);
        }

        public bool IsCustomerSessionSearchHasValues()
        {
            try
            {
                if (!string.IsNullOrEmpty(GetSearchCustomerFirstName())
                    || !string.IsNullOrEmpty(GetSearchCustomerLastName())
                    || !string.IsNullOrEmpty(GetSearchCustomerEmail())
                    || !string.IsNullOrEmpty(GetSearchCustomerAddress())
                    || !string.IsNullOrEmpty(GetSearchCompanyName())
                    || GetSearchCustomerId() != 0
                    ) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"CustomerSession > IsCustomerSessionSearchHasValues(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public string GetSearchCustomerEmail()
        {
            return GetSessionStringValue(key_search_customer_email);
        }

        public horizonlabcustomerview GenerateCustomerObjectFromSession()
        {
            try
            {
                horizonlabcustomerview customer = new horizonlabcustomerview();
                customer.customer_id = GetSearchCustomerId();
                if (!string.IsNullOrEmpty(GetSearchCustomerFirstName())) customer.first_name = GetSearchCustomerFirstName();
                if (!string.IsNullOrEmpty(GetSearchCustomerLastName())) customer.last_name = GetSearchCustomerLastName();
                if (!string.IsNullOrEmpty(GetSearchCustomerEmail())) customer.email = GetSearchCustomerEmail();
                if (!string.IsNullOrEmpty(GetSearchCustomerAddress())) customer.street = GetSearchCustomerAddress();
                if (!string.IsNullOrEmpty(GetSearchCompanyName())) customer.company_name = GetSearchCompanyName();
                customer.status = GetBooleanCustomerRecordStatus();
                return customer;
            }
            catch (Exception exc)
            {
                _logger.LogError($"CustomerSession > GenerateCustomerObjectFromSession(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public void SetIntSearchCustomerId(int customerid)
        {
            SetIntSession(new IntSessionParameter { Key = key_search_customer_id, Value = customerid });
        }

        public string GetSearchCompanyName()
        {
            return GetSessionStringValue(key_search_company);
        }
    }
}

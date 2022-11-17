using HorizonLabLibrary.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Helper
{
    public class CustomerHelper
    {
        private readonly ILogger<CustomerHelper> _logger;

        public CustomerHelper(ILogger<CustomerHelper> logger)
        {
            _logger = logger;
        }

        public bool IsCompanyNameOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (!string.IsNullOrEmpty(customer.company_name)
                    && string.IsNullOrEmpty(customer.first_name)
                    && string.IsNullOrEmpty(customer.last_name)
                    && string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsCompanyNameOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsFirstNameOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.company_name)
                    && !string.IsNullOrEmpty(customer.first_name)
                    && string.IsNullOrEmpty(customer.last_name)
                    && string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsCompanyNameOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsLastNameOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.company_name)
                    && string.IsNullOrEmpty(customer.first_name)
                    && !string.IsNullOrEmpty(customer.last_name)
                    && string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsCompanyNameOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsStreetAddressOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.company_name)
                    && string.IsNullOrEmpty(customer.first_name)
                    && string.IsNullOrEmpty(customer.last_name)
                    && !string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsCompanyNameOnly() {exc.ToString()}");
                return false;
            }
        }
        ///...////
        public bool IsSetCompanyFirstLastNameOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (!string.IsNullOrEmpty(customer.company_name)
                    && !string.IsNullOrEmpty(customer.first_name)
                    && !string.IsNullOrEmpty(customer.last_name)
                    && string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsSetCompanyFirstLastNameOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsSetFirstLastNameOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.company_name)
                    && !string.IsNullOrEmpty(customer.first_name)
                    && !string.IsNullOrEmpty(customer.last_name)
                    && string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsSetFirstLastNameOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsSetFirstLastNameStreetOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.company_name)
                    && !string.IsNullOrEmpty(customer.first_name)
                    && !string.IsNullOrEmpty(customer.last_name)
                    && !string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsSetFirstLastNameStreetOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsSetCompanyStreetOnly(horizonlabcustomerview customer)
        {
            try
            {
                if (!string.IsNullOrEmpty(customer.company_name)
                    && string.IsNullOrEmpty(customer.first_name)
                    && string.IsNullOrEmpty(customer.last_name)
                    && !string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsSetCompanyStreetOnly() {exc.ToString()}");
                return false;
            }
        }

        public bool IsSetCompanyFirstLastNameStreet(horizonlabcustomerview customer)
        {
            try
            {
                if (!string.IsNullOrEmpty(customer.company_name)
                    && !string.IsNullOrEmpty(customer.first_name)
                    && !string.IsNullOrEmpty(customer.last_name)
                    && !string.IsNullOrEmpty(customer.street))
                    return true;

                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > IsSetCompanyFirstLastNameStreet() {exc.ToString()}");
                return false;
            }
        }
    }
}

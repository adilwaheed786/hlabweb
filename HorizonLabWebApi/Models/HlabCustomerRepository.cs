using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.Helper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabCustomerRepository : CustomerHelper, Interface_hlab_customers
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabCustomerRepository> _logger;

        public HlabCustomerRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabCustomerRepository> logger): base(logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public int AddCustomer(customerparameters hc)
        {
            try
            {
                _hlab_Db_Context.hlab_customers.Add(hc.hlab_customers);
                _hlab_Db_Context.SaveChanges();

                if (hc.phone_list.Count > 0)
                {                    
                    foreach (var phone in hc.phone_list)
                    {
                        if (!string.IsNullOrEmpty(phone.phone))
                        {
                            phone.customer_id = hc.hlab_customers.customer_id;
                            _hlab_Db_Context.hlab_customer_phone.Add(phone);
                            _hlab_Db_Context.SaveChanges();
                        }
                    }
                }                    

                if(hc.email_list.Count > 0)
                {
                    foreach (var email in hc.email_list)
                    {
                        if (!string.IsNullOrEmpty(email.email))
                        {
                            email.customer_id = hc.hlab_customers.customer_id;
                            _hlab_Db_Context.hlab_customer_email.Add(email);
                            _hlab_Db_Context.SaveChanges();
                        }
                    }
                }
                
                return hc.hlab_customers.customer_id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return 0;
            }
        }

        public bool DeleteCustomer(hlab_customers customer)
        {
            try
            {
                var hlab_customer = new hlab_customers { customer_id = customer.customer_id, status = false };
                _hlab_Db_Context.hlab_customers.Attach(hlab_customer).Property(x => x.status).IsModified = true;
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<horizonlabcustomerview> GetAllCustomers(horizonlabcustomerview customer)
        {
            try
            {
                List<horizonlabcustomerview> list = new List<horizonlabcustomerview>();
                if (customer.customer_id != 0)
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.customer_id == customer.customer_id && x.status == customer.status).ToList();
                else if (!string.IsNullOrEmpty(customer.email))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.email.ToLower() == customer.email.ToLower() && x.status == customer.status).ToList();
                else if (IsCompanyNameOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.company_name.Contains(customer.company_name) && x.status == customer.status).ToList();
                else if (IsFirstNameOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.first_name.StartsWith(customer.first_name) && x.status == customer.status).ToList();
                else if (IsLastNameOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.last_name.StartsWith(customer.last_name) && x.status == customer.status).ToList();
                else if (IsStreetAddressOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.street.ToLower().Contains(customer.street.ToLower()) && x.status == customer.status).ToList();
                else if (IsSetCompanyFirstLastNameOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(
                        x => x.company_name.ToLower().Contains(customer.company_name.ToLower())
                        && x.first_name.ToLower().StartsWith(customer.first_name.ToLower())
                        && x.last_name.ToLower().StartsWith(customer.last_name.ToLower())
                        && x.status == customer.status
                        ).ToList();
                else if (IsSetFirstLastNameOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(
                        x => x.first_name.ToLower().StartsWith(customer.first_name.ToLower())
                        && x.last_name.ToLower().StartsWith(customer.last_name.ToLower())
                        && x.status == customer.status
                        ).ToList();
                else if (IsSetFirstLastNameStreetOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(
                        x => x.first_name.ToLower().StartsWith(customer.first_name.ToLower())
                        && x.last_name.ToLower().StartsWith(customer.last_name.ToLower())
                        && x.street.ToLower().Contains(customer.street.ToLower())
                        && x.status == customer.status
                        ).ToList();
                else if (IsSetCompanyStreetOnly(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(
                        x => x.company_name.ToLower().Contains(customer.company_name.ToLower())
                        && x.street.ToLower().Contains(customer.street.ToLower())
                        && x.status == customer.status
                        ).ToList();
                else if (IsSetCompanyFirstLastNameStreet(customer))
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(
                        x => x.company_name.ToLower().Contains(customer.company_name.ToLower())
                        && x.first_name.ToLower().StartsWith(customer.first_name.ToLower())
                        && x.last_name.ToLower().StartsWith(customer.last_name.ToLower())
                        && x.street.ToLower().Contains(customer.street.ToLower())
                        && x.status == customer.status
                        ).ToList();
                else
                    list = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.status == customer.status).ToList();

                return list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > GetAllCustomers() {exc.ToString()}");
                return null;
            }
        }

        //checks if email is already assigned to other customer
        public bool CheckIfEmailAssigned(customerparameters customer)
        {
            try
            {
                bool ifExists = false;
                List<hlab_customer_email> email_records = new List<hlab_customer_email>();
                foreach(var email in customer.email_list)
                {
                    if (!string.IsNullOrEmpty(email.email))
                    {
                        if (email.customer_id != 0)
                        {
                            email_records = _hlab_Db_Context.hlab_customer_email.Where(x => x.email.ToLower() == email.email.ToLower() && x.customer_id != email.customer_id).ToList();
                        }
                        else
                        {
                            email_records = _hlab_Db_Context.hlab_customer_email.Where(x => x.email.ToLower() == email.email.ToLower()).ToList();
                        }
                        if (email_records.Count > 0) ifExists = true;
                    }                                        
                }                                
                return ifExists;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > CheckIfEmailAssigned() {exc.ToString()}");
                return false;
            }
            
        }

        public IEnumerable<hlab_customer_phone> GetCustomerPhone(hlab_customers customer)
        {
            try
            {
                if (customer.customer_id != 0) return _hlab_Db_Context.hlab_customer_phone.Where(x => x.customer_id == customer.customer_id).ToList();
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > GetCustomerPhone() {exc.ToString()}");
                return null;
            }
        }

        public IEnumerable<hlab_customer_email> GetCustomerEmail(hlab_customers customer)
        {
            try
            {
                if (customer.customer_id != 0) return _hlab_Db_Context.hlab_customer_email.Where(x => x.customer_id == customer.customer_id).ToList();
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > GetCustomerEmail() {exc.ToString()}");
                return null;
            }
        }

        public horizonlabcustomerview GetCustomersDetails(hlab_customers customer)
        {
            try
            {
                if (customer.customer_id != 0) return _hlab_Db_Context.horizonlabcustomerview.Where(x => x.customer_id == customer.customer_id).FirstOrDefault();
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > GetCustomersDetails() {exc.ToString()}");
                return null;
            }

        }

        public bool UpdateCustomer(customerparameters hc)
        {
            try
            {
                _hlab_Db_Context.hlab_customers.Update(hc.hlab_customers);
                _hlab_Db_Context.SaveChanges();
               
                if(hc.phone_list.Count > 0)
                {
                    _hlab_Db_Context.RemoveRange(_hlab_Db_Context.hlab_customer_phone.Where(x => x.customer_id == hc.hlab_customers.customer_id));
                    _hlab_Db_Context.SaveChanges();

                    foreach (var phone in hc.phone_list)
                    {
                        if (string.IsNullOrEmpty(phone.phone)) continue;
                        phone.customer_id = hc.hlab_customers.customer_id;
                        _hlab_Db_Context.hlab_customer_phone.Add(phone);
                        _hlab_Db_Context.SaveChanges();

                    }
                }
                if (hc.email_list.Count > 0)
                {
                    _hlab_Db_Context.RemoveRange(_hlab_Db_Context.hlab_customer_email.Where(x => x.customer_id == hc.hlab_customers.customer_id));
                    _hlab_Db_Context.SaveChanges();

                    foreach (var email in hc.email_list)
                    {
                        if (string.IsNullOrEmpty(email.email)) continue;
                        email.customer_id = hc.hlab_customers.customer_id;
                        _hlab_Db_Context.hlab_customer_email.Add(email);
                        _hlab_Db_Context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > UpdateCustomer() {exc.ToString()}");
                return false;
            }
        }

        public List<horizonlabcustomerview> ListCustomersDetails(hlab_customers customer)
        {
            try
            {
                List<horizonlabcustomerview> customers = new List<horizonlabcustomerview>();
                customers = _hlab_Db_Context.horizonlabcustomerview.Where(x => x.last_name.ToLower() == customer.last_name.ToLower()).ToList();

                if(customers!=null && customers.Count > 0)
                {
                    customers = _hlab_Db_Context.horizonlabcustomerview.Where(y => y.first_name.ToLower() == customer.first_name.ToLower()).ToList();
                }
                    
                return customers;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabCustomerRepository > ListCustomersDetails() {exc.ToString()}");
                return null;
            }
        }
    }
}

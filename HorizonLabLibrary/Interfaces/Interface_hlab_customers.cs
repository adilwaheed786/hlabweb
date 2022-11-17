using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_customers
    {
        IEnumerable<horizonlabcustomerview> GetAllCustomers(horizonlabcustomerview htr);
        IEnumerable<hlab_customer_phone> GetCustomerPhone(hlab_customers customer);
        IEnumerable<hlab_customer_email> GetCustomerEmail(hlab_customers customer);
        horizonlabcustomerview GetCustomersDetails(hlab_customers customer);
        List<horizonlabcustomerview> ListCustomersDetails(hlab_customers customer);
        int AddCustomer(customerparameters htc);
        bool UpdateCustomer(customerparameters htc);
        bool DeleteCustomer(hlab_customers customer);
        bool CheckIfEmailAssigned(customerparameters customer);
    }
}

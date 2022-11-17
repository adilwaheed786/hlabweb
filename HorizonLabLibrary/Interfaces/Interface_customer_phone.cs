using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_customer_phone
    {
        IEnumerable<hlab_customer_phone> GetAllCustomerPhones(int customerId);
    }
}

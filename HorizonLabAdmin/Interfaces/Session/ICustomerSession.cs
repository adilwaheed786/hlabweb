using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces.Session
{
    public interface ICustomerSession
    {
        void SetSearchCustomerSessionInfo(hlab_customers customer);
        void SetIntSearchCustomerId(int customerid);
        void SetSearchCustomerSessionEmail(string email);
        int GetSearchCustomerId();
        horizonlabcustomerview GenerateCustomerObjectFromSession();
        string GetSearchCustomerFirstName();
        string GetSearchCustomerLastName();
        string GetSearchCustomerAddress();
        string GetSearchCustomerEmail();
        string GetSearchCompanyName();
        bool IsSearchCustomerFirstNameHasValue();
        bool IsSearchCustomerLastNameHasValue();
        bool GetBooleanCustomerRecordStatus();
        bool IsCustomerSessionSearchHasValues();
    }
}

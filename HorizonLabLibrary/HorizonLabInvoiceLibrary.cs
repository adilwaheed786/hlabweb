using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabInvoiceLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_invoice";

        public string GetTransactionInvoice(sp_gethorizonlabtransactioninvoices invoice, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettransactioninvoice?transid=" + transid, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(invoice);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/gettransactioninvoice/", ApiKey, ApiHeader);
        }

        public string UpdateInvoiceChages(hlab_invoice invoice, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(invoice);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateinvoice/", ApiKey, ApiHeader);
        }

        public string AddNewInvoice(hlab_invoice invoice, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(invoice);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/insertnewinvoice/", ApiKey, ApiHeader);
        }
    }
}

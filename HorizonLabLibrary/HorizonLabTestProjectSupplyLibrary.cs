using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTestProjectSupplyLibrary
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_test_project_supplies";

        public string DeleteTransactionSupplies(int proj_form_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletetransactionsupplies?proj_form_id=" + proj_form_id, ApiKey, ApiHeader);
        }

        public string AddTransactionSupplies(project_supply_form param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtransactionsupplies/", ApiKey, ApiHeader);
        }
    }
}

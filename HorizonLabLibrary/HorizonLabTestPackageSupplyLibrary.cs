using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTestPackageSupplyLibrary
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_package_supply";

        public string AddSupplyList(test_pkg_supply_param supplylist, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(supplylist);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addsupplylist/", ApiKey, ApiHeader);
        }

        public string AddNewSupply(hlab_supplies object_parameter, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(object_parameter);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addnewsupply/", ApiKey, ApiHeader);
        }

        public string DeleteTestPackageSupplyList(int supply_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletetestpackagesupply?supply_id=" + supply_id, ApiKey, ApiHeader);
        }

        public string DeleteSupply(int supplyid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletesupply?supplyid=" + supplyid, ApiKey, ApiHeader);
        }

        public string GetAllTestPackageSupplies(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getallsupplies", ApiKey, ApiHeader);
        }

        public string GetFilteredTestPackageSupplies(testpackagesupplyview object_parameter, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(object_parameter);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getfilteredsupplies/", ApiKey, ApiHeader);
        }

        public string UpdateSupply(hlab_supplies object_parameter, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(object_parameter);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatesupply/", ApiKey, ApiHeader);
        }
    }
}

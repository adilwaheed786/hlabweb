using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabSuppliesRepository : Interface_hlab_supplies
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabTestPackageSupplyLibrary _hllSupplyLibrary = new HorizonLabTestPackageSupplyLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabSuppliesRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public IEnumerable<hlab_supplies> GetAllTestPackageSupplies()
        {
            var jsonList = _hllSupplyLibrary.GetAllTestPackageSupplies(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var list_supply = JsonConvert.DeserializeObject<List<hlab_supplies>>(jsonList);
            return list_supply;
        }

        public IEnumerable<testpackagesupplyview> GetFilteredTestPackageSupplies(testpackagesupplyview object_parameter)
        {
            var jsonList = _hllSupplyLibrary.GetFilteredTestPackageSupplies(object_parameter, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var list_supply = JsonConvert.DeserializeObject<List<testpackagesupplyview>>(jsonList);
            return list_supply;
        }

        public bool AddNewSupply(hlab_supplies object_parameter)
        {
            var result = _hllSupplyLibrary.AddNewSupply(object_parameter, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool UpdateSupply(hlab_supplies object_parameter)
        {
            var result = _hllSupplyLibrary.UpdateSupply(object_parameter, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteTestPackageSupplies(int test_pkg_id)
        {
            var result = _hllSupplyLibrary.DeleteTestPackageSupplyList(test_pkg_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool AddTestPackageSupplies(test_pkg_supply_param parameter)
        {
            var result = _hllSupplyLibrary.AddSupplyList(parameter, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteSupply(int supplyid)
        {
            var result = _hllSupplyLibrary.DeleteSupply(supplyid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}

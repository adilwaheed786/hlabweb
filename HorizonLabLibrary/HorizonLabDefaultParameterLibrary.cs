using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabDefaultParameterLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_default_parameter";

        public string GetDefaultParameters(int package_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getdefaultparameters?package_id=" + package_id, ApiKey, ApiHeader);
        }

        public string AddDefaultParam(hlab_test_default_pkg_params param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/adddefaultparam/", ApiKey, ApiHeader);
        }

        public string DeleteDefaultParam(int paramid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletedefaultparam?paramid=" + paramid, ApiKey, ApiHeader);
        }
    }
}

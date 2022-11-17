using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabServiceApiLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();

        private string hlab_api_service_controller_name = "/hlab_services";

        public string AddHlabService(hlab_services service, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(service);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_service_controller_name + "/addnewhorizonlabservicepost/", ApiKey, ApiHeader);
        }

        public string UpdateHlabServicePost(hlab_services service, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(service);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_service_controller_name + "/commithorizonlabservicechanges/", ApiKey, ApiHeader);
        }

        public string CommitServiceHeaderChanges(IEnumerable<hlab_web_services_intro> introList, string baseurl, string ApiKey, string ApiHeader) {
            var dataAsString = JsonConvert.SerializeObject(introList);
            return _hllWebApi.CommitPostAction(dataAsString, baseurl + hlab_api_service_controller_name + "/commithorizonlabserviceheaderchanges/", ApiKey, ApiHeader);
        }

        public string GetAllServices(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallservices", ApiKey, ApiHeader);
        }

        public string GetActiveServices(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallactiveservices", ApiKey, ApiHeader);
        }

        public string GetServiceHeader(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getserviceheader", ApiKey, ApiHeader);
        }
    }
}

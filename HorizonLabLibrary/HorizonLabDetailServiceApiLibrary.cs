using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabDetailServiceApiLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();

        private string hlab_api_service_controller_name = "/hlab_services";

        public string GetAllServiceDetails(int service_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getallservicedetails" + "/?service_id=" + service_id.ToString(), ApiKey, ApiHeader);
        }

        public string GetADetailInfo(int detail_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_service_controller_name + "/getdetailinfo" + "/?detail_id=" + detail_id.ToString(), ApiKey, ApiHeader);
        }

        public string DeleteDetailService(hlab_service_details detail, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(detail);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_service_controller_name + "/deleteservicedetail/", ApiKey, ApiHeader);
        }       

        public string UpdateServiceDetail(hlab_service_details detail, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(detail);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_service_controller_name + "/commitservicedetailchanges/", ApiKey, ApiHeader);
        }

        public string AddServiceDetail(hlab_service_details detail, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(detail);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_service_controller_name + "/addnewservicedetailpost/", ApiKey, ApiHeader);
        }
    }
}

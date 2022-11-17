using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabServiceDetailsRepository : Interface_hlab_service_details
    {
        private HorizonLabLibrary.HorizonLabServiceApiLibrary _hllServiceApi = new HorizonLabLibrary.HorizonLabServiceApiLibrary();
        private HorizonLabLibrary.HorizonLabDetailServiceApiLibrary _hllDetailServiceApi = new HorizonLabLibrary.HorizonLabDetailServiceApiLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabServiceDetailsRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }


        public IEnumerable<hlab_service_details> GetAllServiceDetails(int id)
        {
            var jsonDetailServiceList = _hllDetailServiceApi.GetAllServiceDetails(id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var detailserviceList = JsonConvert.DeserializeObject<List<hlab_service_details>>(jsonDetailServiceList);
            return detailserviceList;
        }

        public hlab_service_details GetServiceDetailInfo(int id)
        {
            var jsonDetailServiceList = _hllDetailServiceApi.GetADetailInfo(id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var detailserviceList = JsonConvert.DeserializeObject<hlab_service_details>(jsonDetailServiceList);
            return detailserviceList;
        }

        public bool AddNewServiceDetail(hlab_service_details detail)
        {
            string result = _hllDetailServiceApi.AddServiceDetail(detail, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (result == "success")
            {
                return true;
            }
            return false;
        }

        public bool UpdateServiceDetail(hlab_service_details detail)
        {
            string result = _hllDetailServiceApi.UpdateServiceDetail(detail, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (result == "success")
            {
                return true;
            }
            return false;
        }

        public bool DeleteServiceDetail(hlab_service_details detail)
        {
            string result = _hllDetailServiceApi.DeleteDetailService(detail, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (result == "success")
            {
                return true;
            }
            return false;
        }
    }
}

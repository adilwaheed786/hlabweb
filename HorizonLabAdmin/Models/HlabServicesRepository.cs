using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Models.Forms;

namespace HorizonLabAdmin.Models
{
    public class HlabServicesRepository : Interface_hlab_services
    {
        private HorizonLabLibrary.HorizonLabServiceApiLibrary _hllServiceApi = new HorizonLabLibrary.HorizonLabServiceApiLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabServicesRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public int AddNewService(hlab_services service_info)
        {
            try
            {
                string result = _hllServiceApi.AddHlabService(service_info, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                if (result != "0")
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            catch(Exception exc)
            {
                return 0;
            }
        }

        public bool DeleteService(hlab_services service)
        {
            service.status = false;
            string result = _hllServiceApi.UpdateHlabServicePost(service, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if(result == "success")
            {
                return true;
            }
            return false;
        }

        public IEnumerable<hlab_services> GetActiveServices()
        {
            var jsonServiceList = _hllServiceApi.GetActiveServices(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var serviceList = JsonConvert.DeserializeObject<List<hlab_services>>(jsonServiceList);
            return serviceList;
        }

        public List<hlab_web_services_intro> GetServiceHeader()
        {
            var jsonHeaderList = _hllServiceApi.GetServiceHeader(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var headerList = JsonConvert.DeserializeObject<List<hlab_web_services_intro>>(jsonHeaderList);
            return headerList;
        }

        public bool UpdateServiceHeader(IEnumerable<hlab_web_services_intro> headerList)
        {
            var result = _hllServiceApi.CommitServiceHeaderChanges(headerList, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success") {
                    return true;
                }                
                return false;
            }
            return false;
        }

        public hlab_services GetServiceInfo(int id)
        {
            var jsonServices = _hllServiceApi.GetAllServices(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var serviceList = JsonConvert.DeserializeObject<List<hlab_services>>(jsonServices);
            return serviceList.FirstOrDefault(x => x.id==id);
        }        

        public bool UpdateService(hlab_services service_info)
        {
            var result = _hllServiceApi.UpdateHlabServicePost(service_info, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

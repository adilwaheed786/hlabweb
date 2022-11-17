using HorizonLabLibrary;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabTestProjectSupplyRepo: Interface_hlab_project_supply
    {
        private HorizonLabTestProjectSupplyLibrary _hlabTestProjectsSupplyApi = new HorizonLabTestProjectSupplyLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestProjectSupplyRepo(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public bool AddProjectSupplies(project_supply_form param)
        {
            var result = _hlabTestProjectsSupplyApi.AddTransactionSupplies(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteProjectSupplies(int proj_form_id)
        {
            var result = _hlabTestProjectsSupplyApi.DeleteTransactionSupplies(proj_form_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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
    }
}

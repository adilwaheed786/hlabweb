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
    public class HlabDefaultParameterRepository : Interface_hlab_test_default_pkg_params
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private HorizonLabLibrary.HorizonLabDefaultParameterLibrary _hllDefaultParam = new HorizonLabLibrary.HorizonLabDefaultParameterLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabDefaultParameterRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public bool AssignDefaultParam(hlab_test_default_pkg_params param)
        {
            var result = _hllDefaultParam.AddDefaultParam(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteDefaultParam(hlab_test_default_pkg_params param)
        {
            var result = _hllDefaultParam.DeleteDefaultParam(param.id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public List<sp_getdefaultpackageparameters> GetDefaultTestParams(int packageid)
        {
            var jsonList = _hllDefaultParam.GetDefaultParameters(packageid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var paramlist = JsonConvert.DeserializeObject<List<sp_getdefaultpackageparameters>>(jsonList);
            return paramlist;
        }
    }
}

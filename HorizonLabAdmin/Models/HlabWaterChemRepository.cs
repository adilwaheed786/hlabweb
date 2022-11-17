using HorizonLabLibrary;
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
    public class HlabWaterChemRepository : Interface_water_chem_result
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabWaterChemLibrary _hllWaterChemApi = new HorizonLabWaterChemLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabWaterChemRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        //ADD
        public bool AddTraceMetalResults(hlab_trace_metal_results test_result)
        {
            var result = _hllWaterChemApi.InsertTraceMetalsResult(test_result, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool AddWaterChemA(hlab_chem_water_results_set_a test_result)
        {
            var result = _hllWaterChemApi.InsertWaterChemResults_A(test_result, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool AddWaterChemB(hlab_chem_water_results_set_b test_result)
        {
            var result = _hllWaterChemApi.InsertWaterChemResults_B(test_result, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        //DELETE
        public bool DeleteReseedTraceMetalResults(int transid)
        {
            var result = _hllWaterChemApi.RemoveReseedTraceMetalResults(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result.ToLower() == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteReseedWaterChemA(int transid)
        {
            var result = _hllWaterChemApi.RemoveReseedWaterChemResults_A(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result.ToLower() == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteReseedWaterChemB(int transid)
        {
            var result = _hllWaterChemApi.RemoveReseedWaterChemResults_B(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result.ToLower() == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //SELECT
        public IEnumerable<hlab_trace_metal_results> GetTraceMetalResults(int transid)
        {
            var jsonList = _hllWaterChemApi.TracveMetals_Results(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var testresultlist = JsonConvert.DeserializeObject<List<hlab_trace_metal_results>>(jsonList);
            return testresultlist;
        }

        public IEnumerable<hlab_chem_water_results_set_a> GetWaterChemResultA(int transid)
        {
            var jsonList = _hllWaterChemApi.WaterChemSet_A_Results(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var testresultlist = JsonConvert.DeserializeObject<List<hlab_chem_water_results_set_a>>(jsonList);
            return testresultlist;
        }

        public IEnumerable<hlab_chem_water_results_set_b> GetWaterChemResultB(int transid)
        {
            var jsonList = _hllWaterChemApi.WaterChemSet_B_Results(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var testresultlist = JsonConvert.DeserializeObject<List<hlab_chem_water_results_set_b>>(jsonList);
            return testresultlist;
        }

        //UPDATE
        public bool UpdateTraceMetalResults(hlab_trace_metal_results test_result)
        {
            var result = _hllWaterChemApi.ExecuteUpdateTraceMetalResults(test_result, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool UpdateWaterChemA(hlab_chem_water_results_set_a test_result)
        {
            var result = _hllWaterChemApi.ExecuteUpdateWaterChemResults_A(test_result, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool UpdateWaterChemB(hlab_chem_water_results_set_b test_result)
        {
            var result = _hllWaterChemApi.ExecuteUpdateWaterChemResults_B(test_result, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabWaterChemLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_water_chem";

        //SELECT
        public string WaterChemSet_A_Results(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getwaterchemresulta?transid=" + transid, ApiKey, ApiHeader);
        }

        public string WaterChemSet_B_Results(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getwaterchemresultb?transid=" + transid, ApiKey, ApiHeader);
        }

        public string TracveMetals_Results(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettracemetalresults?transid=" + transid, ApiKey, ApiHeader);
        }

        //ADD
        public string InsertWaterChemResults_A(hlab_chem_water_results_set_a result, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(result);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addwaterchema/", ApiKey, ApiHeader);
        }

        public string InsertWaterChemResults_B(hlab_chem_water_results_set_b result, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(result);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addwaterchemb/", ApiKey, ApiHeader);
        }

        public string InsertTraceMetalsResult(hlab_trace_metal_results result, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(result);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtracemetalresults/", ApiKey, ApiHeader);
        }

        //UPDATE
        public string ExecuteUpdateWaterChemResults_A(hlab_chem_water_results_set_a result, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(result);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatewaterchema/", ApiKey, ApiHeader);
        }

        public string ExecuteUpdateWaterChemResults_B(hlab_chem_water_results_set_b result, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(result);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatewaterchemb/", ApiKey, ApiHeader);
        }

        public string ExecuteUpdateTraceMetalResults(hlab_trace_metal_results result, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(result);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatetracemetalresults/", ApiKey, ApiHeader);
        }

        //DELETE
        public string RemoveReseedWaterChemResults_A(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletereseedwaterchema?transid=" + transid, ApiKey, ApiHeader);
        }

        public string RemoveReseedWaterChemResults_B(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletereseedwaterchemb?transid=" + transid, ApiKey, ApiHeader);
        }

        public string RemoveReseedTraceMetalResults(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletereseedtracemetalresults?transid=" + transid, ApiKey, ApiHeader);
        }
    }
}

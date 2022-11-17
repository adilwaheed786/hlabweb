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
    public class HlabTestResultRepository : Interface_test_results
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private HorizonLabLibrary.HorizonLabTestTransactionsLibrary _hllTestTransactionApi = new HorizonLabLibrary.HorizonLabTestTransactionsLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestResultRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }


        public bool AddTestResults(hlab_test_results htt)
        {
            var result = _hllTestTransactionApi.AddTestResult(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteTestResults(int id)
        {
            var result = _hllTestTransactionApi.DeleteTestResult(id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public IEnumerable<testresultsview> GetAllTestResults(testresultsview htr)
        {
            var jsonList = _hllTestTransactionApi.GetTestResult(htr, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var testresultlist = JsonConvert.DeserializeObject<List<testresultsview>>(jsonList);
            return testresultlist;
        }

        public hlab_test_results GetTransactionDetails(int result_id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTestResults(hlab_test_results htr)
        {
            var result = _hllTestTransactionApi.UpdateTestResult(htr, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public IEnumerable<sp_gettestresults> GetTestResults(int transid)
        {
            var json_data = _hllTestTransactionApi.GetTestResult(new testresultsview { trans_id = transid}, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var test_result_list = JsonConvert.DeserializeObject<List<sp_gettestresults>>(json_data);
            return test_result_list;
        }

        public bool DeleteTestResultsByTransId(int transid)
        {
            var result = _hllTestTransactionApi.DeleteTestResultByTransId(transid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

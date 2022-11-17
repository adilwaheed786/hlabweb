using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTestTransactionsLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_test_transactions";

        public string GetAllTestTransactions(test_transaction trans, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(trans);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getalltransactionlist/", ApiKey, ApiHeader);
        }

        public string GetSemiPublicTransactionsList(test_transaction trans, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(trans);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getsemipublictransactionlist/", ApiKey, ApiHeader);
        }

        public string GetMonthlySubsidyTransactionsList(test_transaction trans, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(trans);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getmonthlysubsidytransactionlist/", ApiKey, ApiHeader);
        }

        public string GetTestResult(testresultsview testresult, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(testresult);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/gettestresult/", ApiKey, ApiHeader);
        }

        public string GetTransactionSupplyIds(hlab_transaction_supplies parameter, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(parameter);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/gettesttransactionsupplieids/", ApiKey, ApiHeader);
        }

        public string GetTransactionDetails(int transid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettransactiondetails?trans_id=" + transid, ApiKey, ApiHeader);
        }

        public string GetDefaultParameters(int packageid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getdefaultparameters?package_id=" + packageid, ApiKey, ApiHeader);
        }

        public string GetRequestSupplyList(int reqid, int formid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettestrequestsupplylist?reqid=" + reqid + "&formid=" + formid, ApiKey, ApiHeader);
        }

        public string UpdateTestTransactionDetails(hlab_test_transactions transaction, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(transaction);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatetesttransaction/", ApiKey, ApiHeader);
        }

        public string IfWaterSampleExists(hlab_test_transactions transaction, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(transaction);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/ifwatersampleexists/", ApiKey, ApiHeader);
        }

        public string UpdateTestResult(hlab_test_results testresult, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(testresult);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatetestresult/", ApiKey, ApiHeader);
        }

        public string AddTestTransaction(hlab_test_transactions test, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(test);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/addtesttransaction/", ApiKey, ApiHeader);
        }

        public string AddTestResult(hlab_test_results test, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(test);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtestresult/", ApiKey, ApiHeader);
        }

        public string AddTransactionSupplies(transaction_supply_param param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addtransactionsupplies/", ApiKey, ApiHeader);
        }

        public string DeleteTestResult(int resultid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletetestresults?resultid=" + resultid, ApiKey, ApiHeader);
        }

        public string DeleteTransactionSupplies(int transactionid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletetransactionsupplies?transactionid=" + transactionid, ApiKey, ApiHeader);
        }

        public string DeleteTestResultByTransId(int resultid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletetestresultsbytransid?transid=" + resultid, ApiKey, ApiHeader);
        }

        public string AddSubsidyFormImage(hlab_water_subsidy_form_images subsidy, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(subsidy);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addsubsidyformimage/", ApiKey, ApiHeader);
        }

        public string UpdateSubsidyFormImage(hlab_water_subsidy_form_images subsidy, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(subsidy);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatesubsidyformimage/", ApiKey, ApiHeader);
        }

        public string DeleteSubsidyFormImage(int id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deletesubsidyformimage?id=" + id, ApiKey, ApiHeader);
        }

        public string GetAllTranTypes(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/gettesttranstypes", ApiKey, ApiHeader);
        }

        public string PublishTestTransaction(int transactionid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/publishtesttransaction?transactionid=" + transactionid, ApiKey, ApiHeader);
        }
    }
}

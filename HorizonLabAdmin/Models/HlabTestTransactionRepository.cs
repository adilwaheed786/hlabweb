using HorizonLabLibrary;
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
    public class HlabTestTransactionRepository : Interface_test_transactions
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabTestTransactionsLibrary _hllTestTransactionApi = new HorizonLabTestTransactionsLibrary();
        private HorizonLabTableReferenceApiLibrary _hlabRefTableApi = new HorizonLabTableReferenceApiLibrary();
        private HorizonLabTestPackagesApiLibrary _hllTestPackageApi = new HorizonLabTestPackagesApiLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestTransactionRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public IEnumerable<testtransactionsview> GetAllTransactions(test_transaction htt)
        {
            var jsonList = _hllTestTransactionApi.GetAllTestTransactions(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var transactionList = JsonConvert.DeserializeObject<List<testtransactionsview>>(jsonList);
            return transactionList;
        }

        public sp_gethorizonlabtransactiondetails GetTransactionDetails(int trans_id)
        {
            var json_data = _hllTestTransactionApi.GetTransactionDetails(trans_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var transaction_detail = JsonConvert.DeserializeObject<sp_gethorizonlabtransactiondetails>(json_data);
            return transaction_detail;
        }

        public int AddTransactionDetails(hlab_test_transactions htt)
        {
            var result = _hllTestTransactionApi.AddTestTransaction(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            return Convert.ToInt32(result);
        }

        public bool UpdateTransactionDetails(hlab_test_transactions htt)
        {
            var result = _hllTestTransactionApi.UpdateTestTransactionDetails(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteTransactionDetails(int transaction_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<hlab_test_pkgs> GetAllTestPackages(int classid)
        {
            var json_data = _hllTestPackageApi.GetTestPackagesByClassId(classid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var test_package_list = JsonConvert.DeserializeObject<List<hlab_test_pkgs>>(json_data);
            return test_package_list;
        }

        public IEnumerable<sp_getdefaultpackageparameters> GetDefaultParameters(int packageid)
        {
            var json_data = _hllTestTransactionApi.GetDefaultParameters(packageid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var defaults = JsonConvert.DeserializeObject<List<sp_getdefaultpackageparameters>>(json_data);
            return defaults;
        }

        public IEnumerable<hlab_test_sample_types> GetAllSampleTypes()
        {
            var json_data = _hlabRefTableApi.GetAllSampleTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var sampletypes = JsonConvert.DeserializeObject<List<hlab_test_sample_types>>(json_data);
            return sampletypes;
        }

        public IEnumerable<hlab_test_report_types> GetAllReportTypes()
        {
            var json_data = _hlabRefTableApi.GetAllReportTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var reporttypes = JsonConvert.DeserializeObject<List<hlab_test_report_types>>(json_data);
            return reporttypes;
        }

        public bool AddSubsidyFormImage(hlab_water_subsidy_form_images subsidyimage)
        {            
            var result = _hllTestTransactionApi.AddSubsidyFormImage(subsidyimage, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool UpdateSubsidyFormImage(hlab_water_subsidy_form_images subsidyimage)
        {
            {
                var result = _hllTestTransactionApi.UpdateSubsidyFormImage(subsidyimage, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool IfWaterSampleExists(hlab_test_transactions htt)
        {
            var result = _hllTestTransactionApi.IfWaterSampleExists(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public IEnumerable<hlab_test_transaction_types> GetTestTransactionTypes()
        {
            var json_data = _hllTestTransactionApi.GetAllTranTypes(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var types = JsonConvert.DeserializeObject<List<hlab_test_transaction_types>>(json_data);
            return types;
        }

        public IEnumerable<hlab_transaction_supplies> GetTransactionSuppliesIds(hlab_transaction_supplies parameter)
        {
            var jsonList = _hllTestTransactionApi.GetTransactionSupplyIds(parameter, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var transaction_supply_id_list = JsonConvert.DeserializeObject<List<hlab_transaction_supplies>>(jsonList);
            return transaction_supply_id_list;
        }

        public bool AddTransactionSupplies(transaction_supply_param param)
        {
            var result = _hllTestTransactionApi.AddTransactionSupplies(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteTransactionSupplies(int transactionid)
        {
            var result = _hllTestTransactionApi.DeleteTransactionSupplies(transactionid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public List<testrequestsupplyview> GetTestRequestSupplyList(int requestid, int formid)
        {
            var json_data = _hllTestTransactionApi.GetRequestSupplyList(requestid, formid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var defaults = JsonConvert.DeserializeObject<List<testrequestsupplyview>>(json_data);
            return defaults;
        }

        public bool PublishTestTransaction(int transaction_id)
        {
            var result = _hllTestTransactionApi.PublishTestTransaction(transaction_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public IEnumerable<sp_getsemipublicreport> GetSemiPublicTransactions(test_transaction htt)
        {
            var jsonList = _hllTestTransactionApi.GetSemiPublicTransactionsList(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var transactionList = JsonConvert.DeserializeObject<List<sp_getsemipublicreport>>(jsonList);
            return transactionList;
        }

        public IEnumerable<sp_getmonthlysubsidyreport> GetMonthlySubsidyReport(test_transaction htt)
        {
            var jsonList = _hllTestTransactionApi.GetMonthlySubsidyTransactionsList(htt, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var transactionList = JsonConvert.DeserializeObject<List<sp_getmonthlysubsidyreport>>(jsonList);
            return transactionList;
        }

        public bool DeleteSubsidyFormImage(int id)
        {
            var result = _hllTestTransactionApi.DeleteSubsidyFormImage(id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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
    }
}

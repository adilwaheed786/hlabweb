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
    public class HlabTestProject: Interface_test_projects
    {
        private HorizonLabTestTransactionsLibrary _hllTestTransactionApi = new HorizonLabTestTransactionsLibrary();
        private HorizonLabTableReferenceApiLibrary _hllTableReference = new HorizonLabTableReferenceApiLibrary();
        private HorizonLabTestProjectLibrary _hllTestProjectLibrary = new HorizonLabTestProjectLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestProject(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public IEnumerable<hlab_test_projects> GetAllTestProjects()
        {
            var json_data = _hllTableReference.GetAllTestProjects(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var projectlist = JsonConvert.DeserializeObject<List<hlab_test_projects>>(json_data);
            return projectlist;
        }

        public bool AddNewTestPorject(hlab_test_projects new_project)
        {
            var result = _hllTestProjectLibrary.CreateNewTestProject(new_project, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteAddTestProjectSupplies(project_form_supply_param parameter)
        {
            var result = _hllTestProjectLibrary.DeleteAddProjectSupplies(parameter, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public IEnumerable<projectrequestsupplyview> GetAllProjectRequestSupplies(int proj_form_id)
        {
            var jsonList = _hllTestProjectLibrary.GetProjectSupplies(proj_form_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var list_supply = JsonConvert.DeserializeObject<List<projectrequestsupplyview>>(jsonList);
            return list_supply;
        }

        public IEnumerable<temporaryprojectrequestsview> GetTemporaryProjectRequestsView(temporaryprojectrequestsview param)
        {
            var jsonList = _hllTestProjectLibrary.GetProjectRequests(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var requestlist = JsonConvert.DeserializeObject<List<temporaryprojectrequestsview>>(jsonList);
            return requestlist;
        }

        public IEnumerable<hlab_temp_requests> GetTemporaryProjectRequests(hlab_temp_requests param)
        {
            var jsonList = _hllTestProjectLibrary.GetProjectRequests(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var requestlist = JsonConvert.DeserializeObject<List<hlab_temp_requests>>(jsonList);
            return requestlist;
        }

        public bool BulkCreateTemporaryRequest(bulkrequest_params parameter)
        {
            var result = _hllTestProjectLibrary.BulkRequestInsert(
                parameter,
                _webApibaseUrl,
                _hlabApiKey,
                _ApiHeader);
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

        public bool InsertBulkRequestToDb(BulkRequestInsertParameter param)
        {
            var result = _hllTestProjectLibrary.InsertBulkRequest(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool UpdateBulkRequest(BulkRequestInsertParameter param)
        {
            var result = _hllTestProjectLibrary.UpdateBulkRequest(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool DeleteTemporaryRequests(BulkRequestInsertParameter param)
        {
            var result = _hllTestProjectLibrary.DeleteTemporaryRequest(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

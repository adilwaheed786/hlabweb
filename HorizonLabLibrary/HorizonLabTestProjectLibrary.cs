using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTestProjectLibrary
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_test_project";

        public string CreateNewTestProject(hlab_test_projects new_project, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(new_project);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addnewproject/", ApiKey, ApiHeader);
        }

        public string DeleteAddProjectSupplies(project_form_supply_param param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/deleteaddprojectsupplies/", ApiKey, ApiHeader);
        }

        public string GetProjectSupplies(int proj_form_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getprojectsupplies?proj_form_id=" + proj_form_id, ApiKey, ApiHeader);
        }

        public string GetProjectRequests(temporaryprojectrequestsview request, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(request);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/gettemporaryprojectrequestsview/", ApiKey, ApiHeader);
        }

        public string GetProjectRequests(hlab_temp_requests param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/gettemporaryprojectrequests/", ApiKey, ApiHeader);
        }

        //BULK REQUEST METHODS
        public string BulkRequestInsert(bulkrequest_params param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/bulkrequestinsert/", ApiKey, ApiHeader);
            //return _hllWebApi.GetRecords($"{baseUrl}{hlab_api_controller_name}/bulkrequestinsert?row_count={row_count}&payment_id={payment_id}&test_pkg_id={test_pkg_id}&project_id={project_id}", ApiKey, ApiHeader);
        }

        public string UpdateBulkRequest(BulkRequestInsertParameter param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updatebulkrequest/", ApiKey, ApiHeader);
        }

        public string InsertBulkRequest(BulkRequestInsertParameter param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/insertbulkrequest/", ApiKey, ApiHeader);
        }

        public string DeleteTemporaryRequest(BulkRequestInsertParameter param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/deletetemporaryrequests/", ApiKey, ApiHeader);
        }
        //END 
    }
}

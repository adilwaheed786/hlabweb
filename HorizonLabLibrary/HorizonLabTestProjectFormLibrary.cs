using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabTestProjectFormLibrary
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_test_project_form";

        public string UpdatTestProjecForm(hlab_test_project_forms param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateprojectform/", ApiKey, ApiHeader);
        }

        public string CreateNewTestProjectForm(hlab_test_project_forms new_form, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(new_form);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/addnewprojectform/", ApiKey, ApiHeader);
        }

        public string GetProjectRequestForm(int proj_form_id, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getprojectforminfo?proj_form_id=" + proj_form_id, ApiKey, ApiHeader);
        }

        public string GetProjectRequestForms(projectrequestsformview param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getprojectrequestsforms/", ApiKey, ApiHeader);
        }
    }
}

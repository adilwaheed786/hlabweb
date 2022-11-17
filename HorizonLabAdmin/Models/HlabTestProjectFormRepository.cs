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
    public class HlabTestProjectFormRepository : Interface_hlab_test_project_form
    {
        private HorizonLabTestProjectFormLibrary _hllTestProjectFormLibrary = new HorizonLabTestProjectFormLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabTestProjectFormRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public int AddNewTestPorjectForm(hlab_test_project_forms new_form)
        {
            var result = _hllTestProjectFormLibrary.CreateNewTestProjectForm(new_form, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(result))
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            return 0;
        }

        public bool UpdateTestProjForm(hlab_test_project_forms param)
        {
            var result = _hllTestProjectFormLibrary.UpdatTestProjecForm(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public projectrequestsformview GetProjectRequestFormInfo(int proj_form_id)
        {
            var jsondata = _hllTestProjectFormLibrary.GetProjectRequestForm(proj_form_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var info = JsonConvert.DeserializeObject<projectrequestsformview>(jsondata);
            return info;
        }

        public List<projectrequestsformview> ListProjectRequestFormInfo(projectrequestsformview param)
        {
            var jsonList = _hllTestProjectFormLibrary.GetProjectRequestForms(param, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var list_forms = JsonConvert.DeserializeObject<List<projectrequestsformview>>(jsonList);
            return list_forms;
        }
    }
}

using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabEmail
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_email";

        public string GetAllEmailTemplates(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getallemailtemplates", ApiKey, ApiHeader);
        }

        public string GetAllEmailLogs(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getallemaillogs", ApiKey, ApiHeader);
        }

        public string GetEmailFileAttachments(hlab_email_file_attachments efa, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords($"{baseUrl}{hlab_api_controller_name}/getemailattachments?templateid={templateid}", ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(efa);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, $"{baseUrl}{hlab_api_controller_name}/getemailattachments", ApiKey, ApiHeader);
        }

        public string AddNewTemplate(hlab_email_templates template, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(template);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, $"{baseUrl}{hlab_api_controller_name}/insertnewtemplate", ApiKey, ApiHeader);
        }

        public string UpdateTemplate(hlab_email_templates template, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(template);
            return _hllWebApi.CommitPostAction(dataAsString, $"{baseUrl}{hlab_api_controller_name}/updatetemplate", ApiKey, ApiHeader);
        }

        public string AssignFileAttachments(hlab_email_file_attachments file, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(file);
            return _hllWebApi.CommitPostAction(dataAsString, $"{baseUrl}{hlab_api_controller_name}/insertfileattachment", ApiKey, ApiHeader);
        }

        public string DeleteAllAttachment(hlab_email_file_attachments efa, string baseUrl, string ApiKey, string ApiHeader)
        {
            //return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deleteallfileattachments?templateid=" + templateid, ApiKey, ApiHeader);
            var dataAsString = JsonConvert.SerializeObject(efa);
            return _hllWebApi.CommitPostAction(dataAsString, $"{baseUrl}{hlab_api_controller_name}/deleteallfileattachments", ApiKey, ApiHeader);
        }

        public void LogEmail(hlab_email_log emaillog, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(emaillog);
            _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/logemail/", ApiKey, ApiHeader);
        }
    }
}

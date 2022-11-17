using HorizonLabLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabUserAccountApiLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_user";

        public string CreateNewUserAccount(hlab_users user, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(user);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addnewuser/", ApiKey, ApiHeader);
        }

        public string UpdateUserAccountChanges(hlab_users user, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(user);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateuser/", ApiKey, ApiHeader);
        }

        public string GetUserAccountDetails(string userid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getuserinfo?userid=" + userid, ApiKey, ApiHeader);
        }

        public string GetUserAllActiveAccounts(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getallacvtivehlabuseraccounts", ApiKey, ApiHeader);
        }

        public string GetUserAccessList(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getuseraccesslist", ApiKey, ApiHeader);
        }

        public string GetUserAllInActiveAccounts(string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getallinacacvtivehlabuseraccounts", ApiKey, ApiHeader);
        }

        public string SearchUsers(string searchString, string searchBy, bool accountStatus, string baseUrl, string ApiKey, string ApiHeader)
        {
            if (searchBy == "first_name")
            {
                return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/searchhlabuseraccountsbyfirstname?firstName=" + searchString + "&status=" + accountStatus, ApiKey, ApiHeader);
            }
            else if (searchBy == "last_name")
            {
                return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/searchhlabuseraccountsbylastname?lastName=" + searchString + "&status=" + accountStatus, ApiKey, ApiHeader);
            }
            else if(searchBy == "email")
            {
                return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/searchhlabuseraccountsbyemail?email=" + searchString + "&status=" + accountStatus, ApiKey, ApiHeader);
            }
            else //seach by username
            {
                return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/searchhlabuseraccountsbyusername?userName=" + searchString + "&status=" + accountStatus, ApiKey, ApiHeader);
            }
        }
    }
}

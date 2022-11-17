using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Models
{
    public class HlabUserRepository : Interface_hlab_users
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private HorizonLabLibrary.HorizonLabUserAccountApiLibrary _hllUserWebApi = new HorizonLabLibrary.HorizonLabUserAccountApiLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        private readonly ILogger<HlabUserRepository> _logger;
        string _hlabApiKey;
        string _ApiHeader;
        
        public HlabUserRepository(IConfiguration appConfig, ILogger<HlabUserRepository> logger)
        {
            _appConfig = appConfig;
            _logger = logger;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public hlab_users GetUserAuthentication(string username, string password) //for windows authenticate
        {
            hlab_users user = new hlab_users();
            string result;

            try
            {
                if (!String.IsNullOrEmpty(username))
                {
                    result = _hllWebApi.UserDetailsGet(username, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                    if (!string.IsNullOrEmpty(result))
                    {
                        user = JsonConvert.DeserializeObject<hlab_users>(result);
                        return user;
                    }
                    return null;
                }
                return null;
            }
            catch(Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
         
        }

        public IEnumerable<hlab_users> GetAllActiveAccounts()
        {
            var jsonUserList = _hllUserWebApi.GetUserAllActiveAccounts(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var userList = JsonConvert.DeserializeObject<List<hlab_users>>(jsonUserList);
            return userList;
        }

        public IEnumerable<hlab_users> GetAllInActiveAccounts()
        {
            var jsonUserList = _hllUserWebApi.GetUserAllInActiveAccounts(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var userList = JsonConvert.DeserializeObject<List<hlab_users>>(jsonUserList);
            return userList;
        }

        public hlab_users GetUserDetails(int UserId)
        {
            var jsonUserList = _hllUserWebApi.GetUserAccountDetails(UserId.ToString(), _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var user = JsonConvert.DeserializeObject<hlab_users>(jsonUserList);
            return user;
        }

        public bool UpdateUserInformation(hlab_users user)
        {
            var result = _hllUserWebApi.UpdateUserAccountChanges(user, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public bool AddNewUserAccount(hlab_users user)
        {
            var result = _hllUserWebApi.CreateNewUserAccount(user, _webApibaseUrl, _hlabApiKey, _ApiHeader);
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

        public List<hlab_user_access> GetUserAccessList()
        {
            var jsonUserAccessList = _hllUserWebApi.GetUserAccessList(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var userAccessList = JsonConvert.DeserializeObject<List<hlab_user_access>>(jsonUserAccessList);
            return userAccessList;
        }

        public IEnumerable<hlab_users> SearchUserRecords(string searchString, string searchBy, bool accountStatus)
        {
            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchBy))
            {
                var jsonUserList = _hllUserWebApi.SearchUsers(searchString, searchBy, accountStatus, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                var userList = JsonConvert.DeserializeObject<List<hlab_users>>(jsonUserList);
                return userList;
            }
            return null;
        }
    }
}

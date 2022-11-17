using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities.Session
{
    public class UserSession: SessionBase, IUserSession
    {
        private hlab_users user = new hlab_users();
        private readonly ILogger<UserSession> _logger;
        private readonly string key_user_name = "UserName";
        private readonly string key_signature = "SignatureImage";
        private readonly string key_first_name = "UserFirstName";
        private readonly string key_last_name = "UserLastName";
        private readonly string key_user_role = "UserRole";
        public string key_search_useraccount_status { get; }
        public string key_search_useraccount_firstname { get; }
        public string key_search_useraccount_lastname { get; }
        public string key_search_useraccount_email { get; }
        public string key_search_useraccount_username { get; }
        public string key_search_useraccount_role { get; }
        public string key_search_useraccount_accessid { get; }

        public UserSession(IHttpContextAccessor _httpContextAccessor, ILogger<UserSession> logger) : base(_httpContextAccessor, logger)
        {
            _logger = logger;
            key_search_useraccount_status = "search_UserAccountStatus";
            key_search_useraccount_firstname = "search_UserLastName";
            key_search_useraccount_lastname = "search_UserLastName";
            key_search_useraccount_email = "search_Email";
            key_search_useraccount_username = "search_UserName";
            key_search_useraccount_role = "search_UserRole";
            key_search_useraccount_accessid = "search_UserAccessId";
        }

        public void SetUserSession(hlab_users user)
        {
            StringSessionParameter userNameParameter = new StringSessionParameter{ Key= key_user_name, Value=user.username };
            StringSessionParameter signatureParameter = new StringSessionParameter{ Key= key_signature, Value=user.signature_img };
            StringSessionParameter blankSignatureParameter = new StringSessionParameter{ Key= key_signature, Value="" };
            StringSessionParameter firstNameParameter = new StringSessionParameter{ Key= key_first_name, Value=user.fname };
            StringSessionParameter lastNameParameter = new StringSessionParameter{ Key= key_last_name, Value=user.lname };
            StringSessionParameter userRoleParameter = new StringSessionParameter{ Key= key_user_role, Value=user.role };
            IntSessionParameter userAccessIdParameter = new IntSessionParameter { Key = key_search_useraccount_accessid, Value = user.access_id};

            if (IsSessionInputNotNull(user.username)) SetStringSession(userNameParameter);
            SetStringSession(blankSignatureParameter);
            if (IsSessionInputNotNull(user.signature_img)) SetStringSession(signatureParameter);
            if (IsSessionInputNotNull(user.fname)) SetStringSession(firstNameParameter);
            if (IsSessionInputNotNull(user.lname)) SetStringSession(lastNameParameter);
            if (IsSessionInputNotNull(user.role)) SetStringSession(userRoleParameter);
            SetIntSession(userAccessIdParameter);
        }

        public hlab_users GetUserObjectFromSession()
        {            
            user.username = GetSessionStringValue(key_user_name);
            user.signature_img = GetSessionStringValue(key_signature);
            user.fname = GetSessionStringValue(key_first_name);
            user.lname = GetSessionStringValue(key_last_name);
            user.access_id = GetSessionIntValue(key_search_useraccount_accessid);
            return user;
        }

        public bool IsUserNotLoggedIn()
        {
            return string.IsNullOrEmpty(GetSessionStringValue(key_user_name));
        }

        public string GetSessionUserName()
        {
            return GetSessionStringValue(key_user_name);
        }

        public string GetSignatureImgFromSessionWhenEmpty(string parameter_signature)
        {
            if (string.IsNullOrEmpty(parameter_signature))
            {
                return GetSessionStringValue(key_signature);
            }
            return parameter_signature;
        }

        public string GetUserFirstNameFromSessionWhenEmpty(string parameter_first_name)
        {
            if (string.IsNullOrEmpty(parameter_first_name))
            {
                return GetSessionStringValue(key_first_name);
            }
            return parameter_first_name;
        }

        public string GetUserLastNameFromSessionWhenEmpty(string parameter_last_name)
        {
            if (string.IsNullOrEmpty(parameter_last_name))
            {
                return GetSessionStringValue(key_last_name);
            }
            return parameter_last_name;
        }

        public void SetSearchUserSession(hlab_users user, string email)
        {
            StringSessionParameter userNameParameter = new StringSessionParameter { Key = key_search_useraccount_username, Value = user.username };
            StringSessionParameter firstNameParameter = new StringSessionParameter { Key = key_search_useraccount_firstname, Value = user.fname };
            StringSessionParameter lastNameParameter = new StringSessionParameter { Key = key_search_useraccount_lastname, Value = user.lname };
            StringSessionParameter emailParameter = new StringSessionParameter { Key = key_search_useraccount_email, Value = email };
            BooleanSessionParameter accountStatus = new BooleanSessionParameter { Key = key_search_useraccount_status, Value = user.status};

            SetStringSession(userNameParameter);
            SetStringSession(firstNameParameter);
            SetStringSession(lastNameParameter);
            SetStringSession(emailParameter);
            SetBooleanSessionWithNullValidation(accountStatus);
        }

        public void ClearUserAccountSearchSession()
        {
            StringSessionParameter userNameParameter = new StringSessionParameter { Key = key_search_useraccount_username, Value = "" };
            StringSessionParameter firstNameParameter = new StringSessionParameter { Key = key_search_useraccount_firstname, Value = "" };
            StringSessionParameter lastNameParameter = new StringSessionParameter { Key = key_search_useraccount_lastname, Value = "" };
            StringSessionParameter emailParameter = new StringSessionParameter { Key = key_search_useraccount_email, Value = "" };

            SetStringSession(userNameParameter);
            SetStringSession(firstNameParameter);
            SetStringSession(lastNameParameter);
            SetStringSession(emailParameter);
            
        }

        public void SetSearchStringSession(string SearchByKey, string SearchString)
        {
            StringSessionParameter userSearchParameter = new StringSessionParameter { Key = SearchByKey, Value = SearchString };
            SetStringSession(userSearchParameter);
        }

        public hlab_users GetSearchUserObjectFromSession()
        {
            hlab_users search_user = new hlab_users();
            string status = GetSessionStringValue(key_search_useraccount_status);

            search_user.username = GetSessionStringValue(key_search_useraccount_username);
            search_user.status = !string.IsNullOrEmpty(status) ? Convert.ToBoolean(status.ToLower()) : false;
            search_user.fname = GetSessionStringValue(key_search_useraccount_firstname);
            search_user.lname = GetSessionStringValue(key_search_useraccount_lastname);
            search_user.email = GetSessionStringValue(key_search_useraccount_email);
            return search_user;
        }

        public string GetUserAccountStatusSession()
        {
            return GetSessionStringValue(key_search_useraccount_status);
        }

        public string GetUserUserRoleSessionWhenEmpty(string parameter_user_role)
        {
            if (string.IsNullOrEmpty(parameter_user_role))
            {
                return GetSessionStringValue(key_user_role);
            }
            return parameter_user_role;
        }
    }
}

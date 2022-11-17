using HorizonLabAdmin.Helpers.Containers;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces.Session
{
    public interface IUserSession
    {
        string key_search_useraccount_status { get; }
        string key_search_useraccount_firstname { get; }
        string key_search_useraccount_lastname { get; }
        string key_search_useraccount_email { get;  }
        string key_search_useraccount_username { get; }
        string key_search_useraccount_role { get; }

        bool IsUserNotLoggedIn();
        void SetUserSession(hlab_users user);
        void SetSearchUserSession(hlab_users user, string email);
        void ClearUserAccountSearchSession();
        void SetSearchStringSession(string SearchByKey, string SearchString);
        hlab_users GetUserObjectFromSession();
        hlab_users GetSearchUserObjectFromSession();        
        string GetSessionUserName();
        string GetUserAccountStatusSession();
        string GetSignatureImgFromSessionWhenEmpty(string parameter_signature);
        string GetUserFirstNameFromSessionWhenEmpty(string parameter_first_name);
        string GetUserLastNameFromSessionWhenEmpty(string parameter_last_name);        
        string GetUserUserRoleSessionWhenEmpty(string parameter_user_role);        
    }
}

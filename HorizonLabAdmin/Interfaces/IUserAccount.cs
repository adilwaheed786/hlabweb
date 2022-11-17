using HorizonLabAdmin.Helpers.Containers;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IUserAccount
    {
        bool proceed_to_process { get; set; }
        string OutputMessage { get; set; }
        List<SelectListItem> GenerateSearchOptionSelect();
        UserAccountSearchField PopulateUserAccountSearchFields(List<SelectListItem> searchOption);
        List<hlab_user_access> ListUserAccountAccess();
        List<hlab_users> ListUserAccountRecords(string status_filter);
        hlab_users GetUserAccountInfo(int userid);
        bool UpdateUserAccountDb(hlab_users user);
        bool IsUserFieldsNullEmpty(hlab_users user);
        bool IsUserNameAlreadyExistsInDb(string username);
        bool IsEmailAlreadyExistsInDb(string email);
        bool ValidateNewUserAccountInfo(hlab_users user);
        bool ValidateExistingUserAccountInfo(hlab_users user);
        bool AddNewUserAccountToDb(hlab_users user);
        bool UpdateUserAccountInfoToDb(hlab_users user);
    }
}

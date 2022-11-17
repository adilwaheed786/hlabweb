using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_users
    {        
        IEnumerable<hlab_users> GetAllActiveAccounts();
        IEnumerable<hlab_users> GetAllInActiveAccounts();
        IEnumerable<hlab_users> SearchUserRecords(string searchString, string searchBy, bool accountStatus);
        hlab_users GetUserAuthentication(string username, string password);
        hlab_users GetUserDetails(int UserId);
        List<hlab_user_access> GetUserAccessList();
        bool UpdateUserInformation(hlab_users user);
        bool AddNewUserAccount(hlab_users user);
    }
}

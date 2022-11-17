using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class UserAccount : IUserAccount
    {
        private readonly IHorizonLabSession _session;
        private readonly Interface_hlab_users _hlabUserRepo;
        private readonly ILogger<UserAccount> _logger;

        public bool proceed_to_process { get; set; }
        public string OutputMessage { get; set; }

        public UserAccount(IHorizonLabSession session, Interface_hlab_users hlabUserRepo, ILogger<UserAccount> logger)
        {
            _session = session;
            _hlabUserRepo = hlabUserRepo;
            _logger = logger;
        }

        public List<SelectListItem> GenerateSearchOptionSelect()
        {
            List<SelectListItem> searchOption = new List<SelectListItem>();
            searchOption.Add(new SelectListItem { Text = "Search All", Value = "search_All" });
            searchOption.Add(new SelectListItem { Text = "Search By First Name", Value = "search_UserFirstName" });
            searchOption.Add(new SelectListItem { Text = "Search By Last Name", Value = "search_UserLastName" });
            searchOption.Add(new SelectListItem { Text = "Search By UserName", Value = "search_UserName" });
            searchOption.Add(new SelectListItem { Text = "Search By Email", Value = "search_Email" });
            return searchOption;
        }

        public hlab_users GetUserAccountInfo(int userid)
        {
            try
            {
                return _hlabUserRepo.GetUserDetails(userid);
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > GetUserAccountInfo(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsEmailAlreadyExistsInDb(string email)
        {
            try
            {
                List<hlab_users> active_accounts = new List<hlab_users>();
                List<hlab_users> inactive_accounts = new List<hlab_users>();
                active_accounts = ListUserAccountRecords("active");
                inactive_accounts = ListUserAccountRecords("inactive");
                if (active_accounts.FirstOrDefault(x => x.email == email) != null || inactive_accounts.FirstOrDefault(x => x.email == email) != null) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > IsEmailAlreadyExistsInDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsUserFieldsNullEmpty(hlab_users user)
        {
            if (
                string.IsNullOrEmpty(user.fname) ||
                string.IsNullOrEmpty(user.lname) ||
                string.IsNullOrEmpty(user.username) ||
                string.IsNullOrEmpty(user.password) ||
                string.IsNullOrEmpty(user.email)) return true;
            return false;
        }

        public bool IsUserNameAlreadyExistsInDb(string username)
        {            
            try
            {
                if (_hlabUserRepo.GetUserAuthentication(username,"") != null) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > IsUserWindowsAuthenticated(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_user_access> ListUserAccountAccess()
        {
            try
            {
                return _hlabUserRepo.GetUserAccessList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > ListUserAccountRecords(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_users> ListUserAccountRecords(string status_filter)
        {
            try
            {
                List<hlab_users> account_list = new List<hlab_users>();
                if (string.IsNullOrEmpty(status_filter)) return account_list;

                if (status_filter.ToLower() == "active")
                {
                    account_list = _hlabUserRepo.GetAllActiveAccounts().ToList();
                    return account_list;
                }

                account_list = _hlabUserRepo.GetAllInActiveAccounts().ToList();
                return account_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > ListUserAccountRecords(string status_filter): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public UserAccountSearchField PopulateUserAccountSearchFields(List<SelectListItem> searchOption)
        {
            UserAccountSearchField output_search_user= new UserAccountSearchField();
            hlab_users session_user = _session.GetSearchUserObjectFromSession();
            output_search_user.searchOption = searchOption;
            output_search_user.search_UserAccountStatus = true;

            if (!string.IsNullOrEmpty(_session.GetUserAccountStatusSession())) output_search_user.search_UserAccountStatus = session_user.status;

            if (!string.IsNullOrEmpty(session_user.fname))
            {
                output_search_user.search_String = session_user.fname;
                output_search_user.search_By = "first_name";
                output_search_user.searchOption.Where(x => x.Value == _session.key_search_useraccount_firstname).First().Selected = true;
            }
            else if (!string.IsNullOrEmpty(session_user.lname))
            {
                output_search_user.search_String = session_user.lname;
                output_search_user.search_By = "last_name";
                output_search_user.searchOption.Where(x => x.Value == _session.key_search_useraccount_lastname).First().Selected = true;
            }
            else if (!String.IsNullOrEmpty(session_user.username))
            {
                output_search_user.search_String = session_user.username;
                output_search_user.search_By = "username";
                output_search_user.searchOption.Where(x => x.Value == _session.key_search_useraccount_username).First().Selected = true;
            }
            else if (!String.IsNullOrEmpty(session_user.email))
            {
                output_search_user.search_String = session_user.email;
                output_search_user.search_By = "email";
                output_search_user.searchOption.Where(x => x.Value == _session.key_search_useraccount_email).First().Selected = true;
            }

            if (!string.IsNullOrEmpty(output_search_user.search_String) && !string.IsNullOrEmpty(output_search_user.search_By))
            {
                output_search_user.search_String = output_search_user.search_String.Trim();
                output_search_user.userList = _hlabUserRepo.SearchUserRecords(output_search_user.search_String, output_search_user.search_By, output_search_user.search_UserAccountStatus).ToList();
            }
            else
            {
                if (output_search_user.search_UserAccountStatus)
                {
                    output_search_user.userList = _hlabUserRepo.GetAllActiveAccounts().ToList();
                    output_search_user.status_label = "Active Accounts";
                }
                else
                {
                    output_search_user.userList = _hlabUserRepo.GetAllInActiveAccounts().ToList();
                    output_search_user.status_label = "InActive Accounts";
                }
            }

            return output_search_user;
        }

        public bool UpdateUserAccountDb(hlab_users user)
        {
            try
            {
                return _hlabUserRepo.UpdateUserInformation(user);
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > UpdateUserAccountDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool ValidateNewUserAccountInfo(hlab_users user)
        {
            try
            {
                OutputMessage = "";
                if (user == null)
                {
                    OutputMessage = "Error:Saving user account failed, user data object object is null!";
                    return false;
                }

                if (IsUserFieldsNullEmpty(user))
                {
                    OutputMessage = "Error:Saving user account failed, Incomplete user information!";
                    return false;
                }

                if (IsUserNameAlreadyExistsInDb(user.username))
                {
                    OutputMessage = "Error:Saving user account failed, Username " + user.username + " already exists!";
                    return false;
                }

                if (IsEmailAlreadyExistsInDb(user.email))
                {
                    OutputMessage = "Error:Saving user account failed, Email " + user.email + " already exists!";
                    return false;
                }
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > ValidateNewUserAccountInfo(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool AddNewUserAccountToDb(hlab_users user)
        {
            try
            {
                OutputMessage = "Error:Adding new user account failed, Please contact administrator!";
                bool insert_result = true;
                user.status = true;
                user.date_reg = DateTime.Now;
                user.password = FunctionLibrary.MD5Hash(user.password);                
                insert_result = _hlabUserRepo.AddNewUserAccount(user);
                if (insert_result) OutputMessage = "Success:New user account was successfully added!";
                return insert_result;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > AddNewUserAccountToDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool ValidateExistingUserAccountInfo(hlab_users user)
        {
            try
            {
                OutputMessage = "";
                List<hlab_users> useraccounts = new List<hlab_users>();
                List<hlab_users> inactive_accounts = new List<hlab_users>();
                useraccounts = _hlabUserRepo.GetAllActiveAccounts().ToList();
                inactive_accounts = _hlabUserRepo.GetAllInActiveAccounts().ToList();
                if (inactive_accounts.Count > 0) useraccounts.Concat(inactive_accounts).ToList();

                if (user == null)
                {
                    OutputMessage = "Error:Saving user account failed, hlab_users object is null!";
                    return false;
                }

                if (
                    string.IsNullOrEmpty(user.fname) ||
                    string.IsNullOrEmpty(user.lname) ||
                    string.IsNullOrEmpty(user.username) ||
                    string.IsNullOrEmpty(user.password) ||
                    string.IsNullOrEmpty(user.email))
                {
                    OutputMessage = "Error:Saving user account failed, Incomplete user information!";
                    return false;
                }

                //check if new username has already taken 
                var userrecord = useraccounts.Where(x => x.username == user.username).FirstOrDefault();
                if (userrecord != null)
                {
                    if (userrecord.user_id != user.user_id)
                    {
                        OutputMessage = "Error:Saving user account failed, Username " + user.username + " already exists!";
                        return false;
                    }
                }

                //check if new email has already taken on active and inactive accounts
                var emailrecord = useraccounts.Where(x => x.email == user.email).FirstOrDefault();
                if (emailrecord != null)
                {
                    if (emailrecord.user_id != user.user_id)
                    {
                        OutputMessage = "Error:Saving user account failed, Email " + user.email + " already exists!";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > ValidateExistingUserAccountInfo(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateUserAccountInfoToDb(hlab_users user)
        {
            try
            {
                OutputMessage = "Error:saving user account changes failed, Please contact administrator!";
                bool update_result = true;
                user.status = true;
                user.date_reg = DateTime.Now;
                user.password = FunctionLibrary.MD5Hash(user.password);
                update_result = _hlabUserRepo.UpdateUserInformation(user);
                if (update_result) OutputMessage = "Success:user account changes were successfully saved!";
                return update_result;
            }
            catch (Exception exc)
            {
                _logger.LogError($"UserAccount > UpdateUserAccountInfoToDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}

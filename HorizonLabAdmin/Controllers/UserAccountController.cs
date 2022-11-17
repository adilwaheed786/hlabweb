using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class UserAccountController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IUserAccount _userAccountHelper;
        private readonly IUtility _utilityHelper;
        private readonly ILogger<UserAccountController> _logger;
        private readonly hlab_users active_blank_user;
        private readonly hlab_users inactive_blank_user;

        public UserAccountController(
            IUtility utilityHelper, 
            IConfiguration appConfig, 
            IHostingEnvironment env, 
            ILogger<UserAccountController> logger, 
            IHorizonLabSession sessionHelper, 
            IUserAccount userAccountHelper)
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _sessionHelper = sessionHelper;
            _userAccountHelper = userAccountHelper;
            _utilityHelper = utilityHelper;
            active_blank_user = new hlab_users
            {
                username = "",
                fname = "",
                lname = "",
                email = "",
                status = true
            };

            inactive_blank_user = new hlab_users
            {
                username = "",
                fname = "",
                lname = "",
                email = "",
                status = true
            };
        }

        public IActionResult ActiveAccountsPage()
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _sessionHelper.SetSearchUserSession(active_blank_user, "");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return GoToUserAccountMainPage();
        }

        public IActionResult InactiveAccountsPage()
        {
            try
            {
                _sessionHelper.SetSearchUserSession(inactive_blank_user, "");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return GoToUserAccountMainPage();
        }

        [HttpPost]
        public IActionResult SearchUserAccounts(string searchString, string searchBy)
        {
            try
            {
                _sessionHelper.ClearUserAccountSearchSession();
                if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchBy)) _sessionHelper.SetSearchStringSession(searchBy, searchString);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return GoToUserAccountMainPage();
        }

        public IActionResult Index()
        {
            try
            {
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "user");                
                UserAccountSearchField searchField = new UserAccountSearchField();
                List<hlab_users> userList = new List<hlab_users>();
                string str_status = "active";
                List<SelectListItem> searchOption = _userAccountHelper.GenerateSearchOptionSelect();
                
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                searchField = _userAccountHelper.PopulateUserAccountSearchFields(searchOption);

                if (!searchField.search_UserAccountStatus) str_status = "inactive";
                userList = _userAccountHelper.ListUserAccountRecords(str_status);

                ViewBag.status = searchField.search_UserAccountStatus;
                ViewBag.searched = searchField.search_String;
                ViewBag.searchOption = searchField.searchOption;
                ViewBag.menu = _hlabMenu;
                ViewBag.Page = searchField.status_label;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.userList = userList;

                if (TempData["UserAccountMessage"] != null) ViewBag.PageMessage = TempData["UserAccountMessage"].ToString();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return View();
        }

        public IActionResult UserAccountForm(string userId="")
        {
            try
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page

                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "user");
                List<hlab_user_access> user_access_list = new List<hlab_user_access>();
                hlab_users user = new hlab_users();

                List<SelectListItem> selectUserAccessList = _utilityHelper.GenerateSelectListItem(
                    _userAccountHelper.ListUserAccountAccess(),
                    "access_id",
                    "access_name");

                user = null;
                if (!string.IsNullOrEmpty(userId))
                {
                    user = _userAccountHelper.GetUserAccountInfo(Convert.ToInt32(userId));
                    selectUserAccessList.Where(x => x.Value == user.access_id.ToString()).FirstOrDefault().Selected = true;
                }

                ViewBag.user = user;
                ViewBag.menu = _hlabMenu;
                ViewBag.user_access_list = selectUserAccessList;
                ViewBag.asp_action = string.IsNullOrEmpty(userId) ? "SaveUserAccount" : "UpdateUserAccount";
                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ViewUserAccount(string userId)
        {
            try
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "user");

                hlab_users user = new hlab_users();
                List<hlab_user_access> user_access_list = new List<hlab_user_access>();

                user = _userAccountHelper.GetUserAccountInfo(Convert.ToInt32(userId));
                user_access_list = _userAccountHelper.ListUserAccountAccess();

                ViewBag.user = user;
                ViewBag.access = user_access_list.FirstOrDefault(x => x.access_id == user.access_id).access_name;
                ViewBag.menu = _hlabMenu;
                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
                return View();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult DelteUserAccount(string userId)
        {
            try
            {
                string PageMessage = "";
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page
                if (!string.IsNullOrEmpty(userId))
                {
                    hlab_users user = _userAccountHelper.GetUserAccountInfo(Convert.ToInt32(userId));
                    user.status = false;
                    if (_userAccountHelper.UpdateUserAccountDb(user))
                    {
                        PageMessage = "Success:" + user.username + " was successfully deleted!";
                    }
                    else
                    {
                        PageMessage = "Error:Deleting " + user.username + " account failed! Please contact administrator..";
                    }
                }
                if (!string.IsNullOrEmpty(PageMessage)) TempData["UserAccountMessage"] = PageMessage;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return GoToActiveUserAccountPage();
        }

        [HttpGet]
        public IActionResult ActivateAccount(string userId)
        {
            try
            {
                string PageMessage = "";
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page
                if (!string.IsNullOrEmpty(userId))
                {
                    hlab_users user = _userAccountHelper.GetUserAccountInfo(Convert.ToInt32(userId));
                    user.status = true;
                    if (_userAccountHelper.UpdateUserAccountDb(user))
                    {
                        PageMessage = "Success:" + user.username + " was successfully activated!";
                    }
                    else
                    {
                        PageMessage = "Error:Activating " + user.username + " account failed! Please contact administrator..";
                    }
                }
                if (!string.IsNullOrEmpty(PageMessage)) TempData["UserAccountMessage"] = PageMessage;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return GoToActiveUserAccountPage();
        }

        [HttpPost]
        public IActionResult SaveUserAccount(hlab_users user)
        {
            try
            {
                string PageMessage = "";
                bool proceed = true;
                IDictionary<string, string> validationResult = new Dictionary<string, string>();
                proceed = _userAccountHelper.ValidateNewUserAccountInfo(user);
                PageMessage = _userAccountHelper.OutputMessage;

                if (proceed)
                {
                    _userAccountHelper.AddNewUserAccountToDb(user);
                    PageMessage = _userAccountHelper.OutputMessage;
                }

                if (!string.IsNullOrEmpty(PageMessage)) TempData["UserAccountMessage"] = PageMessage;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return GoToUserAccountMainPage();
        }

        [HttpPost]
        public IActionResult UpdateUserAccount(hlab_users user)
        {
            try
            {
                string PageMessage = "";
                bool proceed = true;
                IDictionary<string, string> validationResult = new Dictionary<string, string>();
                proceed = _userAccountHelper.ValidateExistingUserAccountInfo(user);
                PageMessage = _userAccountHelper.OutputMessage;

                if (proceed)
                {
                    _userAccountHelper.UpdateUserAccountInfoToDb(user);
                    PageMessage = _userAccountHelper.OutputMessage;
                }

                if (!string.IsNullOrEmpty(PageMessage)) TempData["UserAccountMessage"] = PageMessage;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return GoToUserAccountMainPage();
        }
    }
}
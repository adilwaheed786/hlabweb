using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using HorizonLabAdmin.Models;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Helpers.Utilities.Session;
using HorizonLabAdmin.Helpers.Utilities;

namespace HorizonLabAdmin.Controllers
{    
    public class LoginController : HController
    {
        private readonly ILogin _login;        
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly UserSession _userSession;
        private readonly ILogger<UserSession> _logger;        

        public LoginController(ILogger<UserSession> logger, ILogin login, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _login = login;
            _httpContextAccessor = httpContextAccessor;
            _userSession = new UserSession(_httpContextAccessor,_logger);
        }       

        public IActionResult Index()
        {
            try
            {
                hlab_users hlabUser = _login.GenerateLoginWindowsAuthenticationUser();
                if (hlabUser == null)
                {
                    ViewData["LoginMessage"] = "Access Denied!";
                    return View();
                }
                else
                {
                    ViewData["LoginMessage"] = "user: " + hlabUser.username;
                    _userSession.SetUserSession(hlabUser);
                    return GoToRequestMainPage();
                }
            }
            catch (Exception xc)
            {
                _logger.LogError("Login Controller Exception: " + xc.Message);
                ViewData["LoginMessage"] = "Login Controller: " + xc.Message;
                return View();
            }
        }
    }
}
using HorizonLabAdmin.Interfaces;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HLogin : ILogin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Interface_hlab_users _hlabUserRepo;
        private readonly ILogger<HLogin> _logger;

        public HLogin(IHttpContextAccessor httpContextAccessor, Interface_hlab_users hlabUserRepo, ILogger<HLogin> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _hlabUserRepo = hlabUserRepo;
            _logger = logger;
        }

        public hlab_users GenerateLoginWindowsAuthenticationUser()
        {
            try
            {
                //var userName = _httpContextAccessor.HttpContext.User.Identity.Name.Split("\\");
                // hlabUser = _hlabUserRepo.GetUserAuthentication(userName[1], "");

                hlab_users hlabUser = _hlabUserRepo.GetUserAuthentication("labtech02", "D@iryM1lk");

                return hlabUser;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HLogin > GenerateLoginWindowsAuthenticationUser(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}

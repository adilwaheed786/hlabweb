using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabWebApi.Models;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HorizonLabWebApi.ApiFilter;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [ServiceFilter(typeof(APIKeyHandlers))]
    [Route("hlab_auth")]
    [ApiController]
    public class HlabAuthController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private Interface_hlab_users _hlabUserRepo;
        private readonly ILogger<HlabTestTransactionsController> _logger;

        public HlabAuthController(IHttpContextAccessor httpContextAccessor, Interface_hlab_users hlabUserRepo, ILogger<HlabTestTransactionsController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _hlabUserRepo = hlabUserRepo;
            _logger = logger;
        }
        
        [HttpGet("authenticateuser")]
        public hlab_users authenticateuser(string username)
        {
            try
            {
                hlab_users hlabUser = _hlabUserRepo.GetUserAuthentication(username, "");
                return hlabUser;
            }
            catch (Exception exc)
            {
                _logger.LogError("Authentication Error: " + exc.Message);
                return null;
            }
        }
    }
}

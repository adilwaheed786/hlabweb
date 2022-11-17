using HorizonLabAdmin.Helpers.Utilities.Session;
using HorizonLabAdmin.Interfaces.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HSession : TestTransactionSession, IHorizonLabSession, IUserSession, ITestTransactionSession, INavigationSession, ICustomerSession, IRequestSession
    {
        private readonly ILogger<HSession> _logger;
        public HSession(IHttpContextAccessor _httpContextAccessor, ILogger<HSession> logger) : base(_httpContextAccessor, logger) {
            _logger = logger;
        }
    }
}

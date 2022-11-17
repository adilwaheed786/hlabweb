using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    [Route("hlab_test_project")]
    [ApiController]
    public class TestPackageApiController : ControllerBase
    {
        public readonly Interface_test_projects _testProject;
        private readonly ILogger<TestPackageApiController> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;

        public TestPackageApiController(
            IConfiguration appConfig,
            ILogger<TestPackageApiController> logger,
            IHostingEnvironment env,
            Interface_test_projects testProject
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _testProject = testProject;
        }

        [HttpPost("addnewproject")]
        public bool addnewproject(hlab_test_projects new_project)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _testProject.AddNewTestPorject(new_project);
            }
            catch (Exception xc)
            {
                return false;
            }
        }
    }
}
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
    [Route("hlab_settings_api")]
    [ApiController]
    public class SettingsApiController : ControllerBase
    {
        private readonly Interface_EmailSender _Email;
        private readonly ILogger<SettingsApiController> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;

        public SettingsApiController(
            IConfiguration appConfig,
            ILogger<SettingsApiController> logger,
            IHostingEnvironment env,
            Interface_EmailSender Email
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _Email = Email;
        }

        [HttpGet("updateemailtemplate")]
        public bool updateemailtemplate(int templateid, bool status)
        {
            try
            {
                hlab_email_templates template = new hlab_email_templates();
                template = _Email.GetAllEmailTemplates().Where(x => x.id == templateid).FirstOrDefault();
                template.status = status;
                return _Email.UpdateEmailTemplate(template);                
            }
            catch (Exception xc)
            {
                return false;
            }
        }
    }
}
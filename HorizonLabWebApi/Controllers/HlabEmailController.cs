using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_email")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabEmailController : ControllerBase
    {
        private Interface_EmailSender _hlabEmail;
        private readonly ILogger<HlabEmailController> _logger;

        public HlabEmailController(
            Interface_EmailSender hlabEmail,
            ILogger<HlabEmailController> logger)
        {
            _hlabEmail = hlabEmail;
            _logger = logger;
        }

        [HttpGet("getallemailtemplates")]
        public List<hlab_email_templates> getallemailtemplates()
        {
            return _hlabEmail.GetAllEmailTemplates().ToList();
        }

        [HttpGet("getallemaillogs")]
        public List<emaillogview> getallemaillogs()
        {
            return _hlabEmail.GetAllEmailLogs().ToList();
        }

        [HttpPost("getemailattachments")]
        public List<hlab_email_file_attachments> getemailattachments(hlab_email_file_attachments efa)
        {
            return _hlabEmail.GetEmailFileAttachments(efa).ToList();
        }

        [HttpPost("insertnewtemplate")]
        public int insertnewtemplate(hlab_email_templates template)
        {
            return _hlabEmail.InsertNewEmailTemplate(template);
        }

        [HttpPost("updatetemplate")]
        public bool updatetemplate(hlab_email_templates template)
        {
            return _hlabEmail.UpdateEmailTemplate(template);
        }

        [HttpPost("insertfileattachment")]
        public bool insertfileattachment(hlab_email_file_attachments file)
        {
            return _hlabEmail.InsertFileAttachment(file);
        }

        [HttpPost("deleteallfileattachments")]
        public bool deleteallfileattachments(hlab_email_file_attachments efa)
        {
            return _hlabEmail.DeleteAllFileAttachments(efa);
        }

        [HttpPost("logemail")]
        public void logemail(hlab_email_log emailog)
        {
            try
            {
                _hlabEmail.LogEmail(emailog);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
            }
        }
    }
}
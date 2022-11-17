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
    [Route("hlab_default_parameter")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabDefaultParameterController : ControllerBase
    {
        private Interface_hlab_test_default_pkg_params _hlabDefaultParams;
        private readonly ILogger<HlabDefaultParameterController> _logger;

        public HlabDefaultParameterController(
            Interface_hlab_test_default_pkg_params hlabDefaultParams,
            ILogger<HlabDefaultParameterController> logger)
        {
            _hlabDefaultParams = hlabDefaultParams;            
            _logger = logger;
        }


        [HttpGet("getdefaultparameters")]
        public List<sp_getdefaultpackageparameters> GetDefaultParameters(int package_id)
        {
            try
            {
                List<sp_getdefaultpackageparameters> defaults = _hlabDefaultParams.GetDefaultTestParams(Convert.ToInt32(package_id)).ToList();
                return defaults;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("adddefaultparam")]
        public ActionResult AddDefaultParam(hlab_test_default_pkg_params param)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("hlab_test_default_pkg_params : Not a valid model");
                bool result = _hlabDefaultParams.AssignDefaultParam(param);
                if (result) return Ok();
                return BadRequest("HlabDefaultParameterController : AddDefaultParam Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabDefaultParameterController > AddDefaultParam Exception Error: " + xc);
            }
        }

        [HttpGet("deletedefaultparam")]
        public ActionResult DeleteDefaultParam(int paramid)
        {
            try
            {
                hlab_test_default_pkg_params param = new hlab_test_default_pkg_params();
                param.id = paramid;
                bool result = _hlabDefaultParams.DeleteDefaultParam(param);
                if (result) return Ok();
                return BadRequest("HlabDefaultParameterController : DeleteDefaultParam Error");
            }
            catch (Exception xc)
            {
                return BadRequest("HlabDefaultParameterController > DeleteDefaultParam Exception Error: " + xc);
            }
        }
    }
}
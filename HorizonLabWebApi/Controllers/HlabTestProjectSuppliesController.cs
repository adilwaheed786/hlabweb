using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_test_project_supplies")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]

    public class HlabTestProjectSuppliesController : ControllerBase
    {
        private Interface_hlab_project_supply _hlabTestProjectsSupply;
        private readonly ILogger<HlabTestProjectSuppliesController> _logger;

        public HlabTestProjectSuppliesController(Interface_hlab_project_supply hlabTestProjectsSupply, ILogger<HlabTestProjectSuppliesController> logger)
        {
            _hlabTestProjectsSupply = hlabTestProjectsSupply;
            _logger = logger;
        }

        [HttpGet("deletetransactionsupplies")]
        public bool DeleteTransactionSupplies(int proj_form_id = 0)
        {
            try
            {
                if (proj_form_id == 0) return false;
                bool result = _hlabTestProjectsSupply.DeleteProjectSupplies(proj_form_id);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpPost("addtransactionsupplies")]
        public bool AddTransactionSupplies(project_supply_form param)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabTestProjectsSupply.AddProjectSupplies(param);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.Message);
                return false;
            }
        }        
    }
}
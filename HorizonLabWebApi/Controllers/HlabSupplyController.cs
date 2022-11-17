using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_package_supply")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabSupplyController : ControllerBase
    {
        private Interface_hlab_supplies _hlabSupply;
        private readonly ILogger<HlabSupplyController> _logger;

        public HlabSupplyController(Interface_hlab_supplies hlabSupply, ILogger<HlabSupplyController> logger)
        {
            _hlabSupply = hlabSupply;
            _logger = logger;
        }

        [HttpPost("addnewsupply")]
        public bool AddNewSupply(hlab_supplies supply)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabSupply.AddNewSupply(supply);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("addsupplylist")]
        public bool AddSupplyList(test_pkg_supply_param supplylist)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabSupply.AddTestPackageSupplies(supplylist);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpGet("deletetestpackagesupply")]
        public bool DeleteTestPackageSupplyList(int supply_id)
        {
            try
            {
                return _hlabSupply.DeleteTestPackageSupplies(supply_id);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpGet("deletesupply")]
        public bool DeleteSupply(int supplyid)
        {
            try
            {
                return _hlabSupply.DeleteSupply(supplyid);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("updatesupply")]
        public bool UpdateSupply(hlab_supplies supply)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabSupply.UpdateSupply(supply);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpGet("getallsupplies")]
        public List<hlab_supplies> GetAllSupplies()
        {
            try
            {
                return _hlabSupply.GetAllTestPackageSupplies().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("getfilteredsupplies")]
        public List<testpackagesupplyview> GetFilteredSupplies(testpackagesupplyview supply_filter)
        {
            try
            {
                return _hlabSupply.GetFilteredTestPackageSupplies(supply_filter).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

    }
}
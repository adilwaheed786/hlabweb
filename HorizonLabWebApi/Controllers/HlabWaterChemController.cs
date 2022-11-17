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
    [Route("hlab_water_chem")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabWaterChemController : ControllerBase
    {
        private Interface_water_chem_result _hlabWaterChemResult;
        private readonly ILogger<HlabWaterChemController> _logger;

        public HlabWaterChemController(
            Interface_water_chem_result hlabWaterChemResult,
            ILogger<HlabWaterChemController> logger)
        {
            _hlabWaterChemResult = hlabWaterChemResult;
            _logger = logger;
        }

        //SELECT
        [HttpGet("getwaterchemresulta")]
        public List<hlab_chem_water_results_set_a> GetWaterChemResultA(int transid)
        {
            try
            {
                List<hlab_chem_water_results_set_a> results = _hlabWaterChemResult.GetWaterChemResultA(transid).ToList();
                return results;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getwaterchemresultb")]
        public List<hlab_chem_water_results_set_b> GetWaterChemResultB(int transid)
        {
            try
            {
                List<hlab_chem_water_results_set_b> results = _hlabWaterChemResult.GetWaterChemResultB(transid).ToList();
                return results;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettracemetalresults")]
        public List<hlab_trace_metal_results> GetTraceMetalResults(int transid)
        {
            try
            {
                List<hlab_trace_metal_results> results = _hlabWaterChemResult.GetTraceMetalResults(transid).ToList();
                return results;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        //ADD
        [HttpPost("addwaterchema")]
        public ActionResult AddWaterChemA(hlab_chem_water_results_set_a test_result)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("AddWaterChemA : Not a valid model");
                bool result = _hlabWaterChemResult.AddWaterChemA(test_result);
                if (result) return Ok();
                return BadRequest("HlabWaterChemController  : AddWaterChemA Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddWaterChemA Exception Error: " + xc);
            }
        }

        [HttpPost("addwaterchemb")]
        public ActionResult AddWaterChemB(hlab_chem_water_results_set_b test_result)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("AddWaterChemB : Not a valid model");
                bool result = _hlabWaterChemResult.AddWaterChemB(test_result);
                if (result) return Ok();
                return BadRequest("HlabWaterChemController : AddWaterChemB Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddWaterChemB Exception Error: " + xc);
            }
        }

        [HttpPost("addtracemetalresults")]
        public ActionResult AddTraceMetalResults(hlab_trace_metal_results test_result)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("AddTraceMetalResults : Not a valid model");
                bool result = _hlabWaterChemResult.AddTraceMetalResults(test_result);
                if (result) return Ok();
                return BadRequest("HlabWaterChemController  : AddTraceMetalResults Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddTraceMetalResults Exception Error: " + xc);
            }
        }

        //UPDATE
        [HttpPost("updatewaterchema")]
        public ActionResult UpdateWaterChemA(hlab_chem_water_results_set_a test_result)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateWaterChemA : Not a valid model");
                bool result = _hlabWaterChemResult.UpdateWaterChemA(test_result);
                if (result) return Ok();
                return BadRequest("HlabWaterChemController  : UpdateWaterChemA Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateWaterChemA Exception Error: " + xc);
            }
        }

        [HttpPost("updatewaterchemb")]
        public ActionResult UpdateWaterChemB(hlab_chem_water_results_set_b test_result)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateWaterChemB : Not a valid model");
                bool result = _hlabWaterChemResult.UpdateWaterChemB(test_result);
                if (result) return Ok();
                return BadRequest("HlabWaterChemController  : UpdateWaterChemB Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateWaterChemB Exception Error: " + xc);
            }
        }

        [HttpPost("updatetracemetalresults")]
        public ActionResult UpdateTraceMetalResults(hlab_trace_metal_results test_result)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateTraceMetalResults : Not a valid model");
                bool result = _hlabWaterChemResult.UpdateTraceMetalResults(test_result);
                if (result) return Ok();
                return BadRequest("HlabWaterChemController  : UpdateTraceMetalResults Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateTraceMetalResults Exception Error: " + xc);
            }
        }

        //DELETE
        [HttpGet("deletereseedwaterchema")]
        public bool DeleteReseedWaterChemA(int transid = 0)
        {
            try
            {
                if (transid == 0) return false;
                bool result = _hlabWaterChemResult.DeleteReseedWaterChemA(transid);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpGet("deletereseedwaterchemb")]
        public bool DeleteReseedWaterChemB(int transid = 0)
        {
            try
            {
                if (transid == 0) return false;
                bool result = _hlabWaterChemResult.DeleteReseedWaterChemB(transid);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpGet("deletereseedtracemetalresults")]
        public bool DeleteReseedTraceMetalResults(int transid = 0)
        {
            try
            {
                if (transid == 0) return false;
                bool result = _hlabWaterChemResult.DeleteReseedTraceMetalResults(transid);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    [Route("hlab_test_transaction")]
    [ApiController]

    public class TestTransactionApiController : ControllerBase
    {
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_test_results _hlabTestResult;
        private readonly Interface_test_package _hlabPkgCtgry;     
        private readonly IHostingEnvironment _env;
        private readonly Interface_hlab_customers _hlabCustomers;
        private readonly Interface_hlab_invoice _hlabInvoice;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_unit_of_measurement _UnitOfMeasurement;
        private readonly IConfiguration _appConfig;
        private readonly ILogger<TestTransactionApiController> _logger;

        public TestTransactionApiController(
            IConfiguration appConfig,
            IHostingEnvironment env,
            Interface_test_transactions hlabTestTransRepo,
            Interface_test_package hlabPkgCtgry,
            Interface_test_results hlabTestResult,
            Interface_hlab_customers hlabCustomers,
            Interface_hlab_invoice hlabInvoice,
            Interface_hlab_orders hlabOrderRepo,
            Interface_unit_of_measurement UnitOfMeasurement,
            ILogger<TestTransactionApiController> logger)
        {
            _hlabPkgCtgry = hlabPkgCtgry;
            _hlabTestResult = hlabTestResult;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabOrderRepo = hlabOrderRepo;
            _env = env;
            _hlabCustomers = hlabCustomers;
            _hlabInvoice = hlabInvoice;
            _appConfig = appConfig;
            _UnitOfMeasurement = UnitOfMeasurement;
            _logger = logger;
        }

        [HttpPost("savetestresultchanges")]
        public ActionResult SaveTestResult(hlab_test_results hlab_test_results)
        {
            try
            {
                var submitresult = _hlabTestResult.UpdateTestResults(hlab_test_results);
                if (submitresult)
                {
                    return Ok();
                }
                return BadRequest("SaveTestResult : SaveTestResult Error"); ;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return BadRequest("SaveTestResult : SaveTestResult Error"); 
        }

        [HttpGet("addnewunitofmeasurement")]
        public int? addnewunitofmeasurement(string new_unit)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                hlab_test_measurement_units unit = new hlab_test_measurement_units();
                unit.unit_of_measurement= new_unit;
                return _UnitOfMeasurement.AddNewUnitofMeasurement(unit);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return 0;
            }
        }

        [HttpGet("getwaterbacteriadetails")]
        public sp_gethorizonlabtransactiondetails GetWaterBacteriaDetails(int transid)
        {
            try
            {
                return _hlabTestTransRepo.GetTransactionDetails(transid);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpGet("getwaterbacteriatestresults")]
        public List<testresultsview> GetWaterBacteriaTestResults(int transid)
        {
            try
            {
                testresultsview result_param = new testresultsview();
                result_param.trans_id = transid;
                return _hlabTestResult.GetAllTestResults(result_param).ToList();
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return null;
            }
        }

        [HttpPost("updatewatertesttransaction")]
        public bool updatewatertesttransaction(hlab_test_transactions water_test)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabTestTransRepo.UpdateTransactionDetails(water_test);
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpGet("deletesubsidyformimage")]
        public bool DeleteSubsidyFormImage(int id)
        {
            try
            {

                bool result = _hlabTestTransRepo.DeleteSubsidyFormImage(id);
                return result;
            }
            catch (Exception xc)
            {
                return false;
            }
        }
    }
}
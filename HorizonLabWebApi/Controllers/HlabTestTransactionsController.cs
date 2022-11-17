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
    [Route("hlab_test_transactions")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabTestTransactionsController : ControllerBase
    {
        private Interface_test_transactions _hlabTestTransactions;
        private Interface_test_results _hlabTestResults;          
        private readonly ILogger<HlabTestTransactionsController> _logger;

        public HlabTestTransactionsController(
            Interface_test_transactions hlabTestTransactions, 
            Interface_test_results hlabTestResults, 
            ILogger<HlabTestTransactionsController> logger)
        {
            _hlabTestTransactions = hlabTestTransactions;
            _hlabTestResults = hlabTestResults;
            _logger = logger;
        }

        [HttpPost("getalltransactionlist")]
        public List<testtransactionsview> GetTransactionList(test_transaction htt)
        {
            try
            {
                htt.status = true; //active trsnaction records only
                List<testtransactionsview> transactionList = _hlabTestTransactions.GetAllTransactions(htt).ToList();
                return transactionList;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }

        }

        [HttpPost("getsemipublictransactionlist")]
        public List<sp_getsemipublicreport> GetSemiPublicTransactionsList(test_transaction htt)
        {
            try
            {
                htt.status = true; //active trsnaction records only
                List<sp_getsemipublicreport> transactionList = _hlabTestTransactions.GetSemiPublicTransactions(htt).ToList();
                return transactionList;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpPost("getmonthlysubsidytransactionlist")]
        public List<sp_getmonthlysubsidyreport> GetMonthlySubsidyTransactionsList(test_transaction htt)
        {
            try
            {
                htt.status = true; //active trsnaction records only
                List<sp_getmonthlysubsidyreport> transactionList = _hlabTestTransactions.GetMonthlySubsidyReport(htt).ToList();
                return transactionList;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpPost("gettestresult")]
        public List<sp_gettestresults> GetTestResult(testresultsview param)
        {
            try
            {
                List<sp_gettestresults> test_result_list = _hlabTestResults.GetTestResults(param.trans_id).ToList();
                return test_result_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("gettesttransactionsupplieids")]
        public List<hlab_transaction_supplies> GetTestTransactionSupplieIds(hlab_transaction_supplies param)
        {
            try
            {
                List<hlab_transaction_supplies> list_supply_ids = _hlabTestTransactions.GetTransactionSuppliesIds(param).ToList();
                return list_supply_ids;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettransactiondetails")]
        public sp_gethorizonlabtransactiondetails GetTransactionDetails(int trans_id)
        {
            try
            {
                sp_gethorizonlabtransactiondetails trans_details = _hlabTestTransactions.GetTransactionDetails(Convert.ToInt32(trans_id));
                return trans_details;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("gettestrequestsupplylist")]
        public List<testrequestsupplyview> GetTestRequestSupplyList(int reqid, int formid)
        {
            try
            {
                return _hlabTestTransactions.GetTestRequestSupplyList(reqid, formid);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getdefaultparameters")]
        public List<sp_getdefaultpackageparameters> GetDefaultParameters(int package_id)
        {
            try
            {
                List<sp_getdefaultpackageparameters> defaults = _hlabTestTransactions.GetDefaultParameters(Convert.ToInt32(package_id)).ToList();
                return defaults;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("updatetesttransaction")]
        public ActionResult UpdateTestTransaction(hlab_test_transactions transaction)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateTestTransaction : Not a valid model");
                bool result = _hlabTestTransactions.UpdateTransactionDetails(transaction);
                if (result) return Ok();
                return BadRequest("UpdateTestTransaction : UpdateTestTransaction Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateUser Exception Error: " + xc);
            }
        }


        [HttpGet("publishtesttransaction")]
        public bool publishtesttransaction(int transactionid)
        {
            try
            {
                bool result = _hlabTestTransactions.PublishTestTransaction(transactionid);
                return result;                
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpPost("ifwatersampleexists")]
        public ActionResult IfWaterSampleExists(hlab_test_transactions transaction)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("UpdateTestTransaction : Not a valid model");
                bool result = _hlabTestTransactions.IfWaterSampleExists(transaction);
                if (result) return Ok();
                return BadRequest("UpdateTestTransaction : IfWaterSampleExists Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateUser Exception Error: " + xc);
            }
        }

        [HttpPost("updatetestresult")]
        public ActionResult UpdateTestResult(hlab_test_results inputresult)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("updatetestresult : Not a valid model");
                bool result = _hlabTestResults.UpdateTestResults(inputresult);
                if (result) return Ok();
                return BadRequest("UpdateTestTransaction : UpdateTestResult Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateTestResult Exception Error: " + xc);
            }
        }

        [HttpPost("addtestresult")]
        public ActionResult AddTestResult(hlab_test_results inputresult)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("updatetestresult : Not a valid model");
                bool result = _hlabTestResults.AddTestResults(inputresult);
                if (result) return Ok();
                return BadRequest("UpdateTestTransaction : UpdateTestResult Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateTestResult Exception Error: " + xc);
            }
        }

        [HttpGet("deletetestresults")]
        public bool DeleteTestResults(int resultid=0)
        {
            try
            {
                if (resultid == 0) return false;
                bool result = _hlabTestResults.DeleteTestResults(resultid);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpGet("deletetransactionsupplies")]
        public bool DeleteTransactionSupplies(int transactionid = 0)
        {
            try
            {
                if (transactionid == 0) return false;
                bool result = _hlabTestTransactions.DeleteTransactionSupplies(transactionid);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpGet("deletetestresultsbytransid")]
        public bool DeleteTestResultsByTransId(int transid = 0)
        {
            try
            {
                if (transid == 0) return false;
                bool result = _hlabTestResults.DeleteTestResultsByTransId(transid);
                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }        

        [HttpPost("addtesttransaction")]
        public int AddTestTransaction(hlab_test_transactions htt)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                return _hlabTestTransactions.AddTransactionDetails(htt);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.Message);
                return 0;
            }
        }

        [HttpPost("addtransactionsupplies")]
        public bool AddTransactionSupplies(transaction_supply_param param)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabTestTransactions.AddTransactionSupplies(param);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.Message);
                return false;
            }
        }

        [HttpPost("addsubsidyformimage")]
        public ActionResult AddSubsidyFormImage(hlab_water_subsidy_form_images subsidy)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("addsubsidyformimage : Not a valid model");
                bool result = _hlabTestTransactions.AddSubsidyFormImage(subsidy);
                if (result) return Ok();
                return BadRequest("AddSubsidyFormImage : AddSubsidyFormImage Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddSubsidyFormImage Exception Error: " + xc);
            }
        }

        [HttpPost("updatesubsidyformimage")]
        public ActionResult UpdateSubsidyFormImage(hlab_water_subsidy_form_images subsidy)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("updatesubsidyformimage : Not a valid model");
                bool result = _hlabTestTransactions.UpdateSubsidyFormImage(subsidy);
                if (result) return Ok();
                return BadRequest("UpdateSubsidyFormImage : UpdateSubsidyFormImage Error");
            }
            catch (Exception xc)
            {
                return BadRequest("UpdateSubsidyFormImage Exception Error: " + xc);
            }
        }

        [HttpGet("deletesubsidyformimage")]
        public bool DeleteSubsidyFormImage(int id)
        {
            try
            {
                
                bool result = _hlabTestTransactions.DeleteSubsidyFormImage(id);
                return result;
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpGet("gettesttranstypes")]
        public List<hlab_test_transaction_types> gettesttranstypes()
        {
            try
            {
                List<hlab_test_transaction_types> trantypes = _hlabTestTransactions.GetTestTransactionTypes().ToList();
                return trantypes;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }
    }
}
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HTestResult : ITestResult
    {
        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        Interface_test_results _hlabTestResult;
        private readonly ILogger<HTestResult> _logger;

        public HTestResult(IHorizonLabSession sessionHelper, IUtility utility, Interface_test_results hlabTestResult, ILogger<HTestResult> logger)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _hlabTestResult = hlabTestResult;
            _logger = logger;
        }

        public TestResultPageViewModel GetTestResultListFromRequestItems(List<orderdetailsview> request_item_list, TestResultPageViewModel page_model)
        {
            try
            {
                testresultsview result_param = new testresultsview();
                foreach (var item in request_item_list)
                {
                    var results = new List<testresultsview>();
                    result_param.trans_id = item.trans_id ?? 0;
                    results = _hlabTestResult.GetAllTestResults(result_param).ToList();
                    foreach (var res in results)
                    {
                        page_model.result_list.Add(res);
                    }
                }
                return page_model;
            }
            catch(Exception exc)
            {
                _logger.LogError($"HTestResult > GetTestResultListFromRequestItems(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public bool AddNewTestResultToDb(hlab_test_results test_result_obj)
        {
            try
            {
                return _hlabTestResult.AddTestResults(test_result_obj);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestResult > GetTestResultListFromRequestItems(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateTestResultDb(hlab_test_results test_result_obj)
        {
            try
            {
                return _hlabTestResult.UpdateTestResults(test_result_obj);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestResult > UpdateTestResultDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool DeleteTestResultDb(int result_id)
        {
            try
            {
                return _hlabTestResult.DeleteTestResults(result_id);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestResult > DeleteTestResultDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<testresultsview> GetTestResultFromDb(int transactionid)
        {
            try
            {
                return _hlabTestResult.GetAllTestResults(new testresultsview { trans_id = transactionid }).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestResult > DeleteTestResultDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}

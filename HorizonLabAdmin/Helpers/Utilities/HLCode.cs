using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HLCode : IHLCode
    {
        private readonly IUtility _utility;
        private readonly ICustomer _customer;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_test_class _testClassRepo;
        private readonly Interface_test_package _testPackageRepo;
        private readonly ILogger<HLCode> _logger;

        public HLCode(
            IUtility utility,
            Interface_hlab_orders hlabOrderRepo,
            ICustomer customer,
            Interface_test_class testClassRepo,
            Interface_test_package testPackageRepo,
            ILogger<HLCode> logger)
        {
            _utility = utility;
            _hlabOrderRepo = hlabOrderRepo;
            _customer = customer;
            _testClassRepo = testClassRepo;
            _testPackageRepo = testPackageRepo;
            _logger = logger;
        }

        public string GenerateHLCode(HLCodeParameters hlcode_param)
        {
            DateTime today = DateTime.Now;
            string hl_code_suffix = "", hl_code = "", str_date_code = "";
            int letterA = 65, letterZ = 90, alphabet_count = 26;
            int unicode1 = 0, counter = 0, unicode2 = letterA;

            try
            {
                str_date_code = GenerateDateStringCode(DateTime.Now);

                hl_code_suffix = $"{hlcode_param.request_count_today}-{hlcode_param.customer_request_count + 1}";

                hl_code = $"{hlcode_param.hl_code_prefix}-{str_date_code}-{hl_code_suffix}";
                return hl_code;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HLCode > GenerateHLCode(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        private bool IsNewCustomerToday(int customerid, int test_pkg_id)
        {
            try
            {
                List<orderdetailsview> request_list = GetTodaysCustomerAllRequestDetails(customerid, test_pkg_id);

                if (request_list.Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception exc)
            {
                _logger.LogError($"HLCode > IsNewCustomerToday(): {exc.InnerException}");
                throw exc.InnerException;
            }

        }

        private List<orderdetailsview> GetTodaysCustomerAllRequestDetails(int customerid = 0, int test_pkg_id = 0)
        {
            try
            {
                List<orderdetailsview> request_list = _hlabOrderRepo.GetOrderItems(
                        new orderdetailsview
                        {
                            order_date = DateTime.Now.Date,
                            trans_id = 0,
                            order_id = 0,
                            customer_id = customerid
                        }
                    ).Where(x => x.pkg_id == test_pkg_id).ToList();
                return request_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HLCode > GetTodaysCustomerAllRequestDetails(): {exc.InnerException}");
                throw exc.InnerException;
            }

        }

        private string GetCurrentHLCodeSuffix(int customerid = 0, int test_pkg_id = 0)
        {
            try
            {
                List<orderdetailsview> request_list = GetTodaysCustomerAllRequestDetails(customerid, test_pkg_id);

                if (request_list.Count == 0) return "A";

                string current_hl_code = request_list[0].hl_code;

                if (string.IsNullOrEmpty(current_hl_code)) return "A";

                string[] hl_code_details = current_hl_code.Split("-");
                return hl_code_details[2];
            }
            catch (Exception exc)
            {
                _logger.LogError($"HLCode > GetCurrentHLCodeSuffix Exception Error: {exc.Message}");
                throw exc.InnerException;
            }            
        }

        private string CreateNewHLSuffixCode(int request_count_today)
        {
            try
            {
                string hl_code_suffix = "";
                int letterA = 65, letterZ = 90, alphabet_count = 26;
                int unicode1 = 0, counter = 0, unicode2 = letterA;
                hl_code_suffix = _utility.ConvertAsciiCodeToString(unicode2);

                for (int x = 0; x < request_count_today; x++)
                {
                    unicode2++;

                    if (unicode2 > letterZ)
                    {
                        unicode2 = letterA;
                        unicode1 = letterA + counter;
                        counter++;
                    }
                    hl_code_suffix = _utility.ConvertAsciiCodeToString(unicode2);

                    if (request_count_today > alphabet_count)
                    {
                        hl_code_suffix = $"{_utility.ConvertAsciiCodeToString(unicode1)}{_utility.ConvertAsciiCodeToString(unicode2)}";
                    }
                }
                return hl_code_suffix;
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }
        }

        private string GenerateDateStringCode(DateTime datetime)
        {
            try
            {
                DateTime date = datetime.Date;
                string day = date.Day.ToString("00");
                string month = date.Month.ToString("00");
                string year = $"{datetime:yy}";

                return $"{day}{month}{year}";
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }
        }
    }
}

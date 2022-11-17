using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HDashboard : IDashboard
    {
        private readonly IUtility _utility;        
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_test_results _hlabTestResult;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly IRequest _requestHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly ICustomer _customerHelper;
        private readonly ITestPackage _testPackageHelper;
        private readonly Interface_hlab_cities _city;
        private readonly Interface_hlab_provinces _province;
        private readonly Interface_test_package _hlabPkgCtgry;
        private readonly ILogger<HDashboard> _logger;
        private readonly int _manitoba_province_id = 3;

        public HDashboard(
            IHorizonLabSession sessionHelper, 
            IUtility utility, 
            Interface_test_transactions hlabTestTransRepo, 
            Interface_test_results hlabTestResult, 
            Interface_hlab_orders hlabOrderRepo,
            IRequest requestHelper,
            IRequestItem requestItemHelper,
            ICustomer customerHelper,
            Interface_test_package hlabPkgCtgry,
            Interface_hlab_test_payments hlabPayment,
            ITestPackage testPackageHelper,
            Interface_hlab_cities city,
            Interface_hlab_provinces province,
            ILogger<HDashboard> logger
            )
        {            
            _sessionHelper = sessionHelper;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabTestResult = hlabTestResult;
            _hlabOrderRepo = hlabOrderRepo;
            _utility = utility;
            _requestHelper = requestHelper;
            _requestItemHelper = requestItemHelper;
            _customerHelper = customerHelper;
            _hlabPkgCtgry = hlabPkgCtgry;
            _hlabPayment = hlabPayment;
            _testPackageHelper = testPackageHelper;
            _province = province;
            _city = city;
            _logger = logger;
        }

        public DashboardDataView AssignDashboardSelectedSearchValues(DashboardDataView data_view)
        {
            if(data_view.search_customer_id == 0) data_view.search_customer_id = _sessionHelper.GetSearchCustomerId();
            if(data_view.search_request_id == 0) data_view.search_transaction_id = _sessionHelper.GetIntSearchWaterBacteriaTransactionId();
            if(data_view.search_transaction_id == 0) data_view.search_customer_id = _sessionHelper.GetSearchCustomerId();
            if (string.IsNullOrEmpty(data_view.search_customer_firstname)) data_view.search_customer_firstname = _sessionHelper.GetSearchCustomerFirstName();
            if (string.IsNullOrEmpty(data_view.search_customer_lastname)) data_view.search_customer_firstname = _sessionHelper.GetSearchCustomerLastName();
            return data_view;
        }

        public DashboardDataView GenerateDashboardViewDataObject(DashboardDataView view_data)
        {
            view_data = InitializeDashboardDataView(view_data);
            view_data = RetrieveDashboardDataView(view_data);
            return view_data;
        }

        public DashboardDataView GenerateHTMLDropDownItems(DashboardDataView view_data)
        {
            var revised_customer_list = view_data.customer_list.Select(x => new SelectListItem {
                Value = x.customer_id.ToString(),
                Text = $"{x.first_name} {x.last_name}"
            });

            var revised_request_list = view_data.request_list.Select(x => new SelectListItem
            {
                Value = x.order_id.ToString(),
                Text = $"{_utility.FormatIntIDToString(x.order_id)}     {_utility.FormatDateTimeToMMDDYYString(x.order_date)}     {_utility.FormatDecimalToMoneyString(x.total_amount)}"
            });
            
            view_data.customer_select_list_item = _utility.GenerateSelectListItem(revised_customer_list, "Value", "Text");
            view_data.request_select_list_item = _utility.GenerateSelectListItem(revised_request_list, "Value", "Text");
            view_data.request_item_select_list_item = _utility.GenerateSelectListItem(view_data.request_detail_list, "trans_id", "lab_code");
            return view_data;
        }

        public void SetSearchDashboardValuesToSession(DashboardDataView view_data)
        {
            _sessionHelper.SetSearchCustomerSessionInfo(new hlab_customers
            {
                customer_id = view_data.search_customer_id,
                first_name = view_data.search_customer_firstname,
                last_name = view_data.search_customer_lastname
            });

            _sessionHelper.SetTestRequestSessionInfo(new RequestSessionParameter
            {
                request_id = view_data.search_request_id,
                request_date_end = null,
                request_date_start = null,
                request_item_id = 0
            });

            _sessionHelper.SetIntSearchWaterBacteriaTransactionId(view_data.search_transaction_id);
        }

        private DashboardDataView InitializeDashboardDataView(DashboardDataView view_data)
        {
            try
            {
                //AssignSearchParameters(view_data);
                view_data.request_list = new List<ordersummaryview>();
                view_data.request_detail_list = new List<orderdetailsview>();
                view_data.test_transaction = new sp_gethorizonlabtransactiondetails();
                view_data.test_result_list = new List<sp_gettestresults>();

                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HDashboard > InitializeDashboardDataView(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        private DashboardDataView AssignSearchParameters(DashboardDataView view_data)
        {
            view_data = _customerHelper.AssignDashboardCustomerSearchParameters(view_data);
            view_data = _requestHelper.AssignDashboardRequestSearchParameters(view_data);
            view_data = _requestItemHelper.AssignDashboardRequestItemSearchParameters(view_data);
            return view_data;
        }

        private DashboardDataView RetrieveDashboardDataView (DashboardDataView view_data)
        {
            try
            {
                view_data.city_selectlist_item = _utility.GenerateSelectListItem(_city.GetAllCities(_manitoba_province_id).ToList(), "id", "city");  
                view_data.city_selectlist_item.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

                view_data.province_selectlist_item = _utility.GenerateSelectListItem(_province.GetAllProvinces().ToList(), "id", "province");
                view_data.province_selectlist_item = _utility.SetSelectedItemFromList(view_data.province_selectlist_item, _manitoba_province_id.ToString());

                view_data.truefalse_selectlist_item = _utility.GenerateTrueFalseSelectList();

                view_data.package_category_selectlist_item = _testPackageHelper.GeneratePackageCategorySelectListItem();
                view_data.payment_type_selectlist_item = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentTypes().ToList(), "id", "payment");
                view_data.customer_list = _customerHelper.GetFilteredCustomersList(
                        new horizonlabcustomerview {
                            customer_id = view_data.search_customer_id,
                            first_name = view_data.search_customer_firstname,
                            last_name = view_data.search_customer_lastname,
                            status = true
                        }, "asc", "asc");


                view_data = GetCustomerRequests(view_data);
                view_data = GetRequestItems(view_data);
                view_data = GetTransactionSampleRecord(view_data);

                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HDashboard > RetrieveDashboardDataView(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        private DashboardDataView GetCustomerRequests(DashboardDataView view_data)
        {
            if (_customerHelper.IsCustomerSelected(view_data.search_customer_id))
            {
                view_data.request_list = _hlabOrderRepo.GetAllOrders(new ordersearch { customer_id = view_data.search_customer_id }).ToList();

                if (view_data.request_list != null && view_data.request_list.Count > 0) view_data.request_list = view_data.request_list.OrderByDescending(x => x.order_date).ToList();

                view_data.request_select_list_item = _utility.GenerateSelectListItem(view_data.request_list, "order_id", "order_id");
                foreach (var item in view_data.request_select_list_item)
                {
                    item.Text = $"Request ID:{item.Text}";
                }
                view_data.customer_select_list_item = _utility.GenerateSelectListItem(view_data.customer_list, "customer_id", "customer_name");
            }
            return view_data;
        }

        private DashboardDataView GetRequestItems(DashboardDataView view_data)
        {
            if (_requestHelper.IsRequestSelectedByUser(view_data.search_request_id))
            {
                view_data.request_detail_list = _hlabOrderRepo.GetOrderItems(new orderdetailsview { order_id = view_data.search_request_id }).ToList();
            }
            return view_data;
        }

        private DashboardDataView GetTransactionSampleRecord(DashboardDataView view_data)
        {
            if (_requestItemHelper.IsRequestItemSelectedByUser(view_data.search_transaction_id))
            {
                view_data = GetTestTransactionRecord(view_data.search_transaction_id, view_data);
            }
            return view_data;
        }

        private DashboardDataView GetTestTransactionRecord(int transactionid, DashboardDataView view_data)
        {
            try
            {
                view_data.test_transaction = _hlabTestTransRepo.GetTransactionDetails(view_data.search_transaction_id);

                if (view_data.test_transaction != null)
                {
                    view_data.test_result_list = _hlabTestResult.GetTestResults(view_data.test_transaction.trans_id).ToList();
                }
                    
                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HDashboard > GetTestTransactionRecord(): {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

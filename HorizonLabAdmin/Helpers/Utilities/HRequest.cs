using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HRequest:IRequest
    {
        private readonly IUtility _utility;        
        private readonly IHLCode _hlcode;        
        private readonly ICustomer _customer;        
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_test_class _testClassRepo;
        private readonly Interface_test_package _testPackageRepo;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly ILogger<HRequest> _logger;
        private readonly IHostingEnvironment _env;
        private readonly ISupply _supplyHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_test_projects _hlabTestProject;

        public bool proceed_csv_process { get; set; }
        public string InsertResult { get; set; }

        public HRequest(
            IHorizonLabSession sessionHelper, 
            IUtility utility, 
            Interface_hlab_orders hlabOrderRepo, 
            Interface_hlab_test_payments hlabPayment, 
            ICustomer customer, 
            Interface_test_class testClassRepo, 
            Interface_test_package testPackageRepo,
            IHLCode hlcode,
            IHostingEnvironment env,
            ILogger<HRequest> logger,
            IRequestItem requestItemHelper,
            ISupply supplyHelper,
            Interface_test_transactions hlabTestTransRepo,
            Interface_test_projects hlabTestProject)
        {            
            _sessionHelper = sessionHelper;
            _utility = utility;
            _hlabOrderRepo = hlabOrderRepo;
            _hlabPayment = hlabPayment;
            _customer = customer;
            _testClassRepo = testClassRepo;
            _testPackageRepo = testPackageRepo;
            _hlcode = hlcode;
            _logger = logger;
            _supplyHelper = supplyHelper;
            _requestItemHelper = requestItemHelper;
            _env = env;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabTestProject = hlabTestProject;
        }

        public int AddNewTestRequest(JsonTestRequestParametercs request)
        {
            try
            {
                int? request_id = _hlabOrderRepo.AddNewOrder(request.hlab_order_logs);
                ProcessRequestItemsParam param = new ProcessRequestItemsParam();
                if (!isRequestCreated(request_id)) return 0;

                param.request_item_list = request.hlab_order_items;
                param.request_id = (int)request_id;
                param.customer_id = (int)request.hlab_order_logs.customer_id;
                ProcessRequestItems(param);
                ProcessRequestPayments(request.hlab_test_payments, (int)request_id);
                return (int)request_id;
            }
            catch(Exception exc)
            {
                _logger.LogError($"HRequest > AddNewTestRequest(): {exc.Message}");
                return 0;
            }
        }

        private bool isRequestCreated(int? requestid)
        {
            if(requestid!=null && requestid != 0)
            {
                return true;
            }
            return false;
        }

        private void ProcessRequestItems(ProcessRequestItemsParam param)
        {
            try
            {
                int counter = 0;
                foreach (var item in param.request_item_list)
                {              
                    if (item.test_pkg_id != 0 && item.test_pkg_id != null)
                    {
                        item.hl_code = CreateHLCode(param.request_id, (int)item.test_pkg_id, counter);
                        counter = counter + 1;
                    }
                }

                foreach (var item in param.request_item_list)
                {
                    if (item.test_pkg_id != 0 && item.test_pkg_id != null)
                    {
                        item.order_id = param.request_id;
                        item.trans_id = 0;
                        _hlabOrderRepo.AddNewOrderItem(item);
                    }
                }
                
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > ProcessRequestItems Exception Error: {exc.Message}");
                throw exc.InnerException;
            }
        }

        private string CreateHLCode(int request_id, int test_pkg_id, int customer_request_count)
        {
            try
            {
                //int customer_request_count = 0; 
                int? request_count_today=0;
                ordersummaryview request = new ordersummaryview();
                hlab_test_pkgs test_package = new hlab_test_pkgs();
                hlab_test_pkgs_class test_package_class = new hlab_test_pkgs_class();
                string hl_code = "";

                request = _hlabOrderRepo.GetOrderInfo(request_id);
                test_package = _testPackageRepo.GetTestPackageDetails((int)test_pkg_id);
                test_package_class = _testClassRepo.GetTestClasses().Where(x => x.class_id == test_package.pkg_class_id).FirstOrDefault();

                //customer_request_count = _customer.GetTodaysCustomerRequestsCount((int)request.customer_id, test_pkg_id, test_package.hl_code_prefix);
                //request_count_today = _customer.GetTodaysTotalRequestsCount((int)request.customer_id, test_package.hl_code_prefix);                

                if (string.IsNullOrEmpty(test_package.hl_code_prefix)) test_package.hl_code_prefix = "XX";
                request_count_today = _hlabOrderRepo.CountTodaysRequests(DateTime.Now.Date, test_package.hl_code_prefix);
                hl_code = _hlcode.GenerateHLCode(new HLCodeParameters
                {
                    customer_request_count = customer_request_count,
                    request_count_today = (int)request_count_today,
                    hl_code_prefix = test_package.hl_code_prefix,
                    customer_id = (int)request.customer_id,
                    package_class_id = test_package_class.class_id,
                    test_pkg_id = test_pkg_id
                });
                return hl_code;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > CreateHLCode Exception Error: {exc.Message}");
                throw exc.InnerException;
            }
        }

        private void ProcessRequestPayments(List<hlab_test_payments> payment_list, int request_id)
        {
            try
            {
                foreach (var pay in payment_list)
                {
                    if (pay.paid_amount != 0 && !string.IsNullOrEmpty(pay.paid_amount.ToString()))
                    {
                        pay.order_id = request_id;
                        pay.payment_date = DateTime.Now;
                        _hlabPayment.AddPayment(pay);
                    }
                }
            }
            catch(Exception exc)
            {
                _logger.LogError($"HRequest > ProcessRequestPayments(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public DashboardDataView AssignDashboardRequestSearchParameters(DashboardDataView view_data)
        {
            try
            {
                if (view_data.search_request_id == 0)
                {
                    view_data.search_request_id = _sessionHelper.GetSearchRequestId();
                }

                if (view_data.search_request_id != 0)
                {
                    ordersummaryview request = new ordersummaryview();
                    request = _hlabOrderRepo.GetOrderInfo(view_data.search_request_id);

                    if (request != null)
                    {
                        view_data.search_customer_id = request.customer_id;
                        view_data.search_request_id = request.order_id;
                        view_data.search_customer_firstname = "";
                        view_data.search_customer_lastname = "";
                    }
                }
                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > AssignDashboardRequestSearchParameters(): {exc.Message}");
                throw exc.InnerException;
            }            
        }

        public ordersearch GenerateRequestSearchParameter()
        {
            try
            {
                ordersearch request = new ordersearch();
                request.order_id = _sessionHelper.GetSearchRequestId();
                request.customer_id = _sessionHelper.GetSearchCustomerId();
                request.Project_id = _sessionHelper.GetSelectedProjectId();
                if (_sessionHelper.IsSearchRequestStartDateHasValue()) request.start_order_date = _sessionHelper.GetSearchRequestStartDate();
                if (_sessionHelper.IsSearchRequestEndDateHasValue()) request.end_order_date = _sessionHelper.GetSearchRequestEndDate();

                if (_sessionHelper.IsSearchSubmitStartDateHasValue()) request.submtd_datetime_start = _sessionHelper.GetSessionSubmitStart();
                if (_sessionHelper.IsSearchSubmitEndDateHasValue()) request.submtd_datetime_end = _sessionHelper.GetSessionSubmitEnd();

                if (_sessionHelper.IsReceivedStartDateHasValue()) request.rcv_date_start = _sessionHelper.GetSessionReceivedStartDate();
                if (_sessionHelper.IsReceivedEndDateHasValue()) request.rcv_date_end = _sessionHelper.GetSessionReceviedEndDate();

                if (_sessionHelper.IsTestStartDateHasValue()) request.test_date_start = _sessionHelper.GetSessionTestStartDate();
                if (_sessionHelper.IsTestdEndDatHasValue()) request.test_date_end = _sessionHelper.GetSessionTestEndDate();
                
                if (_sessionHelper.IsProjectStartDateHasValue()) request.proj_date_start = _sessionHelper.GetSessionProjStartDate();
                if (_sessionHelper.IsProjectEndDateHasValue()) request.proj_date_end = _sessionHelper.GetSessionProjEndDate();

                if (_sessionHelper.IsSearchCustomerFirstNameHasValue()) request.first_name = _sessionHelper.GetSearchCustomerFirstName();
                if (_sessionHelper.IsSearchCustomerLastNameHasValue()) request.last_name = _sessionHelper.GetSearchCustomerLastName();

                return request;
            }
            catch(Exception exc)
            {
                _logger.LogError($"HRequest > GenerateRequestSearchParameter(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public orderdetailsview GetRequestInfoOfaTransaction(int transactionid)
        {
            try
            {
                return _hlabOrderRepo.GetOrderItems(new orderdetailsview { trans_id = transactionid, order_id = 0 }).FirstOrDefault();//have to set order id to 0, so results will be filtered by transaction id only
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > GetRequestInfoOfaTransaction(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool IsRequestIdNotEmpty(string requestid)
        {
            return !string.IsNullOrEmpty(requestid);
        }

        public bool IsRequestSelectedByUser(int requestid)
        {
            if (requestid > 0) return true;
            return false;
        }

        public SelectList GenerateRequestFilterSelectList()
        {
            SelectList selectSearhFilter = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "First Name", Value = "fn"},
                    new SelectListItem { Selected = false, Text = "Last Name", Value = "ln"},
                    new SelectListItem { Selected = false, Text = "Customer ID", Value = "ci"},
                    new SelectListItem { Selected = false, Text = "Request ID", Value = "oid"}
                }, "Value", "Text");

            selectSearhFilter.Where(x => x.Value == "ln").FirstOrDefault().Selected = true;
            return selectSearhFilter;
        }

        public bool IsSearchRequestSet()
        {
            if (_sessionHelper.GetSearchOrderSession() == "true")
            {
                return true;
            }
            return false;
        }

        public string UploadSubsidyImageAndSaveToDb(FileUploadParameter input_file, int transactionid)
        {
            try
            {
                string result_message = "";
                hlab_water_subsidy_form_images image = new hlab_water_subsidy_form_images();
                image.trans_id = transactionid;
                image.scan_image_filename = _utility.GetFileNameFromFormFile(input_file.file);
                
                if (!_utility.IsFileOnRequiredFormat(input_file.file, ".pdf"))
                {
                    result_message = "Error:" + image.scan_image_filename + " scanned file should be in .pdf format only!";
                }
                else
                {
                    input_file.save_path = input_file.save_path;
                    _utility.UploadFile(input_file);
                    _hlabTestTransRepo.AddSubsidyFormImage(image);
                }
                return result_message;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > UploadSibsidyImageAndSaveToDb(): {exc.Message} {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public hlab_order_items ConvertRequestItemObjectToDbObject(orderdetailsview request_item_object)
        {
            try
            {
                if (request_item_object != null)
                {
                    hlab_order_items request_item_db_object = new hlab_order_items()
                    {
                        id = request_item_object.order_item_id,
                        test_pkg_id = request_item_object.pkg_class_id,
                        order_id = request_item_object.order_id,
                        amount = request_item_object.amount,
                        trans_id = 0
                    };
                    return request_item_db_object;
                }
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > ConvertTransactionObjectToDbObject(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public int? InsertCsvTestRequestToDatabase(WaterBacteriaObject wb_csv)
        {
            int? request_id = 0;
            hlab_order_logs orderlog = new hlab_order_logs();
            WaterBacteriaCsvFile current_csv_row = wb_csv.current_csv_row;
            WaterBacteriaCsvFile previous_csv_row = wb_csv.previous_csv_row;
            try
            {
                if (string.IsNullOrEmpty(current_csv_row.CustomerID))
                {
                    try
                    {
                        orderlog.customer_id = Convert.ToInt32(previous_csv_row.CustomerID);
                    }
                    catch (Exception exc)
                    {
                        orderlog.customer_id = 0;
                        proceed_csv_process = false;                        
                        InsertResult = $"{current_csv_row.InsertResult}. Unable to convert customerid: {previous_csv_row.CustomerID} [Customer ID set to 0].";
                    }
                }
                else
                {
                    orderlog.customer_id = Convert.ToInt32(current_csv_row.CustomerID);
                }
                orderlog.order_date = DateTime.Now;
                orderlog.received_by = _sessionHelper.GetSessionUserName(); //receiver of the lab request is the user who uploaded the CSV file
                orderlog.record_status = true;
                orderlog.tax = Convert.ToInt32(current_csv_row.Tax);
                //set to true, so by default users can't modify the order request in the platform. 
                //Have to request to admin to set the request to false, if they wish to modify a request.
                orderlog.proc_status = true;

                try
                {
                    orderlog.total_amount = Convert.ToDecimal(current_csv_row.TotalRequestAmount);
                }
                catch (Exception exc)
                {
                    orderlog.total_amount = 0;
                    proceed_csv_process = false;
                    InsertResult = $"{current_csv_row.InsertResult}. Unable to convert total order amount: {current_csv_row.TotalRequestAmount} [Total Amount set to 0].";
                }

                //if previous csv row is not empty and customer id , last name, first name are blank - the lab pakcage is a second request from the previous customer
                if (
                            previous_csv_row != null
                            && current_csv_row.CustomerID == previous_csv_row.CustomerID
                            && current_csv_row.FirstName == previous_csv_row.FirstName
                            && current_csv_row.LastName == previous_csv_row.LastName
                          )
                {
                    orderlog.customer_id = Convert.ToInt32(previous_csv_row.CustomerID);
                }

                if (proceed_csv_process)
                {
                    request_id = _hlabOrderRepo.AddNewOrder(orderlog);
                    InsertResult = $"{current_csv_row.InsertResult}. Insert Lab Request Successful.";
                }
                else
                {
                    InsertResult = $"{current_csv_row.InsertResult} Inserting Lab Request cancelled.";
                }
                this.proceed_csv_process = wb_csv.proceed_csv_process;
                return request_id;
            }
            catch (Exception exc)
            {
                this.proceed_csv_process = false;
                _logger.LogError($"HRequest > InsertCsvTestRequestToDatabase(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsRequestInFinalStatus(ordersummaryview request)
        {
            if (request.proc_status == true) return true;
            return false;
        }

        public ordersummaryview GetRequestInformationDetails(int requestid)
        {
            try
            {
                ordersummaryview request = new ordersummaryview();
                if(requestid!=0) request = _hlabOrderRepo.GetOrderInfo(requestid);
                return request;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > GetRequestInformationDetails(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public hlab_order_logs ConvertRequestDbObject(ordersummaryview request)
        {
            try
            {
                hlab_order_logs request_db_obj = new hlab_order_logs();
                request_db_obj.order_id = request.order_id;
                request_db_obj.customer_id = request.customer_id;
                request_db_obj.order_date = request.order_date;
                request_db_obj.received_by = request.received_by;
                request_db_obj.proc_status = request.proc_status;
                request_db_obj.total_amount = request.total_amount;
                return request_db_obj;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > ConvertRequestDbObject(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<ordersummaryview> ListAllRequests(ordersearch order_search)
        {
            try
            {
                List<ordersummaryview> order_list = new List<ordersummaryview>();
                order_list = _hlabOrderRepo.GetAllOrders(order_search).ToList();
                return order_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > ListAllRequests(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool DeleteRequest(int requestid)
        {
            try
            {
                return _hlabOrderRepo.DeleteOrder(requestid);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > DeleteRequest(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateRequestChanges(hlab_order_logs request)
        {
            try
            {
                return _hlabOrderRepo.UpdateOrder(request);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > UpdateRequestChanges(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public OrderPageForm PrepareOrderListPageData(OrderListPageParameter parameter, OrderPageForm order)
        {
            try
            {
                orderdetailsview request_item = new orderdetailsview();
                order.SelectedTransId = 0;
                order.SelectedUID = "";
                order.order_search.searchvalue = _sessionHelper.GetRequestSearchValueSession();
                order.order_search.searchfilter = _sessionHelper.GetRequestSearchFilterSession();
                order.selectRush = _utility.GenerateYesNoSelectList();
                order.selectConditionsMet = _utility.GenerateYesNoSelectList();
                order.selectConditionsMet = _utility.SetSelectedItemFromList(order.selectConditionsMet, "true");

                if (parameter.request_id != 0)
                {
                    order.request_view = GetRequestInformationDetails(parameter.request_id);
                    order.request_item_list = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = parameter.request_id });
                    order.selectRush = _utility.SetSelectedItemFromList(order.selectRush, order.request_view.is_rush.ToString());
                    order.selectConditionsMet = _utility.SetSelectedItemFromList(order.selectConditionsMet, order.request_view.is_condition_met.ToString());
                }

                if (parameter.package_id != 0)
                {
                    order.FilteredTestPackageSupplyList = _supplyHelper.GetFilteredTestPackageSupplies(new testpackagesupplyview { pkg_id = parameter.package_id });
                    order.SelectedPackageId = parameter.package_id;
                }

                if (parameter.transaction_id != 0)
                {
                    request_item = order.request_item_list.Where(x => x.trans_id == parameter.transaction_id).FirstOrDefault();
                    order.TransactionSupplyIdList = _hlabTestTransRepo.GetTransactionSuppliesIds(new hlab_transaction_supplies { transaction_id = parameter.transaction_id }).ToList(); ;
                    order.SelectedTransId = (int)request_item.trans_id;
                    order.SelectedUID = request_item.hl_code;
                }

                if(order.request_item_list!=null && order.request_item_list.Count > 0)
                {
                    order.request_item_list = order.request_item_list.OrderBy(x => x.hl_code).ToList();
                }

                return order;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > PrepareOrderListPage(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}

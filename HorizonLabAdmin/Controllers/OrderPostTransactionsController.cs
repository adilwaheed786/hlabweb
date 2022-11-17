using System;
using System.Collections.Generic;
using System.Linq;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class OrderPostTransactionsController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private hlab_test_payments _payment_param = new hlab_test_payments();
        private orderpaymentsview _orderpayment_param = new orderpaymentsview();
        private hlab_customers _customer = new hlab_customers();
        private readonly ILogger<OrderPostTransactionsController> _logger;        
        private readonly IConfiguration _appConfig;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IDashboard _dashboardHelper;
        private readonly IHostingEnvironment _env;
        private readonly IUtility _utilityHelper;
        private readonly ITestPackage _testPackageHelper;
        private readonly ICustomer _customerHelper;
        private readonly IRequest _requestHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly IPayment _paymentHelper;
        private readonly ITransaction _transactionHelper;

        public OrderPostTransactionsController(
            IConfiguration appConfig,
            ILogger<OrderPostTransactionsController> logger,
            IHostingEnvironment env,
            IHorizonLabSession sessionHelper,
            IDashboard dashboardHelper,
            ITestPackage testPackageHelper,
            IUtility utilityHelper,
            ICustomer customerHelper,
            IRequest requestHelper,
            IPayment paymentHelper,
            IRequestItem requestItemHelper,
            ITransaction transactionHelper
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _sessionHelper = sessionHelper;
            _dashboardHelper = dashboardHelper;
            _testPackageHelper = testPackageHelper;
            _utilityHelper = utilityHelper;
            _customerHelper = customerHelper;
            _requestHelper = requestHelper;
            _paymentHelper = paymentHelper;
            _requestItemHelper = requestItemHelper;
            _transactionHelper = transactionHelper;
        }

        [HttpPost]
        public IActionResult SearchOrder(OrderPageForm ordersearch)
        {
            try
            {                
                if (_sessionHelper.IsUserNotLoggedIn()) GoToMainPage();
                _sessionHelper.SetSearchOrder("true");

                if (!string.IsNullOrEmpty(ordersearch.order_search.searchvalue))
                {
                    _sessionHelper.SetTestRequestPageFilter(ordersearch.order_search);
                    _sessionHelper.SetSearchValue(ordersearch.order_search.searchvalue);
                    _sessionHelper.SetSearchFilter(ordersearch.order_search.searchfilter);                    
                }
                else
                {
                    _sessionHelper.SetSearchValue("");
                    _sessionHelper.SetSearchFilter("");

                    _sessionHelper.SetSearchRequestId(0);

                    _sessionHelper.SetSearchCustomerSessionInfo(new hlab_customers
                    {
                        customer_id = 0,
                        first_name = "",
                        last_name = ""
                    });
                }

                DateTime? filter_start_date = ordersearch.order_search.start_order_date;
                DateTime? filter_end_date = ordersearch.order_search.end_order_date;
                if (!_utilityHelper.IsDateTimeNullOrEmpty(filter_start_date))
                {
                    _sessionHelper.SetSearchRequestStartDate(filter_start_date);                    
                }

                if (!_utilityHelper.IsDateTimeNullOrEmpty(filter_end_date))
                {
                    _sessionHelper.SetSearchRequestEndDate(filter_end_date);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return RedirectToAction("OrderListPage", "Order");
        }      

        [HttpPost]
        public IActionResult PlaceOrder(OrderPageForm orderdetails)
        {
            int? orderid = 0;
            TempData["Receipt"] = null;
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                List<hlab_test_pkgs> allPkglist = new List<hlab_test_pkgs>();
                hlab_test_pkgs pkg = new hlab_test_pkgs();
                allPkglist = _testPackageHelper.GetAllTestPackageList();

                orderdetails.hlab_order_log.received_by = HttpContext.Session.GetString("UserName").ToString();
                orderdetails.hlab_order_log.order_date = DateTime.Now;
                orderdetails.hlab_order_log.is_rush = false;
                orderdetails.hlab_order_log.is_condition_met = true;
                //orderdetails.hlab_order_log.proc_status = false;
                if (_customerHelper.IsCustomerNotEmptyAndHasRequests(orderdetails))
                {
                    orderid = _requestHelper.AddNewTestRequest(new JsonTestRequestParametercs {
                        hlab_order_logs = orderdetails.hlab_order_log,
                        hlab_order_items = orderdetails.hlab_order_items,
                        hlab_test_payments = orderdetails.hlab_test_payments
                    });

                    TempData["OrderMessage"] = "success:New Order was submitted successfully.";
                    if (orderdetails.hlab_order_log.proc_status) TempData["Receipt"] = "window.open('/Order/OrderReceipt?orderid=" + orderid + "','_blank')";
                }  
                else
                {
                    TempData["OrderMessage"] = "error:Adding new order failed. It's either customer was not set or no test package was selected.";
                }
            }
            catch (Exception exc)
            {
                TempData["OrderMessage"] = "error:Exception errors were encountered, Check the log file and contact administrator.";
                _logger.LogError(exc.Message);
            }

            if (orderdetails.save_proceed)
            {
                return RedirectToAction("ViewOrderPage", "Order", new { orderid= orderid });
            }
            else
            {
                return RedirectToAction("OrderPage", "Order");
            }
            
        }

        [HttpPost]
        public IActionResult SaveOrderChanges(OrderPageForm orderdetails)
        {
            List<hlab_test_pkgs> allPkglist = new List<hlab_test_pkgs>();
            hlab_test_pkgs pkg = new hlab_test_pkgs();
            orderdetailsview orderview = new orderdetailsview();
            allPkglist = _testPackageHelper.GetAllTestPackageList();

            try
            {
                TempData["OrderListPageMessage"] = null;
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                bool IsUpdateRequestSuccessful = _requestHelper.UpdateRequestChanges(orderdetails.hlab_order_log);
                if (IsUpdateRequestSuccessful)
                {
                    TempData["OrderListPageMessage"] = _requestItemHelper.UpdateCustomerRequestItems(orderdetails);
                    _paymentHelper.UpdateCustomerRequestPayment(orderdetails);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                TempData["OrderListPageMessage"] = "error:Unable to save changes on Order. Exception errors were encountered, Please contact administrator.";
            }
            return RedirectToAction("OrderListPage", "Order");
        }

        [HttpPost]
        public IActionResult SaveRequestFormDetails(OrderPageForm orderdetails, List<int> supply_ids)
        {
            int request_id = orderdetails.hlab_order_log.order_id;
            ordersummaryview view_hlab_order_logs = new ordersummaryview();
            DateTime? incubation_date_time_in;

            if (request_id == null || request_id == 0) return GoToRequestRecordsPage(new OrderListPageParameter{
                request_id = request_id,
                transaction_id = 0,
                package_id = 0
            });
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

            view_hlab_order_logs = _requestHelper.GetRequestInformationDetails(request_id);
            orderdetails.hlab_order_log.order_id = view_hlab_order_logs.order_id;
            orderdetails.hlab_order_log.customer_id = view_hlab_order_logs.customer_id;
            orderdetails.hlab_order_log.order_date = view_hlab_order_logs.order_date;
            orderdetails.hlab_order_log.received_by = view_hlab_order_logs.received_by;
            orderdetails.hlab_order_log.proc_status = view_hlab_order_logs.proc_status;
            orderdetails.hlab_order_log.total_amount = view_hlab_order_logs.total_amount;
            orderdetails.hlab_order_log.tax = view_hlab_order_logs.tax;
            orderdetails.hlab_order_log.record_status = view_hlab_order_logs.record_status;

            incubation_date_time_in = orderdetails.hlab_order_log.incubation_date_time_in;
            if (incubation_date_time_in != null)
                orderdetails.hlab_order_log.incubation_date_time_out = orderdetails.hlab_order_log.incubation_date_time_in.Value.AddHours(24);

            _requestHelper.UpdateRequestChanges(orderdetails.hlab_order_log);
            _transactionHelper.UpdateTransactionSupplies(supply_ids, orderdetails.SelectedTransId);

            return GoToRequestRecordsPage(new OrderListPageParameter{
                request_id = request_id,
                transaction_id = orderdetails.SelectedTransId,
                package_id = orderdetails.SelectedPackageId
            });
        }

        [HttpPost]
        public IActionResult SaveRequestIncubationDetails(OrderPageForm orderdetails)
        {
            int request_id = orderdetails.hlab_order_log.order_id;
            ordersummaryview view_hlab_order_logs = new ordersummaryview();
            DateTime? incubation_date_time_out;
            if (request_id == null || request_id == 0) return GoToRequestRecordsPage(new OrderListPageParameter{
                request_id = request_id,
                transaction_id = 0,
                package_id = 0
            });

            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

            view_hlab_order_logs = _requestHelper.GetRequestInformationDetails(request_id);
            orderdetails.hlab_order_log.order_id = view_hlab_order_logs.order_id;
            orderdetails.hlab_order_log.customer_id = view_hlab_order_logs.customer_id;
            orderdetails.hlab_order_log.order_date = view_hlab_order_logs.order_date;
            orderdetails.hlab_order_log.received_by = view_hlab_order_logs.received_by;
            orderdetails.hlab_order_log.proc_status = view_hlab_order_logs.proc_status;
            orderdetails.hlab_order_log.total_amount = view_hlab_order_logs.total_amount;
            orderdetails.hlab_order_log.tax = view_hlab_order_logs.tax;
            orderdetails.hlab_order_log.record_status = view_hlab_order_logs.record_status;

            incubation_date_time_out = orderdetails.hlab_order_log.incubation_date_time_in;
            if (incubation_date_time_out != null)
                orderdetails.hlab_order_log.incubation_date_time_out = orderdetails.hlab_order_log.incubation_date_time_in.Value.AddHours(24);

            _requestHelper.UpdateRequestChanges(orderdetails.hlab_order_log);

            return GoToRequestRecordsPage(new OrderListPageParameter {
                request_id = request_id,
                transaction_id = orderdetails.SelectedTransId,
                package_id = orderdetails.SelectedPackageId
            });
        }

        [HttpPost]
        public IActionResult CloseOrders(OrderPageForm ordersearch)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page

                foreach(var order in ordersearch.hlab_order_list)
                {
                    if (order.proc_status) _requestHelper.UpdateRequestChanges(order);

                }

                TempData["OrderListPageMessage"] = "success:Order were closed successfully!";
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                TempData["OrderListPageMessage"] = "error:Unable to close order(s). Exception errors were encountered, Please contact administrator.";
            }
            return RedirectToAction("OrderListPage", "Order");
        }

        public IActionResult DeleteOrder(int orderid = 0)
        {
            string Message = "";
            TempData["OrderListPageMessage"] = null;
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");

                Message = "error:Unable to remove order number " + string.Format("{0:00000000}", orderid.ToString()) + ", please contact administrator!";
                if (orderid != 0)
                {
                    
                    if (_requestHelper.DeleteRequest(orderid))
                    {
                        Message = "success:Removing order number " + string.Format("{0:00000000}", orderid.ToString()) + " was successful";
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (!string.IsNullOrEmpty(Message))
            {
                TempData["OrderListPageMessage"] = Message;

            }
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;

            return RedirectToAction("OrderListPage", "Order");
        }

    }
}
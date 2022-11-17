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
    public class OrderController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private hlab_test_payments _payment_param = new hlab_test_payments();
        private orderpaymentsview _orderpayment_param = new orderpaymentsview();
        private hlab_customers _customer = new hlab_customers();
        private readonly ILogger<OrderController> _logger;        
        private readonly IConfiguration _appConfig;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IDashboard _dashboardHelper;
        private readonly IHostingEnvironment _env;
        private readonly ITestPackage _testPackageHelper;
        private readonly IUtility _utilityHelper;
        private readonly IRequest _requestHelper;
        private readonly ICustomer _customerHelper;
        private readonly IPayment _paymentHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly ISupply _supplyHelper;

        public OrderController(
            IConfiguration appConfig,
            ILogger<OrderController> logger,
            IHostingEnvironment env,
            IHorizonLabSession sessionHelper,
            IDashboard dashboardHelper,
            ITestPackage testPackageHelper,
            IUtility utilityHelper,
            IRequest requestHelper,
            ICustomer customerHelper,
            IRequestItem requestItemHelper,
            IPayment paymentHelper,
            ISupply supplyHelper)
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
            _supplyHelper = supplyHelper;
        }

        public IActionResult OrderPage()
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            OrderPageForm orderpageform = new OrderPageForm();
            horizonlabcustomerview selectedCustomer = new horizonlabcustomerview();
            string Message = "", Receipt = "";       
            int selectedCustomerId = 0;
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) GoToMainPage();
                
                List<hlab_test_pkgs_class> classlist = new List<hlab_test_pkgs_class>();
                List<hlab_test_pkgs> testpkglist = new List<hlab_test_pkgs>();

                orderpageform.selectCategoryList = _utilityHelper.GenerateSelectListItem(_testPackageHelper.GetActiveTestPackageCategories(), "category_id", "package_ctgry");
                orderpageform.selectPkgList = new SelectList(testpkglist, "id", "lab_code").ToList();
                orderpageform.selectPaymentTypeList = _utilityHelper.GenerateSelectListItem(_paymentHelper.ListAllDbPaymentTypes(),"id", "payment");
                orderpageform.selectPaymentTypeList = _testPackageHelper.SetDebitPaymentItem(orderpageform.selectPaymentTypeList);

                selectedCustomerId = _sessionHelper.GetSearchCustomerId();
                if (selectedCustomerId != 0)
                {
                    selectedCustomer = _customerHelper.GetDbCustomerInformation(selectedCustomerId);
                    orderpageform.selectCustomerList =  new List<SelectListItem>
                    {
                        new SelectListItem { Selected = true, Text = $"{selectedCustomer.first_name} {selectedCustomer.last_name}", Value = selectedCustomerId.ToString()}
                    };
                }

                ViewBag.defaultPkgClassId = classlist[0].class_id;
                ViewBag.defaultPkgId = testpkglist[0].id;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");

                if (TempData["Receipt"] != null)
                {
                    Receipt = TempData["Receipt"].ToString();
                    TempData["Receipt"] = null;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (TempData["OrderMessage"] != null)
            {
                Message = TempData["OrderMessage"].ToString();
                TempData["OrderMessage"] = null;
            }

            ViewBag.menu = _hlabMenu;
            ViewBag.Receipt = Receipt;
            ViewBag.Message = Message;
            return View(orderpageform);
        }

        public IActionResult EditOrderPage(int orderid)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            OrderPageForm orderpageform = new OrderPageForm();
            orderpageform.hlab_order_log = new hlab_order_logs();
            List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
            List<hlab_test_payment_types> paymenttypes = new List<hlab_test_payment_types>();
            List<hlab_test_pkgs_class> class_list = new List<hlab_test_pkgs_class>();
            List<hlab_test_pkgs> test_pkg_list = new List<hlab_test_pkgs>();
            ordersummaryview request = new ordersummaryview();           
            orderdetailsview orderitem_param = new orderdetailsview();           
            string Message = "";

            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                if (orderid != 0)
                {
                    request = _requestHelper.GetRequestInformationDetails(orderid);
                    if(request == null) GoToRequestRecordsPage(null);
                    if (_requestHelper.IsRequestInFinalStatus(request)) return GoToRequestRecordsPage(null);

                    orderpageform.request_payment_list = _paymentHelper.ListRequestPayments(orderid);
                    customer_list = _customerHelper.GetCustomersForSelectList(request);
                    orderpageform.request_item_list = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = orderid});
                }
                class_list = _testPackageHelper.GetNonHiddenTestClasses();
                test_pkg_list = _testPackageHelper.GetAllWaterBacteriaTestPackageList();
                orderpageform.selectClassList = _utilityHelper.GenerateSelectListItem(class_list, "class_id", "pkg_class");
                orderpageform.selectClassList = _utilityHelper.SetSelectedItemFromList(orderpageform.selectClassList, "1");
                orderpageform.selectPkgList = _utilityHelper.GenerateSelectListItem(test_pkg_list, "id", "lab_code");
                orderpageform.selectCustomerList = _utilityHelper.GenerateSelectListItem(customer_list, "customer_id", "first_name");
                orderpageform.selectPaymentTypeList = _utilityHelper.GenerateSelectListItem(_paymentHelper.ListAllDbPaymentTypes(), "id", "payment");

                ViewBag.defaultPkgClassId = class_list[0].class_id;
                ViewBag.defaultPkgId = test_pkg_list[0].id;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            if (TempData["OrderMessage"] != null) Message = TempData["OrderMessage"].ToString();
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            return View(orderpageform);
        }

        public IActionResult OrderListPage(int req_id=0, int trans_id=0, int pkgid=0)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            List<ordersummaryview> orderlist = new List<ordersummaryview>();            
            OrderPageForm order = new OrderPageForm();
            order.order_search = new ordersearch();
            order.order_search.start_order_date = null;
            order.order_search.end_order_date = null;
            SelectList selectSearhFilter = _requestHelper.GenerateRequestFilterSelectList();
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");

                if (req_id != 0) {
                    _sessionHelper.SetSearchOrder("true");
                    _sessionHelper.SetSearchValue(req_id.ToString());
                    _sessionHelper.SetSearchFilter("oid");
                    _sessionHelper.SetSearchRequestId(req_id);
                }

                if(_requestHelper.IsSearchRequestSet())
                {
                    order = _requestHelper.PrepareOrderListPageData(new OrderListPageParameter {
                        request_id = req_id,
                        package_id = pkgid,
                        transaction_id = trans_id},
                        order
                        );                    

                    if (!string.IsNullOrEmpty(order.order_search.searchfilter))
                        selectSearhFilter.Where(x => x.Value.ToLower() == order.order_search.searchfilter.ToLower()).FirstOrDefault().Selected = true;
                    if(!string.IsNullOrEmpty(_sessionHelper.GetSearchCustomerFirstName()))
                        order.order_search.first_name = _sessionHelper.GetSearchCustomerFirstName();
                    if(!string.IsNullOrEmpty(_sessionHelper.GetSearchCustomerLastName()))
                        order.order_search.last_name = _sessionHelper.GetSearchCustomerLastName();
                    if (!string.IsNullOrEmpty(_sessionHelper.GetSearchRequestStartDate().ToString()))
                        order.order_search.start_order_date = _sessionHelper.GetSearchRequestStartDate();
                    if (!string.IsNullOrEmpty(_sessionHelper.GetSearchRequestEndDate().ToString()))
                        order.order_search.end_order_date = _sessionHelper.GetSearchRequestEndDate();

                    if (order.order_search.start_order_date != null && order.order_search.start_order_date == DateTime.MinValue)
                        order.order_search.start_order_date = null;
                    if (order.order_search.end_order_date != null && order.order_search.end_order_date == DateTime.MinValue)
                        order.order_search.end_order_date = null;

                    order.order_search.order_id = _sessionHelper.GetSearchRequestId();
                    order.order_search.customer_id = _sessionHelper.GetSearchCustomerId();
                    order.SelectedRequestId = req_id;
                    orderlist = _requestHelper.ListAllRequests(order.order_search);
                }
            }
            catch (Exception exc)
            {
                TempData["OrderListPageMessage"] = "error:" + exc.Message;
                _logger.LogError(exc.Message);
            }

            ViewBag.orderlist = orderlist;
            ViewBag.selectSearhFilter = selectSearhFilter;            
            ViewBag.menu = _hlabMenu;
            if (TempData["OrderListPageMessage"] != null)
            {
                ViewBag.Message = TempData["OrderListPageMessage"].ToString();
                TempData["OrderListPageMessage"] = null;
            }
            return View(order);
        }

        public IActionResult OrderReceipt(int orderid=0)
        {
            ordersummaryview orderview = new ordersummaryview();
            List<orderdetailsview> orderitemlist = new List<orderdetailsview>();
            List<orderpaymentsview> payments = new List<orderpaymentsview>();
            orderdetailsview orderitemview = new orderdetailsview();
            OrderPageForm order = new OrderPageForm();
            order.order_search = new ordersearch();
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                orderview = _requestHelper.GetRequestInformationDetails(orderid);
                orderitemlist = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = orderid});
                payments = _paymentHelper.ListRequestPayments(orderid);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            ViewBag.orderview = orderview;
            ViewBag.orderitemlist = orderitemlist;
            ViewBag.payments = payments;
            return View();
        }

        public IActionResult OrderDetailsPage(int orderid)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            ViewBag.menu = _hlabMenu;
            return View();
        }

        [HttpGet]
        public IActionResult ViewOrderPage(int orderid=0)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            orderdetailsview orderitem = new orderdetailsview();            
            List<orderdetailsview> itemlist = new List<orderdetailsview>();
            
            if (orderid != 0) itemlist = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = orderid });
            ViewBag.orderitems = itemlist;
            ViewBag.orderid = orderid;
            ViewBag.menu = _hlabMenu;
            ViewData["PageTitle"] = "View Request";
            return View();
        }

        ////////////////////////////////////////ADDED 12/10/2021///////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult TestRequestPage(int customerid = 0, string fname = "", string lname = "", int requestid = 0, int transactionid = 0)
        {
            DashboardDataView view_data = new DashboardDataView
            {
                search_customer_id = customerid,
                search_customer_firstname = fname,
                search_customer_lastname = lname,
                search_request_id = requestid,
                search_transaction_id = transactionid
            };
            
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
                view_data = _dashboardHelper.GenerateDashboardViewDataObject(view_data);
                _dashboardHelper.SetSearchDashboardValuesToSession(view_data);

                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
                ViewBag.menu = _hlabMenu;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return View(view_data);
        }
    }
}
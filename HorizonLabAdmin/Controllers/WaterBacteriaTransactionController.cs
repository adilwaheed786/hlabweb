using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models;
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
using Newtonsoft.Json;

namespace HorizonLabAdmin.Controllers
{
    //Controller for Handling Water Bacteria Tests
    public class WaterBacteriaTransactionController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu(); private hlab_test_payments _payment_param = new hlab_test_payments();
        private orderpaymentsview _orderpayment_param = new orderpaymentsview(); private hlab_customers _customer = new hlab_customers();
        private sp_gethorizonlabtransactioninvoices _invoice = new sp_gethorizonlabtransactioninvoices();

        private readonly IHorizonLabSession _sessionHelper;private readonly IDashboard _dashboardHelper;private readonly INavigation _navigationHelper;
        private readonly IRequest _requestHelper; private readonly IRequestItem _requestItemHelper;private readonly ITransaction _transactionHelper;
        private readonly ICustomer _customerHelper; private readonly ITestResult _testResultHelper;private readonly IPayment _paymentHelper;
        private readonly ITestPackage _testPackageHelper;private readonly IUtility _utilityHelper;private readonly IWaterBacteria _waterBacteriaHelper;
        private readonly IHostingEnvironment _env; private readonly ILogger<WaterBacteriaTransactionController> _logger;
        private readonly IConfiguration _appConfig; private readonly Iinvoice _invoiceHelper; 
        private readonly int _batchRecord = 100;private readonly int _tax = 0;
        private readonly int WaterBacteriaClassId = 1;
        
        public WaterBacteriaTransactionController(
            IConfiguration appConfig, IHostingEnvironment env, ILogger<WaterBacteriaTransactionController> logger,INavigation navigationHelper,
            IRequest requestHelper,ITestResult testResultHelper,ITransaction transactionHelper,ICustomer customerHelper,
            IHorizonLabSession sessionHelper,IDashboard dashboardHelper,IPayment paymentHelper,IUtility utilityHelper, 
            ITestPackage testPackageHelper,IWaterBacteria waterBacteriaHelper,IRequestItem requestItemHelper,Iinvoice invoiceHelper
            )
        {
            _env = env;_appConfig = appConfig;
            _logger = logger;_customerHelper = customerHelper;_sessionHelper = sessionHelper;
            _testResultHelper = testResultHelper;_navigationHelper = navigationHelper;_requestHelper = requestHelper;
            _transactionHelper = transactionHelper;_dashboardHelper = dashboardHelper;_paymentHelper = paymentHelper;
            _utilityHelper = utilityHelper;_testPackageHelper = testPackageHelper;_waterBacteriaHelper = waterBacteriaHelper;
            _requestItemHelper = requestItemHelper;_invoiceHelper = invoiceHelper;
            _tax = Convert.ToInt32(_appConfig["AppSettings:Gst_Tax"]);
        }
        
        public IActionResult BulkUploadWaterBacteria(bool result = false)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            WaterBacteriaBulkUploadObject wbf = new WaterBacteriaBulkUploadObject();
            wbf.wbf_list = new List<WaterBacteriaCsvFile>();
            ViewBag.menu = _hlabMenu;
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.Message = "";
            if (TempData["CsvParseMessage"] != null)
            {
                ViewBag.Message = TempData["CsvParseMessage"].ToString();
                TempData["CsvParseMessage"] = null;
            }
            
            if(result && TempData["WaterBacteriaBulkUploadObject"] != null)
            {
                wbf = JsonConvert.DeserializeObject<WaterBacteriaBulkUploadObject>(TempData["WaterBacteriaBulkUploadObject"].ToString());
                TempData["WaterBacteriaBulkUploadObject"] = null;
            }

            return View(wbf);
        }

        [HttpGet]
        //get all test transactions (water bacteria, water chemical)
        public IActionResult ViewTransactions(int pkg_class_id = 0, string filter= "unpr")
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");
            List<hlab_test_pkgs> pkgCategoryList = new List<hlab_test_pkgs>();
            test_transaction parameter = new test_transaction();
            TestTransactionSearchParameters search_param = new TestTransactionSearchParameters();
            _sessionHelper.SetIntSearchPackakgeClassId(WaterBacteriaClassId);
            string TestTransactionMessage = "";
            try
            {                
                if(pkg_class_id != 0) _sessionHelper.SetIntSearchPackakgeClassId(pkg_class_id);
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                int rec_start = 0, rec_end = 0, rec_count = 0;

                pkgCategoryList = _testPackageHelper.GetWaterBacteriaTestPackageForSelectList();
                search_param.selectPackageList = _utilityHelper.GenerateSelectListItem(pkgCategoryList, "id", "lab_code");

                search_param.sortByList = _utilityHelper.GenerateSortByListSelectList();
                search_param.sortOptionList = _utilityHelper.GenerateSortByOptionSelectList();
                
                if (_sessionHelper.IsTestTransactionDisplayOptionHasValue())
                {
                    //pkg_class_id = _sessionHelper.GetSearchPackakgeClassId();
                    parameter = _waterBacteriaHelper.PopulateTestTransactionParameterObjFromSession();
                    parameter.filter = filter;
                    search_param.transList = _transactionHelper.GetAllTestTransactionsView(parameter);
                    rec_count = search_param.transList.Count;
                    rec_end = rec_count > _batchRecord ? _batchRecord : rec_count;

                    if (_sessionHelper.IsStartRecordHasValue()) rec_start = _sessionHelper.GetSessionRecordStart();

                    if (_sessionHelper.IsEndRecordHasValue()) rec_end = _sessionHelper.GetSessionRecordEnd();

                    if (rec_end == 0 || rec_end < _batchRecord) rec_end = rec_count > _batchRecord ? _batchRecord : rec_count;

                    search_param.transList = search_param.transList.GetRange(rec_start, rec_end - rec_start);

                    if (rec_end > rec_count)
                    {
                        search_param.transList = search_param.transList.GetRange(rec_start, rec_count - rec_start);
                        rec_end = rec_count;
                    }                 
                    
                    if (parameter.test_pkg_id != 0) search_param.selectPackageList.Where(x => x.Value == parameter.test_pkg_id.ToString()).First().Selected = true;

                    search_param = _navigationHelper.SetSortByFields(search_param);
                    search_param.transList = _navigationHelper.SortTransactionList(search_param);
                }

                search_param.filter = filter;
                ViewBag.rec_start = rec_start;
                ViewBag.rec_end = rec_end;
                ViewBag.rec_count = rec_count;
                ViewBag.filter = filter;
                ViewBag.PgkClass = "Water Bacteria Records";
                ViewBag.PkgClassId = pkg_class_id;
                if (pkg_class_id == 4) ViewBag.PgkClass = "Water Chemical Records";
                
                search_param.searchTransId = parameter.trans_id;
                search_param.customerId = parameter.customer_id;
                search_param = _navigationHelper.SetSearchDateFieldValues(search_param, parameter);
                search_param.searchCustomerFirstName = parameter.first_name;
                search_param.searchCustomerLastName = parameter.last_name;


                search_param.searchPackage = parameter.test_pkg_id;
            }
            catch (Exception exc)
            {
                TestTransactionMessage = exc.Message;
                _logger.LogError(exc.Message);
            }
            ViewBag.TestTransactionMessage = TestTransactionMessage;
            ViewBag.menu = _hlabMenu;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            if (TempData["TestTransactionMessage"] != null) ViewBag.PageMessage = TempData["TestTransactionMessage"].ToString();
            return View(search_param);
        }

        [HttpGet]
        public IActionResult TestTransactionPage (string transId)
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            orderdetailsview orderitemview = new orderdetailsview();
            int int_transid = 0;
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");

                if (!string.IsNullOrEmpty(transId))
                {
                    int_transid = Convert.ToInt32(transId);
                    page_model.trans_details = _transactionHelper.GetTransactionInformationFromSp(int_transid);
                    page_model.result_list = _testResultHelper.GetTestResultFromDb(int_transid);
                    page_model.hlab_invoice_list = _invoiceHelper.GetInvoiceFromDb(int_transid);
                    orderitemview = _requestItemHelper.ListTestRequestItems(new orderdetailsview { trans_id = int_transid, order_id = 0 }).FirstOrDefault();                    
                    page_model.customer_order_payment_list = _paymentHelper.ListRequestPayments(orderitemview.order_id);
                }

                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.menu = _hlabMenu;
                ViewBag.orderitemview = orderitemview;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpGet]
        public IActionResult EditTestTransactionFramePage(string transId = "0")
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();            
            string Message = "";
            
            try
            {
                page_model = _waterBacteriaHelper.PopulateWaterBacteriaEditPageSelectListFields(transId);
                page_model = _waterBacteriaHelper.PopulateWaterBacteriaEditPageListObjects(page_model, transId);

                if (TempData["TestTransactionMessage"] != null)
                {
                    Message = TempData["TestTransactionMessage"].ToString();
                    TempData["TestTransactionMessage"] = null;
                }
                ViewBag.PageMessage = Message;
                ViewBag.default_pkg_params = _testPackageHelper.GetDefaultTestParametersFromDb(page_model.trans_details.test_pkg_id ?? 0);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpGet]
        public IActionResult EditTestTransactionPage(string transId="0")
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            string Message = "", selectedTown = "";
            bool selectCoupon = false;
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");

                page_model = _waterBacteriaHelper.PopulateWaterBacteriaEditPageSelectListFields(transId);
                page_model = _waterBacteriaHelper.PopulateWaterBacteriaEditPageListObjects(page_model, transId);

                selectedTown = page_model.trans_details.town;
                if (!string.IsNullOrEmpty(selectedTown)) page_model.selectTown = _utilityHelper.SetSelectedItemFromList(page_model.selectTown, selectedTown);

                ViewBag.Message = Message;
                ViewBag.default_pkg_params = _testPackageHelper.GetDefaultTestParametersFromDb(page_model.trans_details.test_pkg_id ?? 0);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (TempData["EditTransactionMessage"] != null) Message = TempData["EditTransactionMessage"].ToString();
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.user_access_id = _sessionHelper.GetUserObjectFromSession().access_id;
            return View(page_model);
        }

        [HttpGet]
        public IActionResult NewWaterSampleForm(int oid = 0, int oiid = 0, string frame="yes")
        {
            //start declarations
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            page_model.orderitemview = new orderdetailsview();
            page_model.hlab_invoice_list = new List<sp_gethorizonlabtransactioninvoices>();
            
            //orderdetailsview orderviewparam = new orderdetailsview();
            //orderdetailsview odv = new orderdetailsview();
            orderdetailsview orderItem = new orderdetailsview();
            orderItem = _requestItemHelper.GetRequestItemInfo(oid, oiid);
            //end declarations

            try
            {
                //check #1
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();                
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");

                //check #2
                if (orderItem.trans_id > 0) return GoToEditTestTransactionFrame((int)orderItem.trans_id);

                page_model.isPaid = _utilityHelper.GenerateTrueFalseSelectList();
                page_model.isFloodSample = _utilityHelper.GenerateTrueFalseSelectList();
                page_model.isFloodSample = _utilityHelper.SetSelectedItemFromList(page_model.isFloodSample, "false");
                page_model.selectExistence = _utilityHelper.GenerateTrueFalseSelectList();
                page_model.selected_request_id = oid;
                page_model.selected_request_item_id = oiid;
                page_model.hlab_invoice_list.Add(_paymentHelper.CreateBlankInvoiceItem());

                if (page_model.selected_request_id == 0 && page_model.selected_request_item_id == 0) return GoToRequestRecordsPage(null);
                page_model = _waterBacteriaHelper.PopulateNewWaterBacteriaPageObject(page_model);

                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.menu = _hlabMenu;
                ViewBag.frame = frame;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }
        
        ////////////////////////////////////////ADDED 12/03/2021///////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult EditTestTransactionFrame(int transId = 0, int refresh = 0)
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            orderdetailsview orderitemview = new orderdetailsview();
            int customerid = 0;
            string Message = "", selectedTown = "";            
            try
            {
                
                page_model.customer_order_payment_list = new List<orderpaymentsview>();
                page_model.selectCustomerCoupon = new List<SelectListItem>();               

                if (!_transactionHelper.IsTransactionIDNotEmpty(transId)) return View(page_model);

                page_model = _transactionHelper.InititalizeSelectListForWaterBacteria(page_model);

                testresultsview result_param = new testresultsview();
                page_model.trans_details = _transactionHelper.GetTransactionInformationFromSp(transId);

                selectedTown = page_model.trans_details.town;
                if (!string.IsNullOrEmpty(selectedTown)) page_model.selectTown = _utilityHelper.SetSelectedItemFromList(page_model.selectTown, selectedTown);

                customerid = (int)page_model.trans_details.customer_id;
                if (customerid != 0)
                {
                    page_model.customer_primary_email = _customerHelper.GetCustomerPrimaryEmail(customerid);
                    page_model.customer_primary_phone = _customerHelper.GetCustomerPrimaryPhone(customerid);
                }

                page_model.result_list = _testResultHelper.GetTestResultFromDb(transId);
                page_model.hlab_invoice_list = _invoiceHelper.GetInvoiceFromDb(transId);
                page_model = _transactionHelper.SetSelectedItemsFromWaterBacteriaSelectItemList(page_model);
                orderitemview = _requestHelper.GetRequestInfoOfaTransaction(transId);

                if (orderitemview != null) page_model.customer_order_payment_list = _paymentHelper.ListRequestPayments(orderitemview.order_id);

                if (orderitemview!=null && orderitemview.customer_id != 0) page_model = _customerHelper.GetCustomerCouponRecords(page_model, orderitemview.customer_id);

                if (TempData["TestTransactionMessage"] != null)
                {
                    Message = TempData["TestTransactionMessage"].ToString();
                    TempData["TestTransactionMessage"] = null;
                }

                page_model.refresh = refresh;
                ViewBag.default_pkg_params = _testPackageHelper.GetDefaultTestParametersFromDb(page_model.trans_details.test_pkg_id ?? 0);
                ViewBag.orderitemview = orderitemview;
                ViewBag.user_access_id = _sessionHelper.GetUserObjectFromSession().access_id;
                ViewBag.PageMessage = Message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpGet]
        public IActionResult NoTestRecordFramePage(int url_requestid = 0, int url_requestitemid = 0)
        {
            string hl_code = _requestItemHelper.GetHLCode(url_requestid, url_requestitemid);
            ViewBag.request_id = url_requestid;
            ViewBag.request_item_id = url_requestitemid;
            ViewBag.hl_code = hl_code;
            return View();
        }
    }
}
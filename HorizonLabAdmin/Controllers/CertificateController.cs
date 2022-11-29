using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Helpers.Containers;
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
using System.Net.Mail;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;
//using iTextSharp.text.html.simpleparser;
using SelectPdf;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;


namespace HorizonLabAdmin.Controllers
{
    public class CertificateController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();                
        private hlab_customers _customer = new hlab_customers();
        private readonly int _recordPerBatch = 100;
        public IHttpContextAccessor _httpContextAccessor { get; set; }

        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly ICertificate _certificateHelper;
        private readonly INavigation _navigationHelper;
        private readonly IRequest _requestHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly ITransaction _transactionHelper;
        private readonly IEmail _emailHelper;
        private readonly ICustomer _customerHelper;
        private readonly ITestResult _testResultHelper;
        private readonly ITestProject _testProjectHelper;
        private readonly ITestPackage _testPackagetHelper;
        private readonly IConfiguration _appConfig;
        private readonly ITestProjectForm _testProjForm;
        private readonly IHostingEnvironment _hosting_environment;
        private readonly ILogger<CertificateController> _logger;
        private readonly IWaterBacteria _waterBacteriaHelper;
        private readonly Interface_hlab_test_coupon_logs _hlabTestCouponLogs;
        private readonly ISelectHtmlToPDFConverter _HtmlToPDF;

        public CertificateController(
            IConfiguration appConfig,
            IHostingEnvironment hosting_environment,
            ICertificate certificateHelper,
            INavigation navigationHelper,
            IRequest requestHelper,
            ITestResult testResultHelper,
            ITransaction transactionHelper,
            IEmail emailHelper,
            ICustomer customerHelper,
            ITestPackage testPackagetHelper,
            IHorizonLabSession sessionHelper,
            IRequestItem requestItemHelper,
            ILogger<CertificateController> logger,
            Interface_hlab_test_coupon_logs hlabTestCouponLogs,
            ITestProject testProjectHelper,
            IUtility utility,
            ITestProjectForm testProjForm,
            IWaterBacteria waterBacteriaHelper,
            IHttpContextAccessor httpContextAccessor,
             ISelectHtmlToPDFConverter HtmlToPDF
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _appConfig = appConfig;
            _hosting_environment = hosting_environment;
            _logger = logger;
            _customerHelper = customerHelper;
            _sessionHelper = sessionHelper;
            _testResultHelper = testResultHelper;
            _certificateHelper = certificateHelper;
            _navigationHelper = navigationHelper;
            _requestHelper = requestHelper;
            _transactionHelper = transactionHelper;
            _emailHelper = emailHelper;
            _testPackagetHelper = testPackagetHelper;
            _requestItemHelper = requestItemHelper;
            _hlabTestCouponLogs = hlabTestCouponLogs;
            _testProjectHelper = testProjectHelper;
            _utility = utility;
            _testProjForm = testProjForm;
            _waterBacteriaHelper = waterBacteriaHelper;
            _HtmlToPDF = HtmlToPDF;
        }

        [HttpPost]
        public IActionResult NavigateTransactionRecords(int record_start, int record_end, string filter = "unsent")
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return RedirectToAction("Index", "Login");//back to login page
                _sessionHelper.SetStartCertificateRecordSession(record_start);
                _sessionHelper.SetEndCertificateRecordSession(record_end);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return RedirectToAction("WaterTestCertificatePage", "Certificate", new { filter = filter});
        }

        [HttpPost]
        public IActionResult SearchRequests(TestTransactionSearchParameters param)
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                _sessionHelper.SetSearchCustomerSessionInfo(new hlab_customers
                {
                    customer_id = param.customerId,
                    first_name = param.searchCustomerFirstName,
                    last_name = param.searchCustomerLastName
                });

                _sessionHelper.SetTestRequestSessionInfo(new RequestSessionParameter {
                    request_id = param.searchRequestId,
                    request_item_id = 0,
                    request_date_start = param.searchRequestDateStart,
                    request_date_end = param.searchRequestDateEnd,
                    project_id=param.selectprojectid
                });
                
                _transactionHelper.SetSearchTransactionParameters(new TestTransactionSearchParameters {
                    searchRcvdDateStart = param.searchRcvdDateStart,
                    searchRcvdDateEnd = param.searchRcvdDateEnd,
                    searchSubmtDateStart = param.searchSubmtDateStart,
                    searchSubmtDateEnd = param.searchSubmtDateEnd,
                    searchTestDateStart = param.searchTestDateStart,
                    searchTestDateEnd = param.searchTestDateEnd,
                    customerId = param.customerId,
                    searchCustomerFirstName = param.searchCustomerFirstName,
                    searchCustomerLastName = param.searchCustomerLastName,
                    searchProjectDateStart=param.searchProjectDateStart,
                    searchProjectDateEnd=param.searchProjectDateEnd,
                    selectprojectid=param.selectprojectid
                });

                _sessionHelper.SetTestTransactionDisplayOptionSession(param.display);
                _sessionHelper.SetSortByString(param.sortByString);
                _sessionHelper.SetSortByOption(param.sortByOption);

                //set navigation start-end to 0
                _sessionHelper.SetStartCertificateRecordSession(0);
                _sessionHelper.SetEndCertificateRecordSession(0);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return GoToWaterTestCertificatePage(param.filter);
        }

        public IActionResult WaterTestCertificatePage(string filter="unsent")
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "reports");
            test_transaction parameter = new test_transaction();
            TestTransactionSearchParameters search_param = new TestTransactionSearchParameters();
            string TestTransactionMessage = "";

            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                SelectList selectEmailTemplate = _emailHelper.GenerateEmailTemplateSelectListItems();
                parameter = _waterBacteriaHelper.PopulateTestTransactionParameterObjFromSession();
                search_param = _certificateHelper.PrepareWaterTestCertificatePageData(filter);
                search_param = _navigationHelper.SetSearchDateFieldValues(search_param, parameter);
                

                ViewBag.selectEmailTemplate = selectEmailTemplate;
            }
            catch (Exception exc)
            {
                TestTransactionMessage = exc.Message;
                _logger.LogError(exc.Message);
            }
            search_param.filter = filter;
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.TestTransactionMessage = TestTransactionMessage;
            ViewBag.menu = _hlabMenu;           
            if (TempData["TestTransactionMessage"] != null) ViewBag.PageMessage = TempData["TestTransactionMessage"].ToString();
            return View(search_param);
        }

        //public IActionResult CouponCertificate(int coupon, string SignatureImage = "", string UserFirstName = "", string UserLastName = "")
        //{
        //    sp_getcustomercouponrecords record = new sp_getcustomercouponrecords();
        //    couponrecord param = new couponrecord();
        //    param.coupon = coupon;
        //    param.customerid = 0;

        //    record = _hlabTestCouponLogs.GetCustomerCouponRecord(param).FirstOrDefault();
        //    ViewBag.SignatureImage = _sessionHelper.GetSignatureImgFromSessionWhenEmpty(SignatureImage);
        //    ViewBag.UserFirstName = _sessionHelper.GetUserFirstNameFromSessionWhenEmpty(UserFirstName);
        //    ViewBag.UserLastName = _sessionHelper.GetUserLastNameFromSessionWhenEmpty(UserLastName);
        //    ViewBag.UserRole = _sessionHelper.GetUserUserRoleSessionWhenEmpty("");
        //    return View(record);
        //}


        public IActionResult CouponCertificate(string coupons, string SignatureImage = "", string UserFirstName = "", string UserLastName = "")
        {
            CouponCertificate output = new CouponCertificate();
            output.records = new List<sp_getcustomercouponrecords>();
            couponrecord param = new couponrecord();
            string[] arr_coupon_id;

            if (string.IsNullOrEmpty(coupons)) return View(output.records);
            arr_coupon_id = coupons.Split(',');

            for (int x = 0; x < arr_coupon_id.Length; x++)
            {
                if (string.IsNullOrEmpty(arr_coupon_id[x])) continue;
                param.coupon = Convert.ToInt32(arr_coupon_id[x].Trim());
                param.customerid = 0;
                output.records.Add(_hlabTestCouponLogs.GetCustomerCouponRecord(param).FirstOrDefault());
            }

            ViewBag.SignatureImage = _sessionHelper.GetSignatureImgFromSessionWhenEmpty(SignatureImage);
            ViewBag.UserFirstName = _sessionHelper.GetUserFirstNameFromSessionWhenEmpty(UserFirstName);
            ViewBag.UserLastName = _sessionHelper.GetUserLastNameFromSessionWhenEmpty(UserLastName);
            ViewBag.UserRole = _sessionHelper.GetUserUserRoleSessionWhenEmpty("");
            return View(output);
        }

        [HttpGet]
        public IActionResult b1ns(string transId,string SignatureImage = "", string UserFirstName = "", string UserLastName="")
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            orderdetailsview orderitemview = new orderdetailsview();
            orderdetailsview orderitemparam = new orderdetailsview();
            hlab_test_pkgs testpkg = new hlab_test_pkgs();
            List<hlab_customer_phone> phonelist = new List<hlab_customer_phone>();
            try
            {
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");

                if (_transactionHelper.IsTransactionIDNotEmpty(transId))
                {
                    _customer.customer_id = _customerHelper.GetCustomerIdFromPagePostData(page_model);
                    page_model.trans_details = _transactionHelper.GetTransactionInformationFromSp(Convert.ToInt32(transId));                    
                    page_model.result_list = _testResultHelper.GetTestResultFromDb(Convert.ToInt32(transId));
                    page_model.customer_info = _customerHelper.GetDbCustomerInformation(_customer.customer_id);
                    phonelist = _customerHelper.GetCustomerPhoneListFromDb(_customer.customer_id);
                    testpkg = _testPackagetHelper.GetWaterBacteriaTestPackage((int)page_model.trans_details.test_pkg_id);
                }

                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
                ViewBag.menu = _hlabMenu;
                ViewBag.testpkg = testpkg;
                ViewBag.phonelist = phonelist;
                ViewBag.SignatureImage = _sessionHelper.GetSignatureImgFromSessionWhenEmpty(SignatureImage);
                ViewBag.UserFirstName = _sessionHelper.GetUserFirstNameFromSessionWhenEmpty(UserFirstName);
                ViewBag.UserLastName = _sessionHelper.GetUserLastNameFromSessionWhenEmpty(UserLastName);              
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpGet]
        public IActionResult b1ns2(string requestid, int test_pkg_id = 0, string SignatureImage = "", string UserFirstName = "", string UserLastName = "")
        {
            //B1NS2 Certificate can handle 10 samples / 2 results per sample
            //more than that, certificate layout will be mess up.
             MultipleB1CertificateObject view_object = new MultipleB1CertificateObject();
          // List<TestResultPageViewModel> page_model=new List<TestResultPageViewModel>();
            view_object.page_model = new List<TestResultPageViewModel>();


           // TestResultPageViewModel page_model = new TestResultPageViewModel();
            WaterCertificateListWithCustomerId waterCertificateListWithCustomerId = new WaterCertificateListWithCustomerId();
           // page_model.b1ns_details = new List<b1ns_details>();
           // page_model.selected_test_pkg_id = test_pkg_id;
            int int_request_id = 0;

            try
            {
                if (_requestHelper.IsRequestIdNotEmpty(requestid))
                {
                    int_request_id = Convert.ToInt32(requestid);
                    waterCertificateListWithCustomerId = _certificateHelper.GetCustomerCertificateWithId(int_request_id);
                    // List<int> requestids = new List<int>();
                    var count = waterCertificateListWithCustomerId.certificateList.Count;
                    //requestids
                   foreach (var index in waterCertificateListWithCustomerId.certificateList)
                   {
                        TestResultPageViewModel certificate = new TestResultPageViewModel();
                        List<int> request_id_list = new List<int>();
                        certificate.b1ns_details = new List<b1ns_details>();
                        certificate.selected_test_pkg_id = index.test_pkg_id;
                        request_id_list.Add(index.order_id);
                        certificate = _certificateHelper.GenerateB1NSCertificate(certificate, request_id_list);
                        view_object.page_model.Add(certificate);

                    }
                    
                   
                }

                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
                ViewBag.menu = _hlabMenu;
                //ViewBag.testpkg = testpkg;
                //ViewBag.phonelist = phonelist;
                //ViewBag.Message = Message;
                ViewBag.SignatureImage = _sessionHelper.GetSignatureImgFromSessionWhenEmpty(SignatureImage);
                ViewBag.UserFirstName = _sessionHelper.GetUserFirstNameFromSessionWhenEmpty(UserFirstName);
                ViewBag.UserLastName = _sessionHelper.GetUserLastNameFromSessionWhenEmpty(UserLastName);
                ViewBag.UserRole = _sessionHelper.GetUserUserRoleSessionWhenEmpty("");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(view_object);
        }

        [HttpGet]
        public IActionResult PrintToPdf(string requestid, int test_pkg_id = 0, string SignatureImage = "", string UserFirstName = "", string UserLastName = "")
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream();
                int int_request_id = 0;
                string certificatename = "", CertRequestURL = "";
                int_request_id = Convert.ToInt32(requestid);
                certificatename = $"water_sample_certificate_{int_request_id}";
                CertRequestURL = _certificateHelper.GenerateB1NSCertificateRequestURL(Request.Scheme, Request.Host.ToString(), int_request_id, test_pkg_id);
                memory_stream = _HtmlToPDF.ConvertHtmlURLToPDFMemoryStream(CertRequestURL);
                memory_stream.Position = 0;
                return File(memory_stream, "application/pdf", $"{certificatename}.pdf");
            }
            catch (Exception exc)
            {
                _logger.LogError($"HtmlToPDF > ConvertHtmlURLToPDFMemoryStream() : {exc.Message} ");
                throw exc.InnerException;
            }
        }
        [HttpPost]
        public FileResult ExportToPDF(String Html)
        {            
            try
            {
                HtmlToPdf htmltopdf = new HtmlToPdf();
                htmltopdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                htmltopdf.Options.PdfPageSize = PdfPageSize.A5;
                htmltopdf.Options.MarginTop = 25;
                htmltopdf.Options.MarginLeft = 3;
                htmltopdf.Options.MarginRight = 3;
               // htmltopdf.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
               // htmltopdf.Options.AutoFitHeight= HtmlToPdfPageFitMode.ShrinkOnly;
               // htmltopdf.Options.WebPageWidth = 760;
               // htmltopdf.Options.WebPageHeight = 500;
                htmltopdf.Options.WebPageWidth = 760;
                htmltopdf.Options.WebPageHeight = 1050;                

                PdfDocument pdfDocument = htmltopdf.ConvertHtmlString(Html);
                byte[] pdf = pdfDocument.Save();
                //convert to memory stream
                MemoryStream stream = new MemoryStream(pdf);
                pdfDocument.Close();
                return File(stream, "application/pdf", "Certificate.pdf");
            }
            catch (Exception exc)
            {
                _logger.LogError($"HtmlToPDF > ConvertHtmlURLToPDFMemoryStream() : {exc.Message} ");
                throw exc.InnerException;
            }
        }
        public IActionResult MultipleB1nsReport(TestTransactionSearchParameters transactions, List<int> requestids)
        {            
            
            MultipleB1CertificateObject view_object = new MultipleB1CertificateObject();
            view_object.page_model = new List<TestResultPageViewModel>();            
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");

                if (requestids.Count > 0)
                {
                    foreach (var index in requestids)
                    {
                        TestResultPageViewModel certificate = new TestResultPageViewModel();
                        List<int> request_id_list = new List<int>();

                        certificate.b1ns_details = new List<b1ns_details>();
                        certificate.selected_test_pkg_id = transactions.watercertificatelist[index].test_pkg_id;
                        request_id_list.Add(transactions.watercertificatelist[index].order_id);
                        certificate = _certificateHelper.GenerateB1NSCertificate(certificate, request_id_list);
                        view_object.page_model.Add(certificate);
                    }                    
                }

                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
                ViewBag.menu = _hlabMenu;
                ViewBag.SignatureImage = _sessionHelper.GetSignatureImgFromSessionWhenEmpty("");
                ViewBag.UserFirstName = _sessionHelper.GetUserFirstNameFromSessionWhenEmpty("");
                ViewBag.UserLastName = _sessionHelper.GetUserLastNameFromSessionWhenEmpty("");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(view_object);
        }
       
        
        [HttpPost]
        public IActionResult SendEmailDocuments(TestTransactionSearchParameters transactions, List<int> requestids)
        {
            List<orderdetailsview> request_item_list = new List<orderdetailsview>();
            bool email_result = true;
            int selected_test_package_id = 0;
            int selected_email_template_id = 0;
            int requestid = 0;
            try
            {
                transactions.request_host = Request.Host.ToString();
                transactions.request_scheme = Request.Scheme;
                foreach(var index in requestids)
                {
                    requestid = transactions.watercertificatelist[index].order_id;
                    selected_test_package_id = transactions.watercertificatelist[index].test_pkg_id;
                    request_item_list = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = requestid });

                    if (request_item_list == null && request_item_list.Count < 0) break;
                    request_item_list = request_item_list.Where(x => x.pkg_id == selected_test_package_id).ToList();

                    if (request_item_list == null && request_item_list.Count < 0) break;
                    selected_email_template_id = transactions.watercertificatelist[index].email_template_id;
                    foreach (var item in request_item_list)
                    {
                        item.email_template_id = selected_email_template_id;
                    }                    

                    email_result = _emailHelper.ProcessRequestItemsForEmail(transactions, request_item_list);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"CertificateController > SendEmailDocuments():{exc.Message}");
            }

            return GoToWaterTestCertificatePage(transactions.filter);
        }

        public IActionResult WaterBacteriaProjectsForms(ProjectRequestPageObject view_data)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) GoToMainPage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "reports");
            DateTime? DateCreated = null;
            view_data.ProjectRequestFormsSettings = new List<projectrequestsupplyview>();

            view_data = _testProjectHelper.PrepareProjectRequestData(view_data);
            view_data.ProjectSelectList.Add(new SelectListItem {Text = "", Value="0" });
            view_data.ProjectSelectList = _utility.SetSelectedItemFromList(view_data.ProjectSelectList, view_data.selected_project_id.ToString());

            if (!string.IsNullOrEmpty(view_data.DateCreated)) DateCreated = _utility.FormatStringToDateTime(view_data.DateCreated);            

            view_data.ProjectRequestForms = _testProjForm.ListProjectRequestFormInfo(new projectrequestsformview {
                date_created = DateCreated,
                project_id = view_data.selected_project_id                
            });

            if (view_data.selected_project_form_id != 0 && view_data.selected_project_form_id != null)
            {
                view_data.ProjectRequestFormsSettings = _testProjectHelper.GetProjectRequestSuppliesFromDb(view_data.selected_project_form_id);
                view_data.ProjectRequestFormInfo = _testProjForm.GetProjectRequestFormInfo(view_data.selected_project_form_id);

                view_data.RushList.Where(x => x.Value == "False").FirstOrDefault().Selected = false;
                view_data.RushList.Where(x => x.Value == "True").FirstOrDefault().Selected = false;
                view_data.IsConditionMetList.Where(x => x.Value == "False").FirstOrDefault().Selected = false;
                view_data.IsConditionMetList.Where(x => x.Value == "True").FirstOrDefault().Selected = false;

                view_data.RushList = _utility.SetSelectedItemFromList(view_data.RushList, view_data.ProjectRequestFormInfo.is_rush.ToString());
                view_data.IsConditionMetList = _utility.SetSelectedItemFromList(view_data.IsConditionMetList, view_data.ProjectRequestFormInfo.is_condition_met.ToString());
            }
        
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            return View(view_data);
        }

        public IActionResult SemiPublicReport(string start=null, string end=null)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) GoToMainPage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "reports");
            DateTime? rcv_date_start = null, rcv_date_end = null;
            int water_bacteria_class_id = 1;
            bool transaction_status = true;
            TestTransactionSearchParameters view_data = new TestTransactionSearchParameters();
            view_data.semipublic_transaction_list = new List<sp_getsemipublicreport>();

            if(string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.GetFirstDateInWeek(DateTime.Now);
                rcv_date_end = _utility.GetLastDateInWeek(DateTime.Now);
            }
            else if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.FormatStringToDateTime(start);
                rcv_date_end = _utility.FormatStringToDateTime(end);
            }

            if(rcv_date_start!=null && rcv_date_end!=null)
            {
                view_data.semipublic_transaction_list = _transactionHelper.GetSemiPublicTransactionsList(new test_transaction {
                    rcv_date_start = rcv_date_start,
                    rcv_date_end = rcv_date_end,
                    class_id = water_bacteria_class_id,
                    status = transaction_status
                });
            }

            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.StartReceivedDate = rcv_date_start;
            ViewBag.EndReceivedDate = rcv_date_end;
            return View(view_data);
        }

        public IActionResult MonthlySubsidyReport(string start = null, string end = null)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) GoToMainPage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "reports");
            DateTime? rcv_date_start = null, rcv_date_end = null;
            int water_bacteria_class_id = 1;
            bool transaction_status = true;
            TestTransactionSearchParameters view_data = new TestTransactionSearchParameters();
            view_data.monthly_subsidy_transaction_list = new List<sp_getmonthlysubsidyreport>();

            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.GetFirstDateOfMonth(DateTime.Now);
                rcv_date_end = _utility.GetLastDateOfMonth(DateTime.Now);
            }
            else if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                rcv_date_start = _utility.FormatStringToDateTime(start);
                rcv_date_end = _utility.FormatStringToDateTime(end);
            }

            if (rcv_date_start != null && rcv_date_end != null)
            {
                view_data.monthly_subsidy_transaction_list = _transactionHelper.GetMonthlySubsidyTransactionsList(new test_transaction
                {
                    rcv_date_start = rcv_date_start,
                    rcv_date_end = rcv_date_end,
                    class_id = water_bacteria_class_id,
                    status = transaction_status
                });
            }

            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.StartReceivedDate = rcv_date_start;
            ViewBag.EndReceivedDate = rcv_date_end;
            return View(view_data);
        }
    }
}
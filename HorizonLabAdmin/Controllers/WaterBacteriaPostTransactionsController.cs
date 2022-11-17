using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class WaterBacteriaPostTransactionsController : HController
    {
        private enum TestPakcageClass
        {
            WaterBacteria = 1,
            WaterChemistry = 4
        }

        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private hlab_test_payments _payment_param = new hlab_test_payments();
        private orderpaymentsview _orderpayment_param = new orderpaymentsview();
        private hlab_customers _customer = new hlab_customers();
        private sp_gethorizonlabtransactioninvoices _invoice = new sp_gethorizonlabtransactioninvoices();
        private readonly ITestPackage _testpackage;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IDashboard _dashboardHelper;
        private readonly INavigation _navigationHelper;
        private readonly IRequest _requestHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly ITransaction _transactionHelper;
        private readonly ICustomer _customerHelper;
        private readonly ITestResult _testResultHelper;
        private readonly IPayment _paymentHelper;
        private readonly IUtility _utilityHelper;
        private readonly ITestPackage _packageHelper;
        private readonly IWaterBacteria _waterBacteriaHelper;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly int _batchRecord = 100;
        private readonly int _tax = 0;
        private readonly ILogger<WaterBacteriaPostTransactionsController> _logger;


        public WaterBacteriaPostTransactionsController(
            IConfiguration appConfig,
            IHostingEnvironment env,
            ILogger<WaterBacteriaPostTransactionsController> logger,
            INavigation navigationHelper,
            IRequest requestHelper,
            ITestResult testResultHelper,
            ITransaction transactionHelper,
            ICustomer customerHelper,
            IHorizonLabSession sessionHelper,
            IDashboard dashboardHelper,
            IPayment paymentHelper,
            ITestPackage packageHelper,
            IUtility utilityHelper,
            IWaterBacteria waterBacteriaHelper,
            ITestPackage testpackage,
            IRequestItem requestItemHelper
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _tax = Convert.ToInt32(_appConfig["AppSettings:Gst_Tax"]);
            _customerHelper = customerHelper;
            _sessionHelper = sessionHelper;
            _testResultHelper = testResultHelper;
            _navigationHelper = navigationHelper;
            _requestHelper = requestHelper;
            _transactionHelper = transactionHelper;
            _dashboardHelper = dashboardHelper;
            _paymentHelper = paymentHelper;
            _utilityHelper = utilityHelper;
            _packageHelper = packageHelper;
            _waterBacteriaHelper = waterBacteriaHelper;
            _testpackage = testpackage;
            _requestItemHelper = requestItemHelper;
        }

        [HttpPost]
        public IActionResult AddWaterSample(TestResultPageViewModel htt, IFormFile scan_subsidy_image)
        {
            string result_message = "";
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                int transid = 0;
                string savepath = _env.WebRootPath + "\\scan_subsidy_forms";


                hlab_order_items orderitemparam = new hlab_order_items();
                List<sp_getdefaultpackageparameters> default_params = new List<sp_getdefaultpackageparameters>();

                htt.hlab_test_transactions.date_entered = DateTime.Now;
                htt.hlab_test_transactions.assigned_coupon = htt.hlab_order_items.coupon;
                transid = _transactionHelper.InsertNewTestTransactionToDb(htt.hlab_test_transactions);

                if (_testpackage.IsTestPackageSet(htt.hlab_order_items))
                    default_params = _testpackage.GetDefaultTestParametersFromDb((int)htt.hlab_order_items.test_pkg_id);

                htt.hlab_order_items.trans_id = transid;
                if(transid == 0)
                {
                    result_message = "Error:Error on saving transaction. Please contact administrator!";
                    if (!string.IsNullOrEmpty(result_message)) TempData["OrderListPageMessage"] = result_message;
                    return GoToRequestRecordsPage(null);
                }
                
                if (scan_subsidy_image != null)
                {
                    result_message = _requestHelper.UploadSubsidyImageAndSaveToDb(new FileUploadParameter {
                        file = scan_subsidy_image,
                        required_file_format = ".pdf",
                        save_path = savepath
                    }, transid);
                }

                _requestItemHelper.UpdateTestRequestItemDb(htt.hlab_order_items);
                if (default_params.Count > 0) //if test package has default parameters, insert test results
                {
                    foreach(var param in default_params)
                    {
                        _testResultHelper.AddNewTestResultToDb(new hlab_test_results{
                            param_id = param.param_id,
                            result = "",
                            unit_id = 0,
                            is_failed = false,
                            trans_id = transid,
                            test_note = ""
                        });                           
                    }
                }
                if (htt.orderitemview.pkg_class_id == 4) GoToEditWaterChemPage(transid);
                return GoToEditTestTransactionFrame(transid);               
            }
            catch (Exception exc)
            {
                result_message = "Error:" + exc.Message;
                _logger.LogError(exc.Message);
            }

            if (!string.IsNullOrEmpty(result_message)) TempData["OrderListPageMessage"] = result_message;
            return GoToRequestRecordsPage(null);
        }

        public IActionResult DeactivateTestTransactionRecord(int transid)
        {
            string result_message = "";
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                var transaction_sp = _transactionHelper.GetTransactionInformationFromSp(transid);
                var request_view_obj = _requestHelper.GetRequestInfoOfaTransaction(transid);

                hlab_test_transactions transaction_db = _transactionHelper.ConvertTransactionObjToDbObject(transaction_sp, false);
                hlab_order_items request_item_db = _requestHelper.ConvertRequestItemObjectToDbObject(request_view_obj);

                if(transaction_db != null ) _transactionHelper.UpdateTestTransactionDb(transaction_db);
                if(request_item_db != null) _requestItemHelper.UpdateTestRequestItemDb(request_item_db);
                result_message = "Success:Transaction #" + transid + " was succesfuuly removed";
                if (!string.IsNullOrEmpty(result_message)) TempData["TestTransactionMessage"] = result_message;
            }
            catch (Exception exc)
            {
                result_message = "Error:Error on saving Transaction #" + transid + ". Please contact administrator!";
                _logger.LogError(exc.Message);
            }
            return GoToWaterBacteriaTransactionViewPage();
        }

        [HttpPost]
        public IActionResult UpdateTransactionInfo(TestResultPageViewModel inputtrans, IFormFile scan_subsidy_image)
        {
            try
            {
                string result_message = "";               

                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                if (scan_subsidy_image != null)
                {
                    ProcessSubsidyImage(new FileUploadParameter {
                        file = scan_subsidy_image,
                        required_file_format = ".pdf",
                        trans_id = inputtrans.trans_details.trans_id,
                        image_db_id = inputtrans.trans_details.subsidyimage_id
                });
                }

                hlab_test_transactions database_test_transaction = new hlab_test_transactions();
                database_test_transaction = _transactionHelper.ConvertTransactionObjToDbObject(inputtrans.trans_details, true);

                var result = _transactionHelper.UpdateTestTransactionDb(database_test_transaction);
                if (!string.IsNullOrEmpty(result_message)) TempData["TestTransactionMessage"] = result_message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (inputtrans.ispageframe)
            {
                return RedirectToAction(inputtrans.goto_viewpage, "WaterBacteriaTransaction", new { transId = inputtrans.trans_details.trans_id, refresh = inputtrans.refresh });
            }

            return GoToEditWaterBacteriaPage(inputtrans.trans_details.trans_id, inputtrans.refresh);
        }

        private string ProcessSubsidyImage(FileUploadParameter subsidy_img)
        {
            string result_message="";
            string savepath = _env.WebRootPath + "\\scan_subsidy_forms";
            try
            {
                savepath = savepath + "\\" + subsidy_img.file.FileName;
                if (subsidy_img.image_db_id == 0)
                {                    
                    result_message = _requestHelper.UploadSubsidyImageAndSaveToDb(new FileUploadParameter
                    {
                        file = subsidy_img.file,
                        required_file_format = subsidy_img.required_file_format,
                        save_path = savepath
                    }, subsidy_img.trans_id);
                    return result_message;
                }
                else
                {
                    ScannedSubsidyFileParameter file_parameter = new ScannedSubsidyFileParameter
                    {
                        scanned_pdf_file = subsidy_img.file,
                        saved_path = savepath
                    };
                    file_parameter.database_image_file = new hlab_water_subsidy_form_images();
                    file_parameter.database_image_file.trans_id = subsidy_img.trans_id;
                    file_parameter.database_image_file.id = subsidy_img.image_db_id;
                    file_parameter = _transactionHelper.ProcessScannedSubsidyImagePdfFile(file_parameter);
                    return "Subsidy Image Uploaded.";
                }
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

        [HttpPost]
        public IActionResult UpdateTestResult(TestResultPageViewModel transviewmodel)
        {
            try
            {
                string result_message = "";
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                var result = _testResultHelper.UpdateTestResultDb(transviewmodel.hlab_test_result);
                if (result)
                {
                    result_message = "Success:Test Result #" + transviewmodel.hlab_test_result.result_id + " have been updated successfully!";
                }
                else
                {
                    result_message = "Error:Error on saving Test Result #" + _utilityHelper.AddSuffixToID(transviewmodel.hlab_test_result.result_id) + ". Please contact administrator!";
                }

                if (!string.IsNullOrEmpty(result_message)) TempData["TestTransactionMessage"] = result_message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return GoToEditWaterBacteriaPage(transviewmodel.hlab_test_result.trans_id);
        }

        public IActionResult DeleteTestResult(int r, int t)
        {
            try
            {
                string result_message = "";
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                var result = _testResultHelper.DeleteTestResultDb(r);
                if (result)
                {
                    result_message = "Success:Test Result #" + r + " have been updated deleted!";
                }
                else
                {
                    result_message = "Error:Error on saving Test Result #" + r + ". Please contact administrator!";
                }

                if (!string.IsNullOrEmpty(result_message)) TempData["EditTransactionMessage"] = result_message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return GoToEditWaterBacteriaPage(t);
        }

        [HttpPost]
        public IActionResult AddTestResult(TestResultPageViewModel transviewmodel)
        {
            try
            {
                string result_message = "";
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

                hlab_test_results result_table = new hlab_test_results()
                {
                    param_id = transviewmodel.hlab_test_result.param_id,
                    result = transviewmodel.hlab_test_result.result,
                    unit_id = transviewmodel.hlab_test_result.unit_id,
                    test_note = transviewmodel.hlab_test_result.test_note,
                    is_failed = transviewmodel.hlab_test_result.is_failed,
                    trans_id = transviewmodel.hlab_test_result.trans_id
                };

                var result = _testResultHelper.AddNewTestResultToDb(result_table);
                result_message = "Success:Test Result #" + result_table.result_id + " have been updated successfully!";
                if (!result) result_message = "Error:Error on saving Test Result #" + string.Format("{0:00000000}", result_table.result_id) + ". Please contact administrator!";
                if (!string.IsNullOrEmpty(result_message)) TempData["EditTransactionMessage"] = result_message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return GoToEditWaterBacteriaPage(transviewmodel.hlab_test_result.trans_id);
        }

        [HttpPost]
        public IActionResult SaveToAllWaterSample(TestResultPageViewModel htt)
        {
            string result_message = "";
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                int transid = 0;
                List<int> transid_list = new List<int>();

                List<orderdetailsview> orderitems = new List<orderdetailsview>();
                orderdetailsview order_param = new orderdetailsview();
                order_param.order_id = htt.hlab_order_items.order_id ?? 0;
                orderitems = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = order_param.order_id });
                orderitems = _requestItemHelper.FilterListRequestItems(orderitems, (int)htt.hlab_order_items.test_pkg_id);

                foreach (var item in orderitems)
                {
                    htt.hlab_test_transactions.date_entered = DateTime.Now;
                    htt.hlab_test_transactions.test_pkg_id = item.pkg_id;
                    transid = _transactionHelper.InsertNewTestTransactionToDb(htt.hlab_test_transactions);
                    transid_list.Add(transid);
                    htt.hlab_order_items.trans_id = transid;
                    htt.hlab_order_items.id = item.order_item_id;
                    htt.hlab_order_items.test_pkg_id = item.pkg_id;
                    htt.hlab_order_items.amount = item.amount;
                    htt.hlab_order_items.hl_code = item.hl_code;

                    if (transid != 0)
                    {
                        _requestItemHelper.UpdateTestRequestItemDb(htt.hlab_order_items);
                        _packageHelper.InsertDefaultTestParameters((int)htt.hlab_order_items.test_pkg_id, transid);
                    }
                    else
                    {
                        result_message = "Error:Error on saving transaction. Please contact administrator!";
                    }
                }

                if (htt.orderitemview.pkg_class_id == 4) return GoToEditWaterChemPage(transid);
                if(transid_list.Count == 0) GoToNewWaterSampleForm(order_param.order_id, order_param.order_item_id, "yes");
                return GoToEditTestTransactionFrame(transid_list[0]);
            }
            catch (Exception exc)
            {
                result_message = "Error:" + exc.Message;
                _logger.LogError(exc.Message);
            }

            if (!string.IsNullOrEmpty(result_message)) TempData["OrderListPageMessage"] = result_message;
            return GoToRequestRecordsPage(null);
        }

        [HttpPost]
        public IActionResult ProcessWaterBacteriaBulkUploads(WaterBacteriaBulkUploadObject wbf)
        {
            try
            {
                TempData["WaterBacteriaBulkUploadObject"] = null;
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                
                List<horizonlabcustomerview> customerviewlist = new List<horizonlabcustomerview>();                                
                hlab_order_items orderitem = new hlab_order_items();
                WaterBacteriaCsvFile prev_csv_item = new WaterBacteriaCsvFile();
                WaterBacteriaCsvFile csv_item = new WaterBacteriaCsvFile();                
                hlab_test_transactions watersample = new hlab_test_transactions();
                TestPackageObject test_package_object = new TestPackageObject();
                bool IsCsvInsertToDbSuccessful = true;
                foreach (var item in wbf.wbf_list)
                {
                    _waterBacteriaHelper.proceed_csv_process = true;

                    //START CUSTOMER
                    if (!string.IsNullOrEmpty(item.CustomerID))
                    {
                        csv_item = _waterBacteriaHelper.PopulateCsvFileFromExistingCustomerDb(item);
                    }
                    else if (!string.IsNullOrEmpty(item.FirstName) && !string.IsNullOrEmpty(item.LastName))
                    {
                        //insert customer     
                        csv_item = _waterBacteriaHelper.PopulateCsvFileFromNewCustomerDb(item);
                    }
                    else
                    {
                        csv_item = _waterBacteriaHelper.PopulateCsvFileFromExistingCustomerDb(prev_csv_item);
                    }

                    item.CustomerID = csv_item.CustomerID;
                    item.FirstName = csv_item.FirstName;
                    item.LastName = csv_item.LastName;
                    item.InsertResult = csv_item.InsertResult;

                    //initialize water sample 
                    watersample = _waterBacteriaHelper.InitializeTestTransactionDbObject(item);
                    item.InsertResult = _waterBacteriaHelper.InsertResult; 

                    //check if water sample exists in the database
                    if (_waterBacteriaHelper.IsWaterBacteriaCsvNotNullOrEmpty(watersample))
                    {
                        if (_transactionHelper.IsTestTrsansactionExistsInDb(watersample))
                        {
                            _waterBacteriaHelper.proceed_csv_process = false;
                            item.InsertResult = $"{item.InsertResult} Water Sample Information is already existing in the database. Cancel all inserts.";
                        }
                    }

                    if (!_waterBacteriaHelper.IsOkToProcessCsv())
                    {
                        if (!string.IsNullOrEmpty(item.CustomerID)) prev_csv_item = item;
                        break;
                    }

                    if (string.IsNullOrEmpty(item.LabPackage))
                    {
                        if (!string.IsNullOrEmpty(item.CustomerID)) prev_csv_item = item;
                        break;
                    }
                        
                    test_package_object = _testpackage.GetLabPakcageObject(item.LabPackage);
                    if (prev_csv_item == null && string.IsNullOrEmpty(item.CustomerID))
                    {
                        item.InsertResult = item.InsertResult + "Failed:Can't identify the owner of this request.";
                    }
                    else if (!string.IsNullOrEmpty(item.TotalRequestAmount))
                    {
                        IsCsvInsertToDbSuccessful = _waterBacteriaHelper.InsertCsvWaterBacteriaRowToDB(new WaterBacteriaObject {
                            current_csv_row = item,
                            previous_csv_row = prev_csv_item,
                            proceed_csv_process = _waterBacteriaHelper.proceed_csv_process,
                            watersample = watersample,
                            test_package_object = test_package_object
                        });
                        item.InsertResult = _waterBacteriaHelper.InsertResult;                        
                    }
                    if (!IsCsvInsertToDbSuccessful) item.InsertResult = $"{item.InsertResult} !! Inserting CSV row to Database failed.";
                    if (!string.IsNullOrEmpty(item.CustomerID)) prev_csv_item = item;
                }
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");
                ViewBag.menu = _hlabMenu;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                TempData["WaterBacteriaBulkUploadObject"] = Newtonsoft.Json.JsonConvert.SerializeObject(wbf);
            }
            catch (Exception exc)
            {
                TempData["CsvParseMessage"] = "Error: " + exc.Message;
                _logger.LogError(exc.Message);
            }
            return GoToBulkUploadWaterBacteriaPage(true);
            //return View(wbf);
        }

        [HttpPost]
        public IActionResult ParseWaterBacteriaCsv(IFormFile waterbacteriacsv)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");
            string csvfile = "", fileextension = "";
            WaterBacteriaBulkUploadObject water_bacteria_object = new WaterBacteriaBulkUploadObject();
            water_bacteria_object.wbf_list = new List<WaterBacteriaCsvFile>();

            TempData["CsvParseMessage"] = null;
            ViewBag.menu = _hlabMenu;
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();

            if (waterbacteriacsv == null)
            {
                TempData["CsvParseMessage"] = "No CSV file was uploaded";
                return View(water_bacteria_object);
            }

            csvfile = Path.GetFileName(waterbacteriacsv.FileName).ToString();
            fileextension = Path.GetExtension(waterbacteriacsv.FileName).ToString().ToLower();

            //check if csv files is in right format
            if (fileextension.ToLower() != ".csv")
            {
                TempData["CsvParseMessage"] = "Uploaded file is not a CSV file";
                return View(water_bacteria_object);
            }

            water_bacteria_object = _waterBacteriaHelper.ParseWaterBacteriaFile(waterbacteriacsv);
            return View(water_bacteria_object);
        }

        [HttpPost]
        public IActionResult DonwloadBulkUploadResult(WaterBacteriaBulkUploadObject wbf)
        {
            StringBuilder sb = new StringBuilder();
            var filename = string.Format("{0:d}", DateTime.Now.Millisecond);

            sb.Append("InsertResult,");
            sb.Append("LabPackage,");
            sb.Append("CustomerID,");
            sb.Append("FirstName,");
            sb.Append("LastName,");
            sb.Append("Postal},");
            sb.Append("PrimaryPhone,");
            sb.Append("PrimaryEmail,");
            sb.Append("Postal,");
            sb.Append("TotalRequestAmount,");
            sb.Append("Payment1,");
            sb.Append("Amount1,");
            sb.Append("Payment2,");
            sb.Append("Amount2,");
            sb.Append("Payment3,");
            sb.Append("Amount3,");
            sb.Append("Coupon,");
            sb.Append("SampleID,");
            sb.Append("SampleType,");
            sb.Append("LegalLocation,");
            sb.Append("Town},");
            sb.Append("Municipality,");
            sb.Append("CollectDate,");
            sb.Append("CollectTime,");
            sb.Append("SubmittedBy,");
            sb.Append("SubmittedDate,");
            sb.Append("SubmittedTime,");
            sb.Append("ReceivedBy,");
            sb.Append("ReceivedDate,");
            sb.Append("ReceivedTime,");
            sb.Append("TestDate,");
            sb.Append("Temperature,");
            sb.Append("Tax,");
            sb.Append("\r\n");

            foreach (var row in wbf.wbf_list)
            {
                sb.Append($"{row.InsertResult},");
                sb.Append($"{row.LabPackage},");
                sb.Append($"{row.CustomerID},");
                sb.Append($"{row.FirstName},");
                sb.Append($"{row.LastName},");
                sb.Append($"{row.Postal},");
                sb.Append($"{row.PrimaryPhone},");
                sb.Append($"{row.PrimaryEmail},");
                sb.Append($"{row.Postal},");
                sb.Append($"{row.TotalRequestAmount},");
                sb.Append($"{row.Payment1},");
                sb.Append($"{row.Amount1},");
                sb.Append($"{row.Payment2},");
                sb.Append($"{row.Amount2},");
                sb.Append($"{row.Payment3},");
                sb.Append($"{row.Amount3},");
                sb.Append($"{row.Coupon},");
                sb.Append($"{row.SampleID},");
                sb.Append($"{row.SampleType},");
                sb.Append($"{row.LegalLocation},");
                sb.Append($"{row.Town},");
                sb.Append($"{row.Municipality},");
                sb.Append($"{row.CollectDate},");
                sb.Append($"{row.CollectTime},");
                sb.Append($"{row.SubmittedBy},");
                sb.Append($"{row.SubmittedDate},");
                sb.Append($"{row.SubmittedTime},");
                sb.Append($"{row.ReceivedBy},");
                sb.Append($"{row.ReceivedDate},");
                sb.Append($"{row.ReceivedTime},");
                sb.Append($"{row.TestDate},");
                sb.Append($"{row.Temperature},");
                sb.Append($"{row.Tax},");
                sb.Append("\r\n");
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", $"BulkUploadResult_{filename}.csv");
        }

        [HttpPost]
        public IActionResult NavigateTransactionRecords(int record_start, int record_end, int pkg_class_id, string filter = "unpr")
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _sessionHelper.SetStartRecordSession(record_start);
                _sessionHelper.SetEndRecordSession(record_end);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return RedirectToAction("ViewTransactions", "WaterBacteriaTransaction", new { pkg_class_id  = pkg_class_id , filter = filter });
        }

        [HttpPost]
        public IActionResult SearchTransactions(TestTransactionSearchParameters param)
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _transactionHelper.SetSearchTransactionParameters(param);

                //set navigation start-end to 0
                _sessionHelper.SetStartRecordSession(0);
                _sessionHelper.SetEndRecordSession(0);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            return RedirectToAction("ViewTransactions", "WaterBacteriaTransaction", new { pkg_class_id = param.pkg_class_id, filter = param.filter});
        }

        public IActionResult DownloadWaterBacteriaCsvTemplate()
        {
            string path = _env.WebRootPath + "\\downloads\\";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "water_bacteria_bulk_upload_template_v1.csv");
            string fileName = "water_bacteria_bulk_upload_template_v1.csv";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
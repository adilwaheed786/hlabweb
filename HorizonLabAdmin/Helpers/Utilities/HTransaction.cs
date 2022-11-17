using CsvHelper;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HTransaction: ITransaction
    {
        private readonly IUtility _utility;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_test_parameter _HlabTestParameter;
        private readonly Interface_unit_of_measurement _UnitOfMeasure;
        private readonly Interface_test_description _HlabTestDescription;
        private readonly Interface_test_sample_types _HlabTestSample;
        private readonly Interface_rural_municipality _Municipality;
        private readonly Interface_hlab_cities _city;
        private readonly Interface_receivers _Receiver;
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly Interface_test_report_types _HlabTestReport;
        private readonly Interface_test_package _hlabPkgCtgry;
        private readonly Interface_test_projects _hlabTestProject;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<HTransaction> _logger;
        private readonly ITestPackage _testpackage;
        private readonly Interface_test_results _hlabTestResult;
        private readonly int _mb_province_id = 3;
        public string InsertResult { get; set; }
        private readonly int _MiscClassId = 2, _HiddenClassId = 3, _B1SubsidyId = 1, _B1NonSubsidyId = 10;

        public HTransaction(
            IHorizonLabSession sessionHelper,
            IUtility utility,
            Interface_test_parameter HlabTestParameter,
            Interface_unit_of_measurement UnitOfMeasure,
            Interface_test_description HlabTestDescription,
            Interface_test_sample_types HlabTestSample,
            Interface_rural_municipality Municipality,
            Interface_receivers Receiver,
            Interface_test_transactions hlabTestTransRepo,
            Interface_hlab_test_payments hlabPayment,
            Interface_test_report_types HlabTestReport,
            Interface_test_package hlabPkgCtgry,
            Interface_test_projects hlabTestProject,
            IHostingEnvironment environment,
            ITestPackage testpackage,
            Interface_test_results hlabTestResult,
            Interface_hlab_cities city,
            ILogger<HTransaction> logger)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _HlabTestParameter = HlabTestParameter;
            _UnitOfMeasure = UnitOfMeasure;
            _HlabTestDescription = HlabTestDescription;
            _HlabTestSample = HlabTestSample;
            _Municipality = Municipality;
            _Receiver = Receiver;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabPayment = hlabPayment;
            _HlabTestReport = HlabTestReport;
            _hlabPkgCtgry = hlabPkgCtgry;
            _hlabTestProject = hlabTestProject;
            _environment = environment;
            _logger = logger;
            _hlabTestResult = hlabTestResult;
            _testpackage = testpackage;
            _city = city;
        }

        public int GetEmailTemplateIdFromTestTransactionsViewPageData(TestTransactionSearchParameters test_transaction, int trnsaction_id)
        {
            return test_transaction.transList.Where(x => x.trans_id == trnsaction_id).FirstOrDefault().email_template_id;
        }

        public List<orderdetailsview> GetB1RequestItems(List<orderdetailsview> request_item_list)
        {
            List<int> transaid_list = new List<int>();
            try
            {
                request_item_list = request_item_list
                    .Where(y => y.pkg_id == _B1SubsidyId || y.pkg_id == _B1NonSubsidyId) //get b1 subsidy and b1 non subsidy only
                    .Where(x => x.trans_id != 0).ToList();
                return request_item_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetB1RequestItems(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public TestResultPageViewModel InititalizeSelectListForWaterBacteria(TestResultPageViewModel page_model)
        {
            List<hlab_test_params> testparams = new List<hlab_test_params>();
            page_model.isPaid = _utility.GenerateTrueFalseSelectList();
            page_model.isFloodSample = _utility.GenerateTrueFalseSelectList();
            page_model.selectExistence = _utility.GenerateTrueFalseSelectList();

            testparams = _HlabTestParameter.GetAllTestParameters().Where(x => x.pkg_class_id == 1).ToList(); //pkg_class_id=1 fopr Water Bacteria
            page_model.selectTestParameters = _utility.GenerateSelectListItem(testparams, "param_id", "param_name").ToList();
            page_model.selectTranTypeList = _utility.GenerateSelectListItem(_hlabTestTransRepo.GetTestTransactionTypes().ToList(), "id", "transaction_type").ToList();
            page_model.selectTestDescriptions = _utility.GenerateSelectListItem(_HlabTestDescription.GetAllTestDescriptions().ToList(), "description", "description").ToList();
            page_model.selectReceiversList = _utility.GenerateSelectListItem(_Receiver.GetAllReceivers().ToList(), "id", "receiver").ToList();
            page_model.selectPaymentOptionList = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentOptions().ToList(), "id", "payment_option").ToList();
            page_model.selectPaymentTypeList = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentTypes().ToList(), "id", "payment").ToList();
            page_model.selectProjectList = _utility.GenerateSelectListItem(_hlabTestProject.GetAllTestProjects().ToList(), "id", "project").ToList();
            page_model.selectSampleTypeList = _utility.GenerateSelectListItem(_HlabTestSample.GetAllTestSampleTypes().ToList(), "id", "sample_type").ToList();
            page_model.selectTestPackageList = _utility.GenerateSelectListItem(_hlabPkgCtgry.GetTestPackages(0).ToList(), "id", "lab_code").ToList();//0 = All Test Package
            page_model.selectUnitMeasurements = _utility.GenerateSelectListItem(_UnitOfMeasure.GetAllUnitMeasurements().ToList(), "id", "unit_of_measurement").ToList();
            page_model.selectReportTypeList = _utility.GenerateSelectListItem(_HlabTestReport.GetAllReportTypes().ToList(), "id", "report_type").ToList();
            page_model.selectMunicipalityList = _utility.GenerateSelectListItem(_Municipality.GetRuralMunicipalities().ToList(), "id", "rural_municipality").ToList();
            page_model.selectMunicipalityList = page_model.selectMunicipalityList.OrderBy(x => x.Text).ToList();

            page_model.selectTestDescriptions.Add(new SelectListItem { Selected = false, Text = "", Value = "" });
            page_model.selectReceiversList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            page_model.selectProjectList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            page_model.selectSampleTypeList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            page_model.selectTestPackageList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            page_model.selectReportTypeList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            page_model.selectMunicipalityList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            page_model.selectTestParameters.Add(new SelectListItem { Selected = true, Text = "SELECT PARAMETER", Value = "0" });
            page_model.selectUnitMeasurements.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

            page_model.selectTown = _utility.GenerateSelectListItem(_city.GetAllCities(_mb_province_id).ToList(), "city", "city");
            page_model.selectTown.Add(new SelectListItem { Selected = true, Text = "", Value = "" });
            page_model.selectTown = page_model.selectTown.OrderBy(x => x.Text).ToList();
            
            return page_model;
        }

        public transaction_email InitTransactionParameter(sp_gethorizonlabtransactiondetails transactiondetails, hlab_email_templates email_template)
        {
            transaction_email transactionparam = new transaction_email();
            transactionparam.trans_id = transactiondetails.trans_id;
            transactionparam.customer_id = transactiondetails.customer_id ?? 0;
            transactionparam.email = transactiondetails.email;
            transactionparam.subject = email_template.template_subject;
            transactionparam.body = email_template.template_message;
            transactionparam.email_template_id = email_template.id;
            return transactionparam;
        }

        public bool IsTransactionHasNoGeneratedCoupon(sp_gethorizonlabtransactiondetails transaction)
        {
            if(transaction.gen_coupon.GetValueOrDefault(0) == 0)
            {
                return true;
            }
            return false;
        }

        public bool IsTransactionHasPayments(List<sp_gethorizonlabtransactioninvoices> payment_list)
        {
            if(payment_list.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsTransactionIDNotEmpty(string transid)
        {
            return !string.IsNullOrEmpty(transid);
        }

        public bool IsTransactionIDNotEmpty(int transid)
        {
            if(transid>0)
            {
                return true;
            }
            return false;
        }

        public bool IsTransactionPaymentOptionSet(List<sp_gethorizonlabtransactioninvoices> payment_list)
        {
            if (payment_list[0].payment_option_id!=null)
            {
                return true;
            }
            return false;
        }

        public bool IsTransactionPaymentTypeSet(List<sp_gethorizonlabtransactioninvoices> payment_list)
        {
            if (payment_list[0].payment_type_id != null)
            {
                return true;
            }
            return false;
        }

        public void SetSearchTransactionParameters(TestTransactionSearchParameters search_parameter)
        {
            SetTransactionInfoSearchParameter(search_parameter);
            SetDateTransactionSearchParameter(search_parameter);
            SetSortTransactionSearchParameter(search_parameter);
        }

        private void SetSortTransactionSearchParameter(TestTransactionSearchParameters search_parameter)
        {
            _sessionHelper.SetTestTransactionDisplayOptionSession(search_parameter.display);
            _sessionHelper.SetSortByOption(search_parameter.sortByOption);
            _sessionHelper.SetSortByString(search_parameter.sortByString);            
        }

        private void SetDateTransactionSearchParameter(TestTransactionSearchParameters search_parameter)
        {
            _sessionHelper.SetSubmittedStartDate(search_parameter.searchSubmtDateStart);
            _sessionHelper.SetSubmittedEndDate(search_parameter.searchSubmtDateEnd);
            _sessionHelper.SetReceivedStartDate(search_parameter.searchRcvdDateStart);
            _sessionHelper.SetReceivedEndDate(search_parameter.searchRcvdDateEnd);
            _sessionHelper.SetTestStartDate(search_parameter.searchTestDateStart);
            _sessionHelper.SetTestdEndDate(search_parameter.searchTestDateEnd);
            _sessionHelper.SetProjectStartDate(search_parameter.searchProjectDateStart);
            _sessionHelper.SetProjectEndDate(search_parameter.searchProjectDateEnd);
        
        
        }

        private void SetTransactionInfoSearchParameter(TestTransactionSearchParameters search_parameter)
        {
            _sessionHelper.SetIntSearchWaterBacteriaTransactionId(search_parameter.searchTransId);
            _sessionHelper.SetSearchCustomerSessionInfo(new hlab_customers {
                customer_id = search_parameter.customerId,
                first_name = search_parameter.searchCustomerFirstName ,
                last_name = search_parameter.searchCustomerLastName });
            _sessionHelper.SetIntSearchPackakgeClassId(search_parameter.searchPackageClassId);
            _sessionHelper.SetIntSearchPackakgeId(search_parameter.searchPackage);
            _sessionHelper.SetIntProjectId(search_parameter.selectprojectid);
        }

        public TestResultPageViewModel SetSelectedItemsFromWaterBacteriaSelectItemList(TestResultPageViewModel page_model)
        {
            page_model.isPaid = _utility.SetSelectedItemFromList(
                page_model.isPaid,
                page_model.trans_details.is_paid.ToString().ToLower());

            page_model.isFloodSample = _utility.SetSelectedItemFromList(
                page_model.isFloodSample,
                page_model.trans_details.is_flood_sample.ToString().ToLower());

            page_model.selectExistence = _utility.SetSelectedItemFromList(
                page_model.isFloodSample,
                page_model.trans_details.existence.ToString().ToLower());


            if (page_model.trans_details.project_id != null)
            {
                page_model.selectProjectList = _utility.SetSelectedItemFromList(
                page_model.selectProjectList,
                page_model.trans_details.project_id.ToString().ToLower());
            }

            if (page_model.trans_details.sample_type_id != null)
            {
                page_model.selectSampleTypeList = _utility.SetSelectedItemFromList(
                page_model.selectSampleTypeList,
                page_model.trans_details.sample_type_id.ToString().ToLower());
            }


            if (page_model.trans_details.test_pkg_id != null)
            {
                page_model.selectTestPackageList = _utility.SetSelectedItemFromList(
                page_model.selectTestPackageList,
                page_model.trans_details.test_pkg_id.ToString().ToLower());
            }

            if (page_model.trans_details.report_type_id != null)
            {
                page_model.selectReportTypeList = _utility.SetSelectedItemFromList(
                page_model.selectReportTypeList,
                page_model.trans_details.report_type_id.ToString().ToLower());
            }

            if (page_model.trans_details.rm_id != null)
            {
                page_model.selectMunicipalityList = _utility.SetSelectedItemFromList(
                page_model.selectMunicipalityList,
                page_model.trans_details.rm_id.ToString().ToLower());
            }

            if (page_model.trans_details.rcv_by_id != null) page_model.selectReceiversList.Where(x => x.Value == page_model.trans_details.rcv_by_id.ToString()).FirstOrDefault().Selected = true;

            if (IsTransactionHasPayments(page_model.hlab_invoice_list))
            {
                if (IsTransactionPaymentOptionSet(page_model.hlab_invoice_list))
                {
                    page_model.selectPaymentOptionList = _utility.SetSelectedItemFromList(
                        page_model.selectPaymentOptionList,
                        page_model.hlab_invoice_list[0].payment_option_id.ToString());
                }

                if (IsTransactionPaymentTypeSet(page_model.hlab_invoice_list))
                {
                    page_model.selectPaymentTypeList = _utility.SetSelectedItemFromList(
                        page_model.selectPaymentTypeList,
                        page_model.hlab_invoice_list[0].payment_type_id.ToString());
                }

            }
            return page_model;
        }

        public ScannedSubsidyFileParameter ProcessScannedSubsidyImagePdfFile(ScannedSubsidyFileParameter file_parameter)
        {
            try
            {
                //file_parameter.database_image_file = new hlab_water_subsidy_form_images();
                file_parameter.output_message = "";
                if (!_utility.IsFileOnRequiredFormat(file_parameter.scanned_pdf_file, ".pdf"))
                {
                    file_parameter.output_message = "Error:" + file_parameter.scanned_pdf_file.FileName + " scanned file should be in .pdf format only!";
                    file_parameter.database_image_file = null;
                    return file_parameter;
                }
                _utility.UploadFile(new FileUploadParameter { file = file_parameter.scanned_pdf_file, save_path = file_parameter.saved_path });

                file_parameter.database_image_file.scan_image_filename = file_parameter.scanned_pdf_file.FileName;
                _hlabTestTransRepo.UpdateSubsidyFormImage(file_parameter.database_image_file);
                return file_parameter;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > ProcessScannedSubsidyImagePdfFile(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public hlab_test_transactions ConvertTransactionObjToDbObject(sp_gethorizonlabtransactiondetails transaction, bool record_status)
        {
            try
            {
                if (transaction != null)
                {
                    hlab_test_transactions transaction_db_object = new hlab_test_transactions()
                    {
                        trans_id = transaction.trans_id,
                        test_pkg_id = transaction.test_pkg_id,
                        price = transaction.price,
                        collect_datetime = transaction.collect_datetime,
                        submtd_by = transaction.submtd_by,
                        submtd_datetime = transaction.submtd_datetime,
                        rcv_by_id = transaction.rcv_by_id,
                        rcv_date = transaction.rcv_date,
                        test_date = transaction.test_date,
                        is_flood_sample = transaction.is_flood_sample,
                        notes = transaction.notes,
                        customer_id = transaction.customer_id,
                        assigned_coupon = transaction.assigned_coupon,
                        date_entered = transaction.date_entered,
                        work_date = transaction.work_date,
                        transaction_type_id = transaction.transaction_type_id,
                        report_type_id = transaction.report_type_id,
                        temp = transaction.temp,
                        project_id = transaction.project_id,
                        idnty_name = transaction.idnty_name,
                        idnty_location = transaction.idnty_location,
                        sample_type_id = transaction.sample_type_id,
                        sample_legal_loc = transaction.sample_legal_loc,
                        town = transaction.town,
                        rm_id = transaction.rm_id,
                        latitude = transaction.latitude,
                        longitude = transaction.longitude,
                        utm_x = transaction.utm_x,
                        utm_y = transaction.utm_y,
                        zone = transaction.zone,
                        is_paid = transaction.is_paid,
                        status = record_status,
                        publish = transaction.publish,
                        is_condition_met = transaction.is_condition_met,
                        condition_comment = transaction.condition_comment
                    };
                    return transaction_db_object;
                }
                return null;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > TransferInfotoDatabaseTestTransaction(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public int InsertCsvWaterBacteriaTestTransactionToDatabase(WaterBacteriaObject water_bacteria_object)
        {
            try
            {
                int transid = 0;
                List<sp_getdefaultpackageparameters> default_params = new List<sp_getdefaultpackageparameters>();
                TestPackageObject lab_package_obj = new TestPackageObject();
                lab_package_obj = water_bacteria_object.test_package_object;

                if (!IsHLabPackageNotNullHiddenOrMisc(lab_package_obj.lab_package))
                {
                    InsertResult = $"{water_bacteria_object.current_csv_row.InsertResult} Inserting  Water Sample Information cancelled. Because lab package class is either MISC or Hidden.";
                    return 0;
                }

                if (!water_bacteria_object.proceed_csv_process)
                {
                    InsertResult = $"{water_bacteria_object.current_csv_row.InsertResult} Inserting  Water Sample Information cancelled.";
                    return 0;
                }
                transid = _hlabTestTransRepo.AddTransactionDetails(water_bacteria_object.watersample);

                default_params = _hlabTestTransRepo.GetDefaultParameters(lab_package_obj.lab_package_id).ToList();
                if (default_params.Count > 0) //if test package has default parameters, insert test results
                {
                    foreach (var param in default_params)
                    {
                        _hlabTestResult.AddTestResults(new hlab_test_results
                        {
                            param_id = param.param_id,
                            result = "",
                            unit_id = 0,
                            is_failed = false,
                            trans_id = transid,
                            test_note = ""
                        });
                    }
                }
                InsertResult = $"{water_bacteria_object.current_csv_row.InsertResult} Inserting Water Sample Information Successful.";
                return transid;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > InsertCsvWaterBacteriaTestTransactionToDatabase() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        private bool IsHLabPackageNotNullHiddenOrMisc(hlab_test_pkgs test_package)
        {
            if (test_package != null && test_package.pkg_class_id != 2 && test_package.pkg_class_id != 3)  //lab package is not under Hidden(id:3) or MISC(id:2) class
            {
                return true;
            }
            return false;
        }

        public int InsertNewTestTransactionToDb(hlab_test_transactions transaction_obj)
        {
            try
            {
                int new_transaction_id = 0;
                new_transaction_id = _hlabTestTransRepo.AddTransactionDetails(transaction_obj);
                return new_transaction_id;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > InsertNewTestTransactiontoDb() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public sp_gethorizonlabtransactiondetails GetTransactionInformationFromSp(int transactionid)
        {
            try
            {
                sp_gethorizonlabtransactiondetails transaction_info = new sp_gethorizonlabtransactiondetails();
                transaction_info = _hlabTestTransRepo.GetTransactionDetails(transactionid);
                return transaction_info;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetTransactionInformationFromSp() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool UpdateTestTransactionDb(hlab_test_transactions transaction_obj)
        {
            try
            {
                return _hlabTestTransRepo.UpdateTransactionDetails(transaction_obj);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > UpdateTestTransactionDb() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool IsTestTrsansactionExistsInDb(hlab_test_transactions transaction_obj)
        {
            try
            {
                if (_hlabTestTransRepo.IfWaterSampleExists(transaction_obj)) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > IsTestTrsansactionExistsInDb() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<testtransactionsview> GetAllTestTransactionsView(test_transaction transaction_obj)
        {
            try
            {
                List<testtransactionsview> transaction_list = new List<testtransactionsview>();

                if (!IsTransactionHasValue(transaction_obj)) return transaction_list;

                transaction_list  = _hlabTestTransRepo.GetAllTransactions(transaction_obj).ToList();
                return transaction_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > IsTestTrsansactionExistsInDb() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        private bool IsTransactionHasValue(test_transaction transaction_obj)
        {
            if (transaction_obj.submtd_datetime_start == null && transaction_obj.submtd_datetime_start.HasValue
                && transaction_obj.submtd_datetime_end == null && transaction_obj.submtd_datetime_end.HasValue
                && transaction_obj.rcv_date_start == null && transaction_obj.rcv_date_start.HasValue
                && transaction_obj.rcv_date_end == null && transaction_obj.rcv_date_end.HasValue
                && transaction_obj.test_date_start == null && transaction_obj.test_date_start.HasValue
                && transaction_obj.test_date_end == null && transaction_obj.test_date_end.HasValue
                && transaction_obj.trans_id == 0
                && transaction_obj.trans_id == 0
                && transaction_obj.test_pkg_id == 0
                && string.IsNullOrEmpty(transaction_obj.first_name)
                && string.IsNullOrEmpty(transaction_obj.last_name)
                )
            {
                return false;
            }
            return true;
        }

        public List<hlab_test_transaction_types> GetAllTestTransactionsTypes()
        {
            try
            {
                return _hlabTestTransRepo.GetTestTransactionTypes().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetAllTestTransactionsTypes() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<hlab_test_descriptions> GetAllTestDescriptionDb()
        {
            try
            {
                return _HlabTestDescription.GetAllTestDescriptions().ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetAllTestTransactionsTypes() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool UpdateTransactionSupplies(List<int> supply_ids, int transactionid = 0)
        {
            try
            {
                transaction_supply_param param = new transaction_supply_param();
                param.supply_ids = supply_ids;
                param.transactionid = transactionid;

                if (supply_ids.Count == 0) return _hlabTestTransRepo.DeleteTransactionSupplies(transactionid);

                if (_hlabTestTransRepo.DeleteTransactionSupplies(transactionid))
                {
                    _hlabTestTransRepo.AddTransactionSupplies(param);
                    return true;
                }                
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > UpdateTransactionSupplies() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<testrequestsupplyview> GetTestRequestSupplies(int reqid, int formid)
        {
            try
            {
                return _hlabTestTransRepo.GetTestRequestSupplyList(reqid, formid);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetTestRequestSupplies() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<sp_getsemipublicreport> GetSemiPublicTransactionsList(test_transaction transaction_obj)
        {
            try
            {
                List<sp_getsemipublicreport> transaction_list = new List<sp_getsemipublicreport>();
                transaction_list = _hlabTestTransRepo.GetSemiPublicTransactions(transaction_obj).ToList();
                return transaction_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetSemiPublicTransactionsList() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<sp_getmonthlysubsidyreport> GetMonthlySubsidyTransactionsList(test_transaction transaction_obj)
        {
            try
            {
                List<sp_getmonthlysubsidyreport> transaction_list = new List<sp_getmonthlysubsidyreport>();
                transaction_list = _hlabTestTransRepo.GetMonthlySubsidyReport(transaction_obj).ToList();
                return transaction_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTransaction > GetMonthlySubsidyTransactionsList() : {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

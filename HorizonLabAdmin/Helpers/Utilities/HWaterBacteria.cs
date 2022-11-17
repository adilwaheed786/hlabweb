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
    public class HWaterBacteria : IWaterBacteria
    {
        private readonly IUtility _utility; private readonly ICustomer _customer;
        private readonly ITestPackage _testpackage;private readonly IRequest _request;
        private readonly IRequestItem _requestitem;private readonly IPayment _payment;
        private readonly ITransaction _transaction; private readonly int _mb_province_id = 3;

        private readonly IHorizonLabSession _sessionHelper; private readonly Interface_test_parameter _HlabTestParameter;
        private readonly Interface_hlab_customers _hlabCustomers; private readonly Interface_unit_of_measurement _UnitOfMeasure;
        private readonly Interface_test_description _HlabTestDescription; private readonly Interface_test_sample_types _HlabTestSample;
        private readonly Interface_rural_municipality _Municipality; private readonly Interface_receivers _Receiver;
        private readonly Interface_test_transactions _hlabTestTransRepo; private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly Interface_test_report_types _HlabTestReport; private readonly Interface_test_package _hlabPkgCtgry;
        private readonly Interface_test_projects _hlabTestProject; private readonly Interface_test_results _hlabTestResult;
        private readonly Interface_hlab_invoice _hlabInvoice; private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly IHostingEnvironment _environment; private readonly Interface_hlab_test_coupon_logs _hlabCouponLog;
        private readonly ILogger<HWaterBacteria> _logger; private readonly Interface_hlab_cities _city;

        public bool proceed_csv_process {get;set;}
        public string InsertResult {get;set;}
        private enum TestPakcageClass
        {
            WaterBacteria = 1,
            WaterChemistry = 4
        }

        public HWaterBacteria(
            IUtility utility,ICustomer customer,ITestPackage testpackage,IRequestItem requestitem,IRequest request,
            IPayment payment,ITransaction transaction,IHorizonLabSession sessionHelper, Interface_hlab_cities city,
            Interface_test_parameter HlabTestParameter,Interface_unit_of_measurement UnitOfMeasure,
            Interface_test_description HlabTestDescription,Interface_test_sample_types HlabTestSample,Interface_rural_municipality Municipality,
            Interface_receivers Receiver,Interface_test_transactions hlabTestTransRepo,Interface_hlab_test_payments hlabPayment,
            Interface_test_report_types HlabTestReport,Interface_test_package hlabPkgCtgry,Interface_test_projects hlabTestProject,
            IHostingEnvironment environment,Interface_hlab_customers hlabCustomers,Interface_hlab_test_coupon_logs hlabCouponLog,
            Interface_test_results hlabTestResult,Interface_hlab_orders hlabOrderRepo,Interface_hlab_invoice hlabInvoice,ILogger<HWaterBacteria> logger)
        {
            _sessionHelper = sessionHelper;_utility = utility;_HlabTestParameter = HlabTestParameter;
            _UnitOfMeasure = UnitOfMeasure;_HlabTestDescription = HlabTestDescription;_HlabTestSample = HlabTestSample;
            _Municipality = Municipality;_Receiver = Receiver;_hlabTestTransRepo = hlabTestTransRepo;_hlabPayment = hlabPayment;
            _HlabTestReport = HlabTestReport;_hlabPkgCtgry = hlabPkgCtgry;_hlabTestProject = hlabTestProject;_hlabCustomers = hlabCustomers;
            _environment = environment;_logger = logger;_hlabCouponLog = hlabCouponLog;_customer = customer; _city = city;
            _testpackage = testpackage;_request = request;_payment = payment;_transaction = transaction;
            _requestitem = requestitem;_hlabOrderRepo = hlabOrderRepo;_hlabTestResult = hlabTestResult;_hlabInvoice = hlabInvoice;
        }

        public WaterBacteriaBulkUploadObject ParseWaterBacteriaFile(IFormFile waterbacteriacsv)
        {
            try
            {
                string savepath = _environment.WebRootPath + "\\csvfiles";
                savepath = savepath + "\\" + Guid.NewGuid().ToString("n") + ".csv";
                WaterBacteriaBulkUploadObject water_bacteria_object = new WaterBacteriaBulkUploadObject();
                water_bacteria_object.wbf_list = new List<WaterBacteriaCsvFile>();
                int tax = 0;

                using (var stream = new FileStream(savepath, FileMode.Create))
                {
                    waterbacteriacsv.CopyTo(stream);
                }

                using (var reader = new StreamReader(savepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    var records = csv.GetRecords<WaterBacteriaCsvFile>();
                    foreach (var item in records)
                    {
                        _utility.SetDefaultStringTimeIfNullOrEmpty(item.CollectTime);
                        _utility.SetDefaultStringTimeIfNullOrEmpty(item.SubmittedTime);
                        _utility.SetDefaultStringTimeIfNullOrEmpty(item.ReceivedTime);

                        water_bacteria_object.wbf_list.Add(new WaterBacteriaCsvFile
                        {
                            CustomerID = item.CustomerID ?? null,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Postal = item.Postal,
                            PrimaryPhone = item.PrimaryPhone,
                            PrimaryEmail = item.PrimaryEmail,
                            TotalRequestAmount = item.TotalRequestAmount,
                            SampleID = item.SampleID,
                            SampleType = item.SampleType,
                            LegalLocation = item.LegalLocation,
                            Town = item.Town,
                            CollectDate = item.CollectDate ?? null,
                            CollectTime = item.CollectTime ?? null,
                            SubmittedDate = item.SubmittedDate ?? null,
                            SubmittedTime = item.SubmittedTime ?? null,
                            SubmittedBy = item.SubmittedBy ?? null,
                            ReceivedDate = item.ReceivedDate ?? null,
                            ReceivedTime = item.ReceivedTime ?? null,
                            ReceivedBy = item.ReceivedBy,
                            TestDate = item.TestDate ?? null,
                            Temperature = item.Temperature,
                            Project = item.Project,
                            Municipality = item.Municipality,
                            LabPackage = item.LabPackage,
                            Tax = tax.ToString(),
                            Payment1 = item.Payment1,
                            Payment2 = item.Payment2,
                            Payment3 = item.Payment3,
                            Amount1 = item.Amount1,
                            Amount2 = item.Amount2,
                            Amount3 = item.Amount3,
                            Coupon = item.Coupon,
                        });
                    }
                }
                return water_bacteria_object;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > WaterBacteriaBulkUploadObject(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public WaterBacteriaCsvFile PopulateCsvFileFromExistingCustomerDb(WaterBacteriaCsvFile item)
        {
            try
            {
                horizonlabcustomerview customerview = new horizonlabcustomerview();
                customerview = _hlabCustomers.GetCustomersDetails(new hlab_customers { customer_id = Convert.ToInt32(item.CustomerID) });
                if (customerview != null)
                {
                    item.CustomerID = customerview.customer_id.ToString();
                    item.FirstName = customerview.first_name;
                    item.LastName = customerview.last_name;
                }
                else
                {
                    proceed_csv_process = false;
                    item.InsertResult = $"{item.InsertResult} Unable to retrieve records of customerid: {item.CustomerID}. All inserts will be cancelled";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateCsvFileFromCustomerDb(): {exc.InnerException}");
                item.CustomerID = "0";
                item.InsertResult = $"{item.InsertResult} Exception error in processing customerid: {item.CustomerID}. All inserts will be cancelled";
                proceed_csv_process = false;
            }

            return item;
        }

        public WaterBacteriaCsvFile PopulateCsvFileFromPreviousCsvItem(WaterBacteriaCsvFile item)
        {
            try
            {
                if (item == null)
                {
                    proceed_csv_process = false;
                    item.InsertResult = $"{item.InsertResult} No Customer id or First and Last Name were provided. All inserts will be cancelled";
                    return item;
                }
                item = PopulateCsvFileFromExistingCustomerDb(item);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateCsvFileFromCustomerDb(): {exc.InnerException}");
                item.InsertResult = $"{item.InsertResult} Exception error in processing customerid: {item.CustomerID}. All inserts will be cancelled";
            }
            return item;
        }

        public bool IsOkToProcessCsv()
        {
            return proceed_csv_process;
        }

        public hlab_test_transactions GenerateWaterBacteriaDbObject(WaterBacteriaObject water_bacteria)
        {
            hlab_test_transactions watersample = new hlab_test_transactions();
            watersample.test_pkg_id = water_bacteria.test_package_id;
            watersample.price = water_bacteria.test_package_fee;
            watersample.is_paid = true;
            watersample.test_date = null;
            watersample.is_flood_sample = false;
            watersample.notes = "";
            watersample.customer_id = water_bacteria.customer_id;
            watersample.date_entered = DateTime.Now;
            watersample.work_date = null;
            watersample.transaction_type_id = 0;
            watersample.report_type_id = 0;
            watersample.idnty_name = "";
            watersample.temp = water_bacteria.temperature;
            watersample.idnty_location = water_bacteria.location;
            watersample.sample_legal_loc = water_bacteria.legal_location;
            watersample.town = water_bacteria.town;
            watersample.status = true;
            watersample.existence = true;
            watersample.is_good_condition = true;
            watersample.latitude = "";
            watersample.longitude = "";
            watersample.utm_x = "";
            watersample.utm_y = "";
            watersample.zone = "";
            return watersample;
        }

        public WaterBacteriaCsvFile AssignCouponToWaterTestTransaction(WaterBacteriaCsvFile water_bateria_csv)
        {
            try
            {
                if (!string.IsNullOrEmpty(water_bateria_csv.Coupon))
                {
                    if (!_customer.IsCustomerHasValidCoupon(
                        Convert.ToInt32(water_bateria_csv.CustomerID), 
                        Convert.ToInt32(water_bateria_csv.Coupon)
                        ))
                    {
                        water_bateria_csv.Coupon = "0";
                        water_bateria_csv.InsertResult = $"{water_bateria_csv.InsertResult} Unable to assign Coupon:{water_bateria_csv.Coupon} [Coupon set to 0].";
                        return water_bateria_csv;
                    }

                    return water_bateria_csv;
                }
            }
            catch (Exception exc)
            {
                water_bateria_csv.Coupon = "0";
                water_bateria_csv.InsertResult = $"{water_bateria_csv.InsertResult} Unable to process Coupon:{water_bateria_csv.Coupon} or CustomerID: {water_bateria_csv.CustomerID} [Coupon set to 0].";
            }

            return water_bateria_csv;
        }

        public DateTime? ProcessCsvDateTime(WaterBacteriaObject water_bacteria_obj)
        {
            DateTime? output_datetime = DateTime.Now;
            try
            {
                
                if (string.IsNullOrEmpty(water_bacteria_obj.strdate)) water_bacteria_obj.strdate = "01/01/2000";
                if (string.IsNullOrEmpty(water_bacteria_obj.strtime)) water_bacteria_obj.strtime = "00:00";
                output_datetime = DateTime.Parse($"{water_bacteria_obj.strdate} {water_bacteria_obj.strtime}");
                InsertResult = water_bacteria_obj.insert_result;
            }
            catch (Exception exc)
            {                
                InsertResult = $"{water_bacteria_obj.insert_result} Unable to process collect date/time:{water_bacteria_obj.strdate} {water_bacteria_obj.strtime} [Set to current date time.].";
            }

            return output_datetime;
        }

        public int GetDbProjectId(WaterBacteriaCsvFile water_bateria_csv)
        {
            int project_id = 0;
            if (!string.IsNullOrEmpty(water_bateria_csv.Project))
            {
                List<hlab_test_projects> projects = new List<hlab_test_projects>();
                projects = _hlabTestProject.GetAllTestProjects().ToList();
                try
                {
                    if (projects.Where(x => x.project.ToLower() == water_bateria_csv.Project.ToLower()).FirstOrDefault() != null)
                    {
                        project_id = projects.Where(x => x.project.ToLower() == water_bateria_csv.Project.ToLower()).FirstOrDefault().id;
                    }
                    else
                    {
                        InsertResult = $"{water_bateria_csv.InsertResult} Unable to identify Project:{water_bateria_csv.Project} [Project Set to blank.].";
                    }
                }
                catch (Exception exc)
                {
                    InsertResult = $"{water_bateria_csv.InsertResult} Exception Error in identifying Project:{water_bateria_csv.Project} [Project Set to blank.].";
                }

            }
            return project_id;
        }

        public int GetDbReceivedById(WaterBacteriaCsvFile water_bateria_csv)
        {
            int rcv_by_id = 0;
            if (!string.IsNullOrEmpty(water_bateria_csv.ReceivedBy))
            {
                List<hlab_receivers> receivers = new List<hlab_receivers>();
                receivers = _Receiver.GetAllReceivers().ToList();
                try
                {
                    if (receivers.Where(x => x.receiver.ToLower() == water_bateria_csv.ReceivedBy.ToLower()).FirstOrDefault() != null)
                    {
                        rcv_by_id = receivers.Where(x => x.receiver.ToLower() == water_bateria_csv.ReceivedBy.ToLower()).FirstOrDefault().id;
                    }
                    else
                    {
                        InsertResult = $"{water_bateria_csv.InsertResult} Unable to identify receiver:{water_bateria_csv.ReceivedBy} [Receiver Set to blank].";
                    }
                }
                catch (Exception exc)
                {
                    InsertResult = $"{water_bateria_csv.InsertResult} Exception Error in identifying receiver:{water_bateria_csv.ReceivedBy} [Receiver Set to blank].";
                }
            }

            return rcv_by_id;
        }

        public int GetDbSampleTypeId(WaterBacteriaCsvFile water_bateria_csv)
        {
            int sample_type_id = 0;
            if (!string.IsNullOrEmpty(water_bateria_csv.ReceivedBy))
            {
                try
                {
                    List<hlab_test_sample_types> sampletypes = new List<hlab_test_sample_types>();
                    sampletypes = _HlabTestSample.GetAllTestSampleTypes().ToList();
                    if (sampletypes.Where(x => x.sample_type.ToLower() == water_bateria_csv.SampleType.ToLower()).FirstOrDefault() != null)
                    {
                        sample_type_id = sampletypes.Where(x => x.sample_type.ToLower() == water_bateria_csv.SampleType.ToLower()).FirstOrDefault().id;
                    }
                    else
                    {
                        InsertResult = $"{water_bateria_csv.InsertResult} Unable to identify Sample Type:{water_bateria_csv.SampleType} [Sample Type set to blank].";
                    }
                }
                catch (Exception exc)
                {
                    InsertResult = $"{water_bateria_csv.InsertResult} Exception Error in identifying Sample Type:{water_bateria_csv.SampleType} [Sample Type set to blank].";
                }
            }

            return sample_type_id;
        }

        public int GetDbMunicipalityId(WaterBacteriaCsvFile water_bateria_csv)
        {
             int rm_id = 0;
            if (!string.IsNullOrEmpty(water_bateria_csv.Municipality))
            {
                try
                {
                    List<hlab_rural_municipalities> municipalities = new List<hlab_rural_municipalities>();
                    municipalities = _Municipality.GetRuralMunicipalities().ToList();
                    if (municipalities.Where(x => x.rural_municipality.ToLower() == water_bateria_csv.Municipality.ToLower()).FirstOrDefault() != null)
                    {
                        rm_id = municipalities.Where(x => x.rural_municipality.ToLower() == water_bateria_csv.Municipality.ToLower()).FirstOrDefault().id;
                    }
                    else
                    {
                        InsertResult = $"{water_bateria_csv.InsertResult} Unable to identify Municipality:{water_bateria_csv.Municipality} [Municipality set to blank].";
                    }
                }
                catch (Exception exc)
                {
                    InsertResult = $"{water_bateria_csv.InsertResult} Exception Error in identifying Municipality:{water_bateria_csv.Municipality} [Municipality set to blank].";
                }
            }

            return rm_id;
        }

        public hlab_test_transactions InitializeTestTransactionDbObject(WaterBacteriaCsvFile item)
        {
            hlab_test_transactions watersample = new hlab_test_transactions();
            try
            {                
                WaterBacteriaCsvFile csv_item = new WaterBacteriaCsvFile();
                TestPackageObject labpackage = new TestPackageObject();
                int b1Subsidy = 10; ;
                labpackage = _testpackage.GetLabPakcageObject(item.LabPackage);
                watersample = GenerateWaterBacteriaDbObject(new WaterBacteriaObject
                {
                    test_package_id = labpackage.lab_package_id,
                    test_package_fee = labpackage.lab_package_fee,
                    customer_id = Convert.ToInt32(item.CustomerID),
                    temperature = item.Temperature,
                    location = item.SampleID,
                    legal_location = item.LegalLocation,
                    town = item.Town
                });

                if (labpackage.lab_package_id == b1Subsidy)//assign coupons for B1 Subsidy(id:10) only
                {
                    csv_item = AssignCouponToWaterTestTransaction(item);
                    watersample.assigned_coupon = Convert.ToInt32(csv_item.Coupon);
                    item.InsertResult = csv_item.InsertResult;
                }

                watersample.submtd_by = string.IsNullOrEmpty(item.SubmittedBy) ? item.SubmittedBy : $"{item.FirstName} {item.LastName}";

                watersample.collect_datetime = ProcessCsvDateTime(new WaterBacteriaObject
                {
                    strdate = item.CollectDate,
                    strtime = item.CollectTime,
                    insert_result = item.InsertResult
                });

                watersample.submtd_datetime = ProcessCsvDateTime(new WaterBacteriaObject
                {
                    strdate = item.SubmittedDate,
                    strtime = item.SubmittedTime,
                    insert_result = item.InsertResult
                });

                watersample.rcv_date = ProcessCsvDateTime(new WaterBacteriaObject
                {
                    strdate = item.ReceivedDate,
                    strtime = item.ReceivedTime,
                    insert_result = item.InsertResult
                });

                watersample.project_id = GetDbProjectId(item);
                watersample.rcv_by_id = GetDbReceivedById(item);
                watersample.sample_type_id = GetDbSampleTypeId(item);
                watersample.rm_id = GetDbMunicipalityId(item);                
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > InitializeTestTransactionDbObject() : {exc.Message}");
                InsertResult = $"{item.InsertResult} HWaterBacteria > InitializeTestTransactionDbObject() : {exc.Message}";
            }
            return watersample;
        }

        public bool IsWaterBacteriaCsvNotNullOrEmpty(hlab_test_transactions watersample)
        {
            if (
                watersample.project_id != 0
                && watersample.idnty_location != ""
                && watersample.customer_id != 0
                && watersample.temp != ""
                && watersample.sample_legal_loc != ""
                && watersample.town != ""
                && (watersample.collect_datetime != DateTime.Parse($"01/01/2000 00:00") || watersample.collect_datetime.Value.Date != DateTime.Now.Date)
                && (watersample.submtd_datetime != DateTime.Parse($"01/01/2000 00:00") || watersample.submtd_datetime.Value.Date != DateTime.Now.Date)
                && (watersample.rcv_date != DateTime.Parse($"01/01/2000 00:00") || watersample.rcv_date.Value.Date != DateTime.Now.Date)
                && watersample.sample_type_id != 0
                && watersample.rm_id != 0)
            {
                return true;
            }
            return false;
        }

        public WaterBacteriaCsvFile PopulateCsvFileFromNewCustomerDb(WaterBacteriaCsvFile item)
        {
            WaterBacteriaCsvFile csv_item = new WaterBacteriaCsvFile();
            try
            {
                customerparameters customer_param = new customerparameters();
                int new_customerid = 0;

                customer_param = _customer.CreateCustomerDbObject(item);
                new_customerid = _hlabCustomers.AddCustomer(customer_param);
                csv_item.CustomerID = new_customerid.ToString();
                csv_item.FirstName = customer_param.hlab_customers.first_name;
                csv_item.LastName = customer_param.hlab_customers.last_name;
                csv_item.Postal = customer_param.hlab_customers.postal_code;
                csv_item.InsertResult = " New Customer Successfully added.";
                return csv_item;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateCsvFileFromNewCustomerDb() : {exc.Message}");
                csv_item.InsertResult = " Error on getting customer information.";
                proceed_csv_process = false;
                return null;
            }
        }

        public bool InsertCsvWaterBacteriaRowToDB(WaterBacteriaObject water_bacteria_object)
        {
            try
            {
                int? orderid = 0, transid = 0;
                WaterBacteriaCsvFile item = water_bacteria_object.current_csv_row;
                WaterBacteriaCsvFile prev_csv_item = water_bacteria_object.previous_csv_row;
                hlab_test_transactions watersample = water_bacteria_object.watersample;
                TestPackageObject test_package_object = water_bacteria_object.test_package_object;
                bool current_proceed_csv_process = water_bacteria_object.proceed_csv_process;
                //insert Lab Request/order
                orderid = _request.InsertCsvTestRequestToDatabase(new WaterBacteriaObject
                {
                    current_csv_row = item,
                    previous_csv_row = prev_csv_item,
                    proceed_csv_process = current_proceed_csv_process
                });
                proceed_csv_process = _request.proceed_csv_process;
                InsertResult = _request.InsertResult;

                //insert payments
                _payment.InsertWaterBacteriaPaymentsCsvToDatabase(new WaterBacteriaObject
                {
                    payment = item.Payment1,
                    payment_item_label = "Payment #1",
                    amount = item.Amount1,
                    request_id = (int)orderid,
                    proceed_csv_process = current_proceed_csv_process
                });
                InsertResult = _payment.InsertResult;
                proceed_csv_process = _payment.proceed_csv_process;

                _payment.InsertWaterBacteriaPaymentsCsvToDatabase(new WaterBacteriaObject
                {
                    payment = item.Payment2,
                    payment_item_label = "Payment #2",
                    amount = item.Amount2,
                    request_id = (int)orderid,
                    proceed_csv_process = current_proceed_csv_process
                });
                InsertResult = _payment.InsertResult;
                proceed_csv_process = _payment.proceed_csv_process;

                _payment.InsertWaterBacteriaPaymentsCsvToDatabase(new WaterBacteriaObject
                {
                    payment = item.Payment3,
                    payment_item_label = "Payment #3",
                    amount = item.Amount3,
                    request_id = (int)orderid,
                    proceed_csv_process = current_proceed_csv_process
                });
                InsertResult = _payment.InsertResult;
                proceed_csv_process = _payment.proceed_csv_process;

                //insert water sample information                            
                transid = _transaction.InsertCsvWaterBacteriaTestTransactionToDatabase(new WaterBacteriaObject
                {
                    current_csv_row = item,
                    watersample = watersample,
                    test_package_object = test_package_object
                });
                InsertResult = _transaction.InsertResult;

                //Add order item
                _requestitem.InsertCsvRequestItemToDatabase(new WaterBacteriaObject
                {
                    test_package_object = test_package_object,
                    request_id = (int)orderid,
                    transaction_id = (int)transid,
                    current_csv_row = item,
                    proceed_csv_process = current_proceed_csv_process
                });
                item.InsertResult = _requestitem.InsertResult;

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateCsvFileFromNewCustomerDb() : {exc.Message}");
                return false;
            }
        }

        public test_transaction PopulateTestTransactionParameterObjFromSession()
        {
            try
            {
                test_transaction parameter = new test_transaction();
                parameter.trans_id = _sessionHelper.GetIntSearchWaterBacteriaTransactionId();
                parameter.customer_id = _sessionHelper.GetSearchCustomerId();
                parameter.test_pkg_id = _sessionHelper.GetSearchPackakgeId();

                if (_sessionHelper.IsSearchSubmitStartDateHasValue()) parameter.submtd_datetime_start = _sessionHelper.GetSessionSubmitStart();
                if (_sessionHelper.IsSearchSubmitEndDateHasValue()) parameter.submtd_datetime_end = _sessionHelper.GetSessionSubmitEnd();
                if (_sessionHelper.IsReceivedStartDateHasValue()) parameter.rcv_date_start = _sessionHelper.GetSessionReceivedStartDate();
                if (_sessionHelper.IsReceivedEndDateHasValue()) parameter.rcv_date_end = _sessionHelper.GetSessionReceviedEndDate();
                if (_sessionHelper.IsTestStartDateHasValue()) parameter.test_date_start = _sessionHelper.GetSessionTestStartDate();
                if (_sessionHelper.IsTestdEndDatHasValue()) parameter.test_date_end = _sessionHelper.GetSessionTestEndDate();

                if (_sessionHelper.IsProjectStartDateHasValue()) parameter.project_date_start = _sessionHelper.GetSessionProjStartDate();
                if (_sessionHelper.IsProjectEndDateHasValue()) parameter.project_date_end = _sessionHelper.GetSessionProjEndDate();

                if (_sessionHelper.IsSearchCustomerFirstNameHasValue()) parameter.first_name = _sessionHelper.GetSearchCustomerFirstName();
                if (_sessionHelper.IsSearchCustomerLastNameHasValue()) parameter.last_name = _sessionHelper.GetSearchCustomerLastName();

                parameter.assigned_coupon = 0;
                parameter.project_id = _sessionHelper.GetProjectId();
                parameter.class_id = _sessionHelper.GetSearchPackakgeClassId(); //default: WaterSample = 1
                return parameter;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateTestTransactionParameterFromSession() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public TestResultPageViewModel PopulateWaterBacteriaEditPageSelectListFields(string transid)
        {
            try
            {
                List<hlab_test_params> testparams = new List<hlab_test_params>();
                TestResultPageViewModel page_model = new TestResultPageViewModel();
                page_model.customer_order_payment_list = new List<orderpaymentsview>();
                page_model.selectCustomerCoupon = new List<SelectListItem>();

                page_model.isPaid = _utility.GenerateTrueFalseSelectList();
                page_model.isFloodSample = _utility.GenerateTrueFalseSelectList();
                page_model.selectExistence = _utility.GenerateTrueFalseSelectList();
                page_model.selectTranTypeList = _utility.GenerateSelectListItem(_transaction.GetAllTestTransactionsTypes(), "id", "transaction_type");
                page_model.selectTestDescriptions = _utility.GenerateSelectListItem(_transaction.GetAllTestDescriptionDb(), "description", "description");
                page_model.selectTestDescriptions.Add(new SelectListItem { Selected = false, Text = "", Value = "" });
                page_model.selectReceiversList = _utility.GenerateSelectListItem(_Receiver.GetAllReceivers().ToList(), "id", "receiver");
                page_model.selectReceiversList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                page_model.selectPaymentOptionList = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentOptions().ToList(), "id", "payment_option");
                page_model.selectPaymentTypeList = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentTypes().ToList(), "id", "payment");
                page_model.selectProjectList = _utility.GenerateSelectListItem(_hlabTestProject.GetAllTestProjects().ToList(), "id", "project");
                page_model.selectProjectList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                page_model.selectSampleTypeList = _utility.GenerateSelectListItem(_HlabTestSample.GetAllTestSampleTypes().ToList(), "id", "sample_type");
                page_model.selectSampleTypeList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                page_model.selectTestPackageList = _utility.GenerateSelectListItem(_hlabPkgCtgry.GetTestPackages(0).ToList(), "id", "lab_code");
                page_model.selectTestPackageList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                page_model.selectReportTypeList = _utility.GenerateSelectListItem(_HlabTestReport.GetAllReportTypes().ToList(), "id", "report_type");
                page_model.selectReportTypeList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                page_model.selectMunicipalityList = _utility.GenerateSelectListItem(_Municipality.GetRuralMunicipalities().ToList(), "id", "rural_municipality");
                page_model.selectMunicipalityList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                page_model.selectTown = _utility.GenerateSelectListItem(_city.GetAllCities(_mb_province_id).ToList(), "city", "city");
                page_model.selectTown.Add(new SelectListItem { Selected = true, Text = "", Value = "" });

                testparams = _HlabTestParameter.GetAllTestParameters().Where(x => x.pkg_class_id == (int)TestPakcageClass.WaterBacteria).ToList();
                page_model.selectTestParameters = _utility.GenerateSelectListItem(testparams, "param_id", "param_name");
                page_model.selectTestParameters.Add(new SelectListItem { Selected = true, Text = "SELECT PARAMETER", Value = "0" });

                page_model.selectUnitMeasurements = _utility.GenerateSelectListItem(_UnitOfMeasure.GetAllUnitMeasurements().ToList(), "id", "unit_of_measurement");
                page_model.selectUnitMeasurements.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
            
                return page_model;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateWaterBacteriaEditPageSelectListFields() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public TestResultPageViewModel PopulateWaterBacteriaEditPageListObjects(TestResultPageViewModel page_model, string transid)
        {
            try
            {
                if (string.IsNullOrEmpty(transid)) return page_model;

                orderdetailsview orderitemparam = new orderdetailsview();               
                testresultsview result_param = new testresultsview();
                List<sp_getcustomercouponrecords> getcustomercoupon_list = new List<sp_getcustomercouponrecords>();
                bool selectCoupon = false;
                int? customerid = 0, orderid = 0;

                page_model.trans_details = _hlabTestTransRepo.GetTransactionDetails(Convert.ToInt32(transid));
                page_model.result_list = _hlabTestResult.GetAllTestResults(new testresultsview { trans_id = Convert.ToInt32(transid) }).ToList();
                page_model.hlab_invoice_list = _hlabInvoice.GetTransactionInvoice(new sp_gethorizonlabtransactioninvoices { trans_id = Convert.ToInt32(transid) }).ToList();

                if(page_model.trans_details != null)
                {
                    page_model.isPaid.Where(x => x.Value == page_model.trans_details.is_paid.ToString().ToLower()).FirstOrDefault().Selected = true;
                    page_model.isFloodSample.Where(x => x.Value == page_model.trans_details.is_flood_sample.ToString().ToLower()).FirstOrDefault().Selected = true;
                    page_model.selectExistence.Where(x => x.Value == page_model.trans_details.existence.ToString().ToLower()).FirstOrDefault().Selected = true;

                    if (page_model.trans_details.project_id != null) page_model.selectProjectList.Where(x => x.Value == page_model.trans_details.project_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.trans_details.sample_type_id != null) page_model.selectSampleTypeList.Where(x => x.Value == page_model.trans_details.sample_type_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.trans_details.test_pkg_id != null) page_model.selectTestPackageList.Where(x => x.Value == page_model.trans_details.test_pkg_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.trans_details.report_type_id != null) page_model.selectReportTypeList.Where(x => x.Value == page_model.trans_details.report_type_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.trans_details.rm_id != null) page_model.selectMunicipalityList.Where(x => x.Value == page_model.trans_details.rm_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.trans_details.rcv_by_id != null) page_model.selectReceiversList.Where(x => x.Value == page_model.trans_details.rcv_by_id.ToString()).FirstOrDefault().Selected = true;
                }

                if (page_model.hlab_invoice_list.Count > 0)
                {
                    if (page_model.hlab_invoice_list[0].payment_option_id != null) page_model.selectPaymentOptionList.Where(x => x.Value == page_model.hlab_invoice_list[0].payment_option_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.hlab_invoice_list[0].payment_type_id != null) page_model.selectPaymentTypeList.Where(x => x.Value == page_model.hlab_invoice_list[0].payment_type_id.ToString()).FirstOrDefault().Selected = true;
                }
                orderitemparam.trans_id = Convert.ToInt32(transid);
                orderitemparam.order_id = 0; //have to set order id to 0, so results will be filtered by transaction id only
                page_model.orderitemview = _hlabOrderRepo.GetOrderItems(orderitemparam).FirstOrDefault();
                orderid = page_model.orderitemview.order_id;
                customerid = page_model.orderitemview.customer_id;

                if (page_model.orderitemview != null) page_model.customer_order_payment_list = _hlabPayment.GetAllPayments(new orderpaymentsview { order_id = (int)orderid }).ToList();

                if (customerid != 0)
                {
                    getcustomercoupon_list = _hlabCouponLog.GetCustomerCouponRecord(new couponrecord { customerid = customerid ?? 0 }).Where(x => x.trans_id != 0).ToList();
                    if (page_model.trans_details != null && page_model.trans_details.gen_coupon.GetValueOrDefault(0) == 0) page_model.selectCustomerCoupon.Add(new SelectListItem { Selected = false, Text = "", Value = "" });
                    foreach (var coupon in getcustomercoupon_list)
                    {
                        selectCoupon = false;
                        if (page_model.trans_details != null && coupon.coupon == page_model.trans_details.gen_coupon) selectCoupon = true;
                        page_model.selectCustomerCoupon.Add(new SelectListItem { Selected = selectCoupon, Text = $"{coupon.coupon} ({coupon.coupon_issued_date.Value.ToShortDateString()})", Value = $"{coupon.coupon}" });
                    }

                }
                return page_model;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateWaterBacteriaEditPageListObjects() : {exc.Message}");
                throw exc.InnerException;
            }
        }

        public TestResultPageViewModel PopulateNewWaterBacteriaPageObject(TestResultPageViewModel page_model)
        {
            try
            {
                string subsidy_program = "1", private_report = "5", package_id = "";
                string bacteria_subsidy = "10", bacteria_non_subsidy = "1";
                int request_id = page_model.selected_request_id;
                int request_item_id = page_model.selected_request_item_id;

                page_model.orderitemview = _hlabOrderRepo.GetOrderItems(new orderdetailsview { 
                    order_id = request_id,
                    order_item_id = request_item_id
                }).Where(x => x.order_item_id == request_item_id).FirstOrDefault();
                package_id = Convert.ToString(page_model.orderitemview.pkg_id);

                if (request_id != 0 && request_item_id != 0)
                {
                    page_model.selectTranTypeList = _utility.GenerateSelectListItem(_hlabTestTransRepo.GetTestTransactionTypes().ToList(), "id", "transaction_type");
                    page_model.selectReceiversList = _utility.GenerateSelectListItem(_Receiver.GetAllReceivers().ToList(), "id", "receiver");
                    page_model.selectReceiversList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                    page_model.selectPaymentOptionList = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentOptions().ToList(), "id", "payment_option");
                    page_model.selectPaymentTypeList = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentTypes().ToList(), "id", "payment");

                    page_model.selectProjectList = _utility.GenerateSelectListItem(_hlabTestProject.GetAllTestProjects().ToList(), "id", "project");
                    page_model.selectProjectList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

                    page_model.selectSampleTypeList = _utility.GenerateSelectListItem(_HlabTestSample.GetAllTestSampleTypes().ToList(), "id", "sample_type");
                    page_model.selectSampleTypeList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

                    page_model.selectTestPackageList = _utility.GenerateSelectListItem(_hlabPkgCtgry.GetTestPackages(0).ToList(), "id", "lab_code");
                    page_model.selectTestPackageList.Add(new SelectListItem { Selected = false, Text = "", Value = "0" });
                    page_model.selectTestPackageList = _utility.SetSelectedItemFromList(page_model.selectTestPackageList, package_id);

                    page_model.selectReportTypeList = _utility.GenerateSelectListItem(_HlabTestReport.GetAllReportTypes().ToList(), "id", "report_type");
                    page_model.selectReportTypeList = _utility.SetSelectedItemFromList(page_model.selectReportTypeList, subsidy_program);

                    if (package_id == bacteria_subsidy) page_model.selectReportTypeList = _utility.SetSelectedItemFromList(page_model.selectReportTypeList, subsidy_program);
                    if (package_id == bacteria_non_subsidy) page_model.selectReportTypeList = _utility.SetSelectedItemFromList(page_model.selectReportTypeList, private_report);

                    page_model.selectMunicipalityList = _utility.GenerateSelectListItem(_Municipality.GetRuralMunicipalities().ToList(), "id", "rural_municipality");
                    page_model.selectMunicipalityList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                    page_model.selectMunicipalityList = page_model.selectMunicipalityList.OrderBy(x => x.Text).ToList();

                    page_model.selectTestParameters = _utility.GenerateSelectListItem(_HlabTestParameter.GetAllTestParameters().ToList(), "param_id", "param_name");
                    page_model.selectTestParameters.Add(new SelectListItem { Selected = true, Text = "SELECT PARAMETER", Value = "0" });

                    page_model.selectUnitMeasurements = _utility.GenerateSelectListItem(_UnitOfMeasure.GetAllUnitMeasurements().ToList(), "id", "unit_of_measurement");
                    page_model.selectUnitMeasurements.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

                    page_model.customer_order_payment_list = _hlabPayment.GetAllPayments(new orderpaymentsview { order_id = request_id }).ToList();
                    page_model.orderitemview = _hlabOrderRepo.GetOrderItems(new orderdetailsview { order_id = request_id }).ToList()
                        .Where(x => x.order_item_id == request_item_id).FirstOrDefault();
                    page_model.customer_info = _hlabCustomers.GetCustomersDetails(new hlab_customers { customer_id = (int)page_model.orderitemview.customer_id });

                    page_model.selectTown = _utility.GenerateSelectListItem(_city.GetAllCities(_mb_province_id).ToList(), "city", "city");
                    page_model.selectTown.Add(new SelectListItem { Selected = true, Text = "", Value = "" });
                    page_model.selectTown = page_model.selectTown.OrderBy(x => x.Text).ToList();
                }

                return page_model;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HWaterBacteria > PopulateNewWaterBacteriaPageSelectList() : {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

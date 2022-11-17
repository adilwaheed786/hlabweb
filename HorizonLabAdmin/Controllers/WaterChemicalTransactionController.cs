using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
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

namespace HorizonLabAdmin.Controllers
{
    public class WaterChemicalTransactionController : Controller
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private hlab_test_payments _payment_param = new hlab_test_payments();
        private orderpaymentsview _orderpayment_param = new orderpaymentsview();
        private hlab_customers _customer = new hlab_customers();
        private sp_gethorizonlabtransactioninvoices _invoice = new sp_gethorizonlabtransactioninvoices();

        private readonly Interface_test_transactions _hlabTestTransRepo;      
        private readonly Interface_test_package _hlabPkgCtgry;
        private readonly Interface_water_chem_result _hlabWaterChem;
        private readonly IHostingEnvironment _env;
        private readonly Interface_hlab_customers _hlabCustomers;
        private readonly Interface_hlab_invoice _hlabInvoice;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly IConfiguration _appConfig;
        private readonly Interface_hlab_test_payments _payments;
        private readonly Interface_receivers _receivers;
        private readonly int _batchRecord = 100;
        private readonly ILogger<WaterChemicalTransactionController> _logger;

        public WaterChemicalTransactionController(
            IConfiguration appConfig,
            IHostingEnvironment env,
            Interface_test_transactions hlabTestTransRepo,
            Interface_test_package hlabPkgCtgry,
            Interface_water_chem_result hlabWaterChem,
            Interface_hlab_customers hlabCustomers,
            Interface_hlab_invoice hlabInvoice,
            Interface_hlab_orders hlabOrderRepo,
            Interface_hlab_test_payments hlabPayment,
            ILogger<WaterChemicalTransactionController> logger)
        {
            _hlabPkgCtgry = hlabPkgCtgry;
            _hlabWaterChem = hlabWaterChem;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabOrderRepo = hlabOrderRepo;
            _hlabPayment = hlabPayment;
            _env = env;
            _hlabCustomers = hlabCustomers;
            _hlabInvoice = hlabInvoice;
            _appConfig = appConfig;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult NewWaterChemForm(int oid = 0, int oiid = 0)
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            horizonlabcustomerview customer = new horizonlabcustomerview();
            orderdetailsview orderviewparam = new orderdetailsview();
            List<hlab_test_payment_options> paymentoptions = new List<hlab_test_payment_options>();
            List<hlab_test_payment_types> paymenttypes = new List<hlab_test_payment_types>();
            List<hlab_receivers> receivers = new List<hlab_receivers>();

            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");

                if (oid != 0 && oiid != 0)
                {
                    _customer.customer_id = page_model.orderitemview.customer_id.Value;
                    orderviewparam.order_id = oid;
                    page_model.orderitemview = _hlabOrderRepo.GetOrderItems(orderviewparam).ToList().Where(x => x.order_item_id == oiid).FirstOrDefault();
                    customer = _hlabCustomers.GetCustomersDetails(_customer);
                }

                page_model.isPaid = new List<SelectListItem>
                {
                new SelectListItem { Selected = false, Text = "True", Value = "true"},
                new SelectListItem { Selected = false, Text = "False", Value = "false"}
                };

                page_model.isGoodCondition = new List<SelectListItem>
                {
                new SelectListItem { Selected = false, Text = "True", Value = "true"},
                new SelectListItem { Selected = false, Text = "False", Value = "false"}
                };

                receivers = _receivers.GetAllReceivers().ToList();
                page_model.selectReceiversList = new SelectList(receivers, "id", "receiver").ToList();
                page_model.selectReceiversList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

                paymentoptions = _payments.GetAllPaymentOptions().ToList();
                page_model.selectPaymentOptionList = new SelectList(paymentoptions, "id", "payment_option").ToList();

                paymenttypes = _payments.GetAllPaymentTypes().ToList();
                page_model.selectPaymentTypeList = new SelectList(paymenttypes, "id", "payment").ToList();
                _orderpayment_param.order_id = oid;
                page_model.customer_order_payment_list = _hlabPayment.GetAllPayments(_orderpayment_param).ToList();

                ViewBag.customer = customer;
                ViewBag.menu = _hlabMenu;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpGet]
        public IActionResult ViewWaterChemForm(string transid = "0")
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            horizonlabcustomerview customer = new horizonlabcustomerview();
            orderdetailsview orderitemparam = new orderdetailsview();
            string Message = "";
            List<hlab_chem_water_results_set_b> water_set_b = new List<hlab_chem_water_results_set_b>();
            var label_array = new[] { "QC Batch #", "Test Date", "Technician", "Calibration Date", "ph Meter Used" };
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");
                List<hlab_test_payment_options> paymentoptions = new List<hlab_test_payment_options>();
                List<hlab_test_payment_types> paymenttypes = new List<hlab_test_payment_types>();
                page_model.list_waterchemresults_a = new List<hlab_chem_water_results_set_a>();
                page_model.list_waterchemresults_b = new List<hlab_chem_water_results_set_b>();
                page_model.list_tracemetal_results = new List<hlab_trace_metal_results>();

                page_model.trans_details = _hlabTestTransRepo.GetTransactionDetails(Convert.ToInt32(transid));
                _invoice.invoice_id = Convert.ToInt32(transid);
                page_model.hlab_invoice_list = _hlabInvoice.GetTransactionInvoice(_invoice).ToList();
                if (page_model.hlab_invoice_list.Count == 0) page_model.hlab_invoice_list.Add(new sp_gethorizonlabtransactioninvoices());

                page_model.list_waterchemresults_a = _hlabWaterChem.GetWaterChemResultA(Convert.ToInt32(transid)).ToList();

                page_model.list_waterchemresults_b = _hlabWaterChem.GetWaterChemResultB(Convert.ToInt32(transid)).ToList();
                water_set_b = _hlabWaterChem.GetWaterChemResultB(Convert.ToInt32(transid)).ToList();
                foreach (var a in label_array) { page_model.list_waterchemresults_b.Add(new hlab_chem_water_results_set_b()); }
                for (int i = 0; i < water_set_b.Count(); i++) { page_model.list_waterchemresults_b.Insert(i, water_set_b[i]); }

                page_model.list_tracemetal_results = _hlabWaterChem.GetTraceMetalResults(Convert.ToInt32(transid)).ToList();

                orderitemparam.trans_id = Convert.ToInt32(transid);
                orderitemparam.order_id = 0;//have to set order id to 0, so results will be filtered by transaction id only
                page_model.orderitemview = _hlabOrderRepo.GetOrderItems(orderitemparam).FirstOrDefault();
                _orderpayment_param.order_id = page_model.orderitemview.order_id;
                page_model.customer_order_payment_list = _hlabPayment.GetAllPayments(_orderpayment_param).ToList();
                _customer.customer_id = (int)page_model.trans_details.customer_id;
                customer = _hlabCustomers.GetCustomersDetails(_customer);

                if (TempData["EditWaterChemMessage"] != null) Message = TempData["EditWaterChemMessage"].ToString();
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.menu = _hlabMenu;
                ViewBag.customer = customer;
                ViewBag.Message = Message;
                ViewBag.label_array = label_array;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpGet]
        public IActionResult EditWaterChemPage(string transId = "0")
        {
            TestResultPageViewModel page_model = new TestResultPageViewModel();
            orderdetailsview orderitemview = new orderdetailsview();
            orderdetailsview orderitemparam = new orderdetailsview();
            horizonlabcustomerview customer = new horizonlabcustomerview();
            List<hlab_receivers> receivers = new List<hlab_receivers>();
            List<hlab_chem_water_results_set_a> water_set_a = new List<hlab_chem_water_results_set_a>();
            List<hlab_chem_water_results_set_b> water_set_b = new List<hlab_chem_water_results_set_b>();
            var label_array = new[] { "QC Batch #", "Test Date", "Technician", "Calibration Date", "ph Meter Used" };
            string Message = "";

            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "transactions");
                List<hlab_test_payment_options> paymentoptions = new List<hlab_test_payment_options>();
                List<hlab_test_payment_types> paymenttypes = new List<hlab_test_payment_types>();
                page_model.list_waterchemresults_a = new List<hlab_chem_water_results_set_a>();
                page_model.list_waterchemresults_b = new List<hlab_chem_water_results_set_b>();
                page_model.list_tracemetal_results = new List<hlab_trace_metal_results>();

                page_model.hlab_test_transactions = new hlab_test_transactions();
                page_model.trans_details = _hlabTestTransRepo.GetTransactionDetails(Convert.ToInt32(transId));
                _invoice.invoice_id = Convert.ToInt32(transId);
                page_model.hlab_invoice_list = _hlabInvoice.GetTransactionInvoice(_invoice).ToList();

                water_set_a = _hlabWaterChem.GetWaterChemResultA(Convert.ToInt32(transId)).ToList();
                for (int i = 0; i < 3; i++) { page_model.list_waterchemresults_a.Add(new hlab_chem_water_results_set_a()); }
                for (int i = 0; i < water_set_a.Count(); i++) { page_model.list_waterchemresults_a.Insert(i, water_set_a[i]); }


                water_set_b = _hlabWaterChem.GetWaterChemResultB(Convert.ToInt32(transId)).ToList();
                foreach (var a in label_array) { page_model.list_waterchemresults_b.Add(new hlab_chem_water_results_set_b()); }
                for (int i = 0; i < water_set_b.Count(); i++) { page_model.list_waterchemresults_b.Insert(i, water_set_b[i]); }

                page_model.list_tracemetal_results = _hlabWaterChem.GetTraceMetalResults(Convert.ToInt32(transId)).ToList();
                orderitemparam.trans_id = Convert.ToInt32(transId);
                orderitemparam.order_id = 0;

                page_model.isPaid = new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "True", Value = "true"},
                    new SelectListItem { Selected = false, Text = "False", Value = "false"}
                };

                page_model.isGoodCondition = new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "True", Value = "true"},
                    new SelectListItem { Selected = false, Text = "False", Value = "false"}
                };

                receivers = _receivers.GetAllReceivers().ToList();
                page_model.selectReceiversList = new SelectList(receivers, "id", "receiver").ToList();
                page_model.selectReceiversList.Add(new SelectListItem { Text = "", Value = "" });
                page_model.hlab_test_transactions.notes = page_model.trans_details.notes;
                page_model.hlab_test_transactions.is_good_condition = page_model.trans_details.is_good_condition;
                page_model.hlab_test_transactions.rcv_by_id = page_model.trans_details.rcv_by_id;
                page_model.hlab_test_transactions.submtd_by = page_model.trans_details.submtd_by;
                page_model.hlab_test_transactions.temp = page_model.trans_details.temp;
                page_model.hlab_test_transactions.collect_datetime = page_model.trans_details.collect_datetime;
                page_model.hlab_test_transactions.rcv_date = page_model.trans_details.rcv_date;                
                page_model.hlab_test_transactions.trans_id = page_model.trans_details.trans_id;                
                
                page_model.orderitemview = _hlabOrderRepo.GetOrderItems(orderitemparam).FirstOrDefault();
                _customer.customer_id = (int)page_model.orderitemview.customer_id;
                customer = _hlabCustomers.GetCustomersDetails(_customer);

                paymentoptions = _payments.GetAllPaymentOptions().ToList();
                page_model.selectPaymentOptionList = new SelectList(paymentoptions, "id", "payment_option").ToList();

                paymenttypes = _payments.GetAllPaymentTypes().ToList();
                page_model.selectPaymentTypeList = new SelectList(paymenttypes, "id", "payment").ToList();
                page_model.isPaid.Where(x => x.Value == page_model.trans_details.is_paid.ToString().ToLower()).FirstOrDefault().Selected = true;
                page_model.selectReceiversList.Where(x => x.Value == page_model.trans_details.rcv_by_id.ToString()).FirstOrDefault().Selected = true;
                page_model.isGoodCondition.Where(x => x.Value.ToLower() == page_model.trans_details.is_good_condition.ToString().ToLower()).FirstOrDefault().Selected = true;
                _orderpayment_param.order_id = page_model.orderitemview.order_id;
                page_model.customer_order_payment_list = _hlabPayment.GetAllPayments(_orderpayment_param).ToList();

                if (page_model.hlab_invoice_list.Count > 0)
                {
                    if (page_model.hlab_invoice_list[0].payment_option_id != null) page_model.selectPaymentOptionList.Where(x => x.Value == page_model.hlab_invoice_list[0].payment_option_id.ToString()).FirstOrDefault().Selected = true;
                    if (page_model.hlab_invoice_list[0].payment_type_id != null) page_model.selectPaymentTypeList.Where(x => x.Value == page_model.hlab_invoice_list[0].payment_type_id.ToString()).FirstOrDefault().Selected = true;
                }

                if (TempData["EditWaterChemMessage"] != null) Message = TempData["EditWaterChemMessage"].ToString();
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.menu = _hlabMenu;
                ViewBag.customer = customer;
                ViewBag.Message = Message;
                ViewBag.label_array = label_array;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(page_model);
        }

        [HttpPost]
        public IActionResult UpdateWaterChem(TestResultPageViewModel water_chem)
        {
            string result_message = "", csvfile="", fileextension="";
            List<sp_getdefaultpackageparameters> trace_metal_params = new List<sp_getdefaultpackageparameters>();
            List<sp_getdefaultpackageparameters> default_params = new List<sp_getdefaultpackageparameters>();
            hlab_trace_metal_results trace_metals_result = new hlab_trace_metal_results();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page
            bool test_result_query = false, add_setA = true, add_setB = true, add_trace_metals = true;
            string savepath = _env.WebRootPath + "\\csvfiles";
            List<string> trace_metals_db_code = new List<string>() { "B", "Sc", "Mn", "Fe-1", "Fe-2", "Cu", "Zn", "Ge", "As", "As-1", "Rh", "In", "Ba", "Tb", "Pb", "U", "Na", "Mg", "K", "Ca", "Sc-1", "Cl" };
            int count_setA = 0, count_setB = 0, count_tracemetals;

            try
            {
                default_params = _hlabTestTransRepo.GetDefaultParameters((int)water_chem.hlab_test_transactions.test_pkg_id).ToList();
                //trace_metal_params = default_params.Where(x => trace_metals_db_code.Contains(x.pkg_db_code)).OrderBy(a => a.param_id).ToList();

                //check if transaction id is not null or 0
                if (water_chem.hlab_test_transactions.trans_id != 0)
                {
                    //update water chem details / information
                    if (!_hlabTestTransRepo.UpdateTransactionDetails(water_chem.hlab_test_transactions)) result_message= "Error: Unable to save changes on Water Chemical Test details.";

                    //delete all test results by transid, then reseed - Set A
                    count_setA = _hlabWaterChem.GetWaterChemResultA(water_chem.hlab_test_transactions.trans_id).ToList().Count;
                    if (count_setA > 0)
                    {
                        if (_hlabWaterChem.DeleteReseedWaterChemA(water_chem.hlab_test_transactions.trans_id))
                        {
                            count_setA = _hlabWaterChem.GetWaterChemResultA(water_chem.hlab_test_transactions.trans_id).ToList().Count;
                            if (count_setA > 0)
                            {
                                result_message = "Error: Unable to delete water chemical test result A!";
                                add_setA = false;
                            }
                            else
                            {
                                add_setA = true;
                            }                                
                        }
                        else
                        {
                            result_message = "Error: Unable to delete water chemical test result A!";
                            add_setA = false;
                        }
                    }

                    if (add_setA)
                    {
                        //loop on hlab_test_result_list
                        foreach (var result in water_chem.list_waterchemresults_a)
                        {
                            //foreach hlab_test_result_list :
                            if (
                                !string.IsNullOrEmpty(result.nitrate) || !string.IsNullOrEmpty(result.nitrite) || !string.IsNullOrEmpty(result.ph) ||
                                !string.IsNullOrEmpty(result.conductivity) || !string.IsNullOrEmpty(result.sodium) || !string.IsNullOrEmpty(result.flouride) ||
                                !string.IsNullOrEmpty(result.chloride_tr) || !string.IsNullOrEmpty(result.chloride_df) || !string.IsNullOrEmpty(result.chloride_fr) ||
                                !string.IsNullOrEmpty(result.sulfate_tr) || !string.IsNullOrEmpty(result.sulfate_df) || !string.IsNullOrEmpty(result.sulfate_fr)
                                )
                            {
                                if (!_hlabWaterChem.AddWaterChemA(result)) result_message = "Error: One of the Water Chemical Result Set A failed to insert!"; 
                            }
                        }
                    }

                    //delete all test results by transid, then reseed - Set B
                    count_setB = _hlabWaterChem.GetWaterChemResultB(water_chem.hlab_test_transactions.trans_id).ToList().Count;
                    if (count_setB > 0)
                    {
                        if (_hlabWaterChem.DeleteReseedWaterChemB(water_chem.hlab_test_transactions.trans_id))
                        {
                            count_setB = _hlabWaterChem.GetWaterChemResultB(water_chem.hlab_test_transactions.trans_id).ToList().Count;
                            if ( count_setB > 0)
                            {
                                result_message = "Error: Unable to delete water chemical test result B!";
                                add_setB = false;
                            }
                            else
                            {
                                add_setB = true;
                            }
                                
                        }
                        else
                        {
                            result_message = "Error: Unable to delete water chemical test result B!";
                            add_setB = false;
                        }
                    }

                    if (add_setB)
                    {
                        //loop on hlab_test_result_list
                        foreach (var result in water_chem.list_waterchemresults_b)
                        {
                            //foreach hlab_test_result_list :
                            /*if (
                                !string.IsNullOrEmpty(result.nitrate_nitrite_info) || !string.IsNullOrEmpty(result.ph_info) || !string.IsNullOrEmpty(result.conductivity_info) ||
                                !string.IsNullOrEmpty(result.sodium_info) || !string.IsNullOrEmpty(result.flouride_info) || !string.IsNullOrEmpty(result.chloride_info) ||
                                !string.IsNullOrEmpty(result.sulfate_info)
                                )
                            {
                                
                            }*/
                            if (!_hlabWaterChem.AddWaterChemB(result)) result_message = "Error: One of the Water Chemical Result Set B failed to insert!";
                        }
                    }

                    //parse csv file                    
                    if (water_chem.water_chem_upload != null)
                    {
                        csvfile = Path.GetFileName(water_chem.water_chem_upload.FileName).ToString();
                        fileextension = Path.GetExtension(water_chem.water_chem_upload.FileName).ToString().ToLower();
                        //check if csv files is in right format
                        if (fileextension.ToLower() == ".csv")
                        {
                            savepath = savepath + "\\" + Guid.NewGuid().ToString("n") + ".csv";
                            using (var stream = new FileStream(savepath, FileMode.Create))
                            {
                                water_chem.water_chem_upload.CopyTo(stream);
                            }

                            //delete trace metals
                            count_tracemetals = _hlabWaterChem.GetTraceMetalResults(water_chem.hlab_test_transactions.trans_id).ToList().Count;
                            if(count_tracemetals > 0)
                            {
                                if (_hlabWaterChem.DeleteReseedTraceMetalResults(water_chem.hlab_test_transactions.trans_id))
                                {
                                    count_tracemetals = _hlabWaterChem.GetTraceMetalResults(water_chem.hlab_test_transactions.trans_id).ToList().Count;
                                    if (count_tracemetals > 0)
                                    {
                                        result_message = "Error: Unable to delete trace metals!";
                                        add_trace_metals = false;
                                    }
                                    else
                                    {
                                        add_trace_metals = true;
                                    }

                                }
                                else
                                {
                                    result_message = "Error: Unable to delete trace metals!";
                                    add_trace_metals = false;
                                }
                            }

                            if (add_trace_metals)
                            {
                                using (var reader = new StreamReader(savepath))
                                using (var c = new CsvReader(reader, CultureInfo.InvariantCulture))
                                {
                                    c.Configuration.HasHeaderRecord = true;
                                    var records = c.GetRecords<TraceMetalCsv>();
                                    foreach (var item in records)
                                    {
                                        trace_metals_result = new hlab_trace_metal_results();
                                        trace_metals_result.qc_stat = false;
                                        if (item.qc_status.ToLower() == "passed") trace_metals_result.qc_stat = true;
                                        trace_metals_result.description = item.sample_id;
                                        try { trace_metals_result.acquisition_date = DateTime.Parse(item.acquisition_date); } catch (Exception exc) { trace_metals_result.acquisition_date = DateTime.Now; }
                                        trace_metals_result.trans_id = water_chem.hlab_test_transactions.trans_id;
                                        trace_metals_result.Be9 = item.Be9;
                                        trace_metals_result.B10 = item.B10;
                                        trace_metals_result.Al_27 = item.Al_27;
                                        trace_metals_result.Ti_47 = item.Ti_47;
                                        trace_metals_result.V_1_51 = item.V_1_51;
                                        trace_metals_result.V_51 = item.V_51;
                                        trace_metals_result.Sc_45 = item.Sc_45;
                                        trace_metals_result.Cr_52 = item.Cr_52;
                                        trace_metals_result.Mn_55 = item.Mn_55;
                                        trace_metals_result.Fe_1_57 = item.Fe_1_57;
                                        trace_metals_result.Fe_2_57 = item.Fe_2_57;
                                        trace_metals_result.Co_59 = item.Co_59;
                                        trace_metals_result.Ni_60 = item.Ni_60;
                                        trace_metals_result.Cu_65 = item.Cu_65;
                                        trace_metals_result.Zn_66 = item.Zn_66;
                                        trace_metals_result.Ge_72 = item.Ge_72;
                                        trace_metals_result.As_75 = item.As_75;
                                        trace_metals_result.As_1_75 = item.As_1_75;
                                        trace_metals_result.Se_82 = item.Se_82;
                                        trace_metals_result.Sr_87 = item.Sr_87;
                                        trace_metals_result.Mo_98 = item.Mo_98;
                                        trace_metals_result.Rh_103 = item.Rh_103;
                                        trace_metals_result.Ag_107 = item.Ag_107;
                                        trace_metals_result.Cd_111 = item.Cd_111;
                                        trace_metals_result.In_115 = item.In_115;
                                        trace_metals_result.Sb_121 = item.Sb_121;
                                        trace_metals_result.Sn_118 = item.Sn_118;
                                        trace_metals_result.Ba_137 = item.Ba_137;
                                        trace_metals_result.Tb_159 = item.Tb_159;
                                        trace_metals_result.Tl_205 = item.Tl_205;
                                        trace_metals_result.Pb_208 = item.Pb_208;
                                        trace_metals_result.Th_232 = item.Th_232;
                                        trace_metals_result.U_238 = item.U_238;
                                        trace_metals_result.Na_23 = item.Na_23;
                                        trace_metals_result.Mg_24 = item.Mg_24;
                                        trace_metals_result.K_39 = item.K_39;
                                        trace_metals_result.Ca_44 = item.Ca_44;
                                        trace_metals_result.Sc_145 = item.Sc_145;
                                        trace_metals_result.CI_35 = item.CI_35;

                                        _hlabWaterChem.AddTraceMetalResults(trace_metals_result);
                                    }
                                }
                            }

                        }
                        else
                        {
                            result_message = "Error: File is not in csv format!";
                        }
                    }
                    //add water chem results from the csv file
                    if (test_result_query) result_message = "Error:One of the Test results (water chemical - manual test results) failed to save. Please contact administrator!";

                }
                else
                {
                    result_message = "Error:Unable to save changes. Transaction id is empty.";
                }

                if (!string.IsNullOrEmpty(result_message)) TempData["TestTransactionMessage"] = result_message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return RedirectToAction("ViewTransactions", "WaterBacteriaTransaction", new { pkg_class_id = 4});
        }

        //public IActionResult PartialWaterChemTestResult_a (int transid=0)
        //{
        //    return PartialView();
        //}
    }
}
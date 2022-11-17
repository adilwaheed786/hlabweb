using System;
using System.Collections.Generic;
using System.Linq;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary;
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
    public class CustomerController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private hlab_customers _customer = new hlab_customers();

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IUtility _utilityHelper;
        private readonly ICustomer _customerHelper;        
        private HorizonLabTableReferenceApiLibrary _hllTableReference = new HorizonLabTableReferenceApiLibrary();
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            IConfiguration appConfig,
            IHostingEnvironment env,
            ILogger<CustomerController> logger,
            IHorizonLabSession sessionHelper,
            IUtility utilityHelper,
            ICustomer customerHelper
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;
            _sessionHelper = sessionHelper;
            _utilityHelper = utilityHelper;
            _customerHelper = customerHelper;
        }

        public IActionResult Index()
        {
            return View();
        }     

        [HttpPost]
        public IActionResult NavigateCustomer(int customer_rec_start, int customer_rec_end)
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _sessionHelper.SetStartCustomerRecordSession(customer_rec_start);
                _sessionHelper.SetEndCustomerRecordSession(customer_rec_end);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return RedirectToAction("ViewCustomerListPage", "Customer");
        }

        [HttpPost]
        public IActionResult SearchCustomer(horizonlabcustomerview param)
        {
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _sessionHelper.SetSearchCustomerSessionInfo(new hlab_customers {
                    customer_id = param.customer_id,
                    first_name = param.first_name,
                    last_name = param.last_name,
                    company_name = param.company_name,
                    street = param.street,
                    status = param.status
                });
                _sessionHelper.SetSearchCustomerSessionEmail(param.email);
                //set navigation start-end to 0
                _sessionHelper.SetStartCustomerRecordSession(0);
                _sessionHelper.SetEndCustomerRecordSession(0);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return RedirectToAction("ViewCustomerListPage", "Customer", new { status = param.status.ToString().ToLower() });
        }

        public IActionResult ViewCustomerListPage(string status = "true", string sort_customer_name="asc", string sort_town="asc")
        {
            
            List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
            horizonlabcustomerview customer = new horizonlabcustomerview();            
            string Message = "", next_sort_customer_name = "asc", next_sort_town="asc";

            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");

                status = _utilityHelper.SetDefaultStatusIfInvalid(status);
                _sessionHelper.SetSearchCustomerSessionInfo(new hlab_customers {
                    customer_id = _sessionHelper.GetSearchCustomerId(),
                    first_name = _sessionHelper.GetSearchCustomerFirstName(),
                    last_name = _sessionHelper.GetSearchCustomerLastName(),
                    street = _sessionHelper.GetSearchCustomerAddress(),
                    company_name = _sessionHelper.GetSearchCompanyName(),
                    status = Convert.ToBoolean(status.ToLower())
                });

                if (_sessionHelper.IsCustomerSessionSearchHasValues())
                {
                    customer = _sessionHelper.GenerateCustomerObjectFromSession();
                    customer_list = _customerHelper.GetFilteredCustomersList(customer, sort_customer_name, sort_town);
                    customer_list = _customerHelper.ApplyPaginationToCustomerList(customer_list);
                }

                if (TempData["CustomerMessage"] != null)
                {
                    Message = TempData["CustomerMessage"].ToString();
                    TempData["CustomerMessage"] = null;

                }

                if (sort_customer_name == "asc") next_sort_customer_name = "desc";
                if (sort_town == "asc") next_sort_town = "desc";

                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.rec_start = _customerHelper.rec_start;
                ViewBag.rec_end = _customerHelper.rec_end;
                ViewBag.rec_count = _customerHelper.rec_count;
                ViewBag.customer_list = customer_list;
                ViewBag.menu = _hlabMenu;
                ViewBag.Message = Message;
                ViewBag.customerstatus = status;
                ViewBag.sort_customer_name = next_sort_customer_name;
                ViewBag.sort_town = next_sort_town;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(customer);
        }

        public IActionResult ViewCustomerDetails(int customerid)
        {
            horizonlabcustomerview customer = new horizonlabcustomerview();
            List<hlab_customer_phone> phonelist = new List<hlab_customer_phone>();
            List<hlab_customer_email> emaillist = new List<hlab_customer_email>();
            string Message = "";

            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");
                if (customerid != 0)
                {
                    customer = _customerHelper.GetDbCustomerInformation(customerid);
                    phonelist = _customerHelper.GetCustomerPhoneListFromDb(customerid);
                    emaillist = _customerHelper.GetCustomerEmailListFromDb(customerid);
                }
                else
                {
                    return GoToCustomerListPage();
                }

                if (TempData["CustomerMessage"] != null)
                {
                    Message = TempData["CustomerMessage"].ToString();
                    TempData["CustomerMessage"] = null;

                }
                ViewData["UserName"] = _sessionHelper.GetSessionUserName();
                ViewBag.menu = _hlabMenu;
                ViewBag.phonelist = phonelist;
                ViewBag.emaillist = emaillist;
                ViewBag.Message = Message;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            return View(customer);
        }

        public IActionResult ActivateCustomer(int customerid)
        {            
            string Message = "";            
            TempData["CustomerMessage"] = null;
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");                                
                Message = "Unable to Activate customer, please contact administrator!";
                if (customerid != 0)
                {                    
                    if (_customerHelper.ActivateCustomerRecord(customerid))
                    {
                        Message = "Customer was Activated successfully";
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (!string.IsNullOrEmpty(Message))
            {
                TempData["CustomerMessage"] = Message;

            }
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            return GoToCustomerListPage("false");
        }

        public IActionResult DeactivateCustomer(int customerid)
        {
            string Message = "";
            TempData["CustomerMessage"] = null;
            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");

                if (customerid != 0)
                {
                    Message = "Unable to deactivate customer, please contact administrator!";
                    if (_customerHelper.DeactivateCustomerRecord(customerid))
                    {
                        Message = "Customer was deactivated successfully";
                    }
                }
                else
                {
                    return GoToCustomerListPage();
                }               
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (!string.IsNullOrEmpty(Message))
            {
                TempData["CustomerMessage"] = Message;

            }
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            return GoToCustomerListPage();
        }

        public IActionResult EditCustomerForm(int cid = 0)
        {
            customerparameters customer = new customerparameters();
            string Message = "";
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");

            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();  
                customer = _customerHelper.PrepareCustomerHTMLPageFieldsData(cid);
                if (customer == null) TempData["CustomerMessage"] = "Invalid Customer ID";                
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (TempData["CustomerMessage"] != null)
            {
                Message = TempData["CustomerMessage"].ToString();
                TempData["CustomerMessage"] = null;
            }
            
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            return View(customer);
        }

        public IActionResult NewCustomerForm()
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");
            customerparameters customer = new customerparameters();
            List<hlab_cities> city_list = new List<hlab_cities>();
            List<hlab_provinces> province_list = new List<hlab_provinces>();
            string Message = ""; 

            try
            {
                if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "customer");

                customer = _customerHelper.PrepareCustomerHTMLPageFieldsData();
                if (customer == null) TempData["CustomerMessage"] = "Invalid Customer ID";                
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (TempData["CustomerMessage"] != null)
            {
                Message = TempData["CustomerMessage"].ToString();
                TempData["CustomerMessage"] = null;
            }

            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.Message = Message;
            return View(customer);
        }

        [HttpPost]
        public IActionResult InsertNewCustomer(customerparameters customer)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            hlab_customers search_customer_obj = new hlab_customers();
            hlab_test_coupon_logs couponlog = new hlab_test_coupon_logs();
            int new_customerid = 0;
            try
            {
                search_customer_obj.status = true;
                new_customerid = _customerHelper.SaveNewCustomerToDb(customer);
                TempData["CustomerMessage"] = _customerHelper.InsertNewCustomerResultMessage;

            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if (customer.save_proceed)
            {
                _sessionHelper.SetIntSearchCustomerId(Convert.ToInt32(new_customerid));
                return GoToRequestPage();
            }

            if(new_customerid!=0) return GoToEditCustomerPage(new_customerid);
            return GoToNewCustomerPage();
        }

        [HttpPost]
        public IActionResult UpdateCustomer(customerparameters customer)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page
                hlab_test_coupon_logs couponlog = new hlab_test_coupon_logs();

                foreach(var email in customer.email_list){email.customer_id = customer.hlab_customers.customer_id;}
                TempData["CustomerMessage"] = "";
                //if (customer.hlab_customers.coupon == 1) customer.hlab_customers.coupon = _customerHelper.GenerateNewCoupon();
                var result = _customerHelper.UpdateCustomerRecordChanges(customer);
                if (result)
                {
                    TempData["CustomerMessage"] = "Modifying Customer: " + customer.hlab_customers.first_name + " " + customer.hlab_customers.last_name + " was successful.";
                }
                else
                {
                    TempData["CustomerMessage"] = "Unable to update Customer: " + customer.hlab_customers.first_name + " " + customer.hlab_customers.last_name + " , Please contact administrator.";
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            if(customer.save_proceed)
            {
                HttpContext.Session.SetInt32("orderpage_customerId", Convert.ToInt32(customer.hlab_customers.customer_id));
                return RedirectToAction("OrderPage", "Order", new { customerid = customer.hlab_customers.customer_id });
            }
            ViewBag.CustomerMessage = TempData["CustomerMessage"];
            return RedirectToAction("ViewCustomerListPage", "Customer");
        }
    }
}
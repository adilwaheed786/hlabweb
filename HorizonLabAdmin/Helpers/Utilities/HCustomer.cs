using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HCustomer : ICustomer
    {
        private readonly Interface_hlab_customers _hlabCustomers;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly ITransaction _transaction;
        private readonly IUtility _utility;
        private readonly Interface_hlab_cities _city;
        private readonly Interface_hlab_provinces _province;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_hlab_test_coupon_logs _hlabCouponLog;
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly ILogger<HCustomer> _logger;
        private readonly ITestProject _projectRequestHelper;
        private readonly int _batchRecord = 100;
        private readonly int _mb_province_id = 3;

        public int rec_start { get; set; }
        public int rec_end {get;set;}
        public int rec_count { get; set; }
        public string InsertNewCustomerResultMessage { get; set; }
        
        public HCustomer(
            IHttpContextAccessor httpContextAccessor, 
            Interface_hlab_customers hlabCustomers, 
            IHorizonLabSession sessionHelper, 
            ITransaction transaction,
            Interface_hlab_test_coupon_logs hlabCouponLog,
            IUtility utility,
            Interface_hlab_cities City,
            Interface_hlab_provinces Province,
            Interface_hlab_orders hlabOrderRepo,
            ILogger<HCustomer> logger,
            Interface_test_transactions hlabTestTransRepo,
            ITestProject projectRequestHelper
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _hlabCustomers = hlabCustomers;
            _sessionHelper = sessionHelper;
            _transaction = transaction;
            _hlabCouponLog = hlabCouponLog;
            _city = City;
            _utility = utility;
            _province = Province;
            _hlabOrderRepo = hlabOrderRepo;
            _logger = logger;
            _hlabTestTransRepo = hlabTestTransRepo;
            _projectRequestHelper = projectRequestHelper;
        }

        public bool AreAllCustomerSearchParameterEmpty(hlab_customers customer)
        {
            if(customer.customer_id==0 && string.IsNullOrEmpty(customer.first_name) && string.IsNullOrEmpty(customer.last_name))
            {
                return true;
            }
            return false;
        }

        public DashboardDataView AssignDashboardCustomerSearchParameters(DashboardDataView view_data)
        {
            try
            {
                if (AreAllCustomerSearchParameterEmpty(new hlab_customers
                {
                    customer_id = view_data.search_customer_id,
                    first_name = view_data.search_customer_firstname,
                    last_name = view_data.search_customer_lastname
                }))
                {
                    view_data.search_customer_id = _sessionHelper.GetSearchCustomerId();
                    view_data.search_customer_firstname = _sessionHelper.GetSearchCustomerFirstName();
                    view_data.search_customer_lastname = _sessionHelper.GetSearchCustomerLastName();
                }
                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > AssignDashboardCustomerSearchParameters(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public int GetCustomerIdFromPagePostData(TestResultPageViewModel pagepostdata)
        {
            try
            {
                return (int)pagepostdata.trans_details.customer_id;
            }
            catch (Exception exc)
            {
                throw exc.InnerException;
            }           
        }

        public List<horizonlabcustomerview> GetFilteredCustomersList(horizonlabcustomerview customer, string sort_name="asc", string sort_town = "asc")
        {            
            try
            {
                List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
                customer_list = _hlabCustomers.GetAllCustomers(customer).ToList();

                if (sort_name == "asc") customer_list = customer_list.OrderBy(x => x.first_name).ToList();
                if (sort_name == "desc") customer_list = customer_list.OrderByDescending(x => x.first_name).ToList();
                if (sort_town == "asc") customer_list = customer_list.OrderBy(x => x.city).ToList();
                if (sort_town == "desc") customer_list = customer_list.OrderByDescending(x => x.city).ToList();
                return customer_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetFilteredCustomersList(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public bool IsCustomerSelected(int customerid = 0)
        {
            if (customerid != 0) return true;
            return false;
        }

        public TestResultPageViewModel GetCustomerCouponRecords(TestResultPageViewModel page_data, int? customer_id)
        {
            try
            {
                List<sp_getcustomercouponrecords> getcustomercoupon_list = new List<sp_getcustomercouponrecords>();
                bool selectCoupon = false;
                string coupon_issued_date = "";

                if (_transaction.IsTransactionHasNoGeneratedCoupon(page_data.trans_details))
                {
                    page_data.selectCustomerCoupon.Add(new SelectListItem { Selected = false, Text = "", Value = "" });
                }

                getcustomercoupon_list = _hlabCouponLog.GetCustomerCouponRecord(new couponrecord { customerid = customer_id ?? 0 }).Where(x => x.trans_id != 0).ToList();
                foreach (var coupon in getcustomercoupon_list)
                {
                    selectCoupon = false;
                    if (coupon.coupon == page_data.trans_details.gen_coupon) selectCoupon = true;
                    if (coupon.coupon_issued_date != null) coupon_issued_date = coupon.coupon_issued_date.Value.ToShortDateString();
                    page_data.selectCustomerCoupon.Add(new SelectListItem { Selected = selectCoupon, Text = $"{coupon.coupon} ({coupon_issued_date})", Value = $"{coupon.coupon}" });
                }
                return page_data;
            }
            catch(Exception exc)
            {
                _logger.LogError($"HCustomer > GetCustomerCouponRecords(): {exc.InnerException}");
                throw exc.InnerException;
            }

        }

        public DashboardDataView PopulateDashboardCustomerSelectItemList(DashboardDataView view_data)
        {
            try
            {
                int manitoba = 3;
                view_data.city_selectlist_item = _utility.GenerateSelectListItem(_city.GetAllCities(manitoba), "id", "city");

                view_data.truefalse_selectlist_item = _utility.GenerateTrueFalseSelectList();
                view_data.truefalse_selectlist_item = _utility.SetSelectedItemFromList(view_data.truefalse_selectlist_item, "false");

                view_data.province_selectlist_item = _utility.GenerateSelectListItem(_province.GetAllProvinces(), "id", "province");
                view_data.province_selectlist_item = _utility.SetSelectedItemFromList(view_data.province_selectlist_item, manitoba.ToString());

                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > PopulateDashboardCustomerSelectItemList(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public int GetTodaysCustomerRequestsCount(int customer_id, int test_pkg_id, string hl_code_prefix)
        {
            try
            {
                int count = 0;
                List<orderdetailsview> customer_request_list = _hlabOrderRepo.GetOrderItems(new orderdetailsview
                {
                    trans_id = 0,
                    order_id = 0,
                    order_date = DateTime.Now.Date,
                    customer_id = (int)customer_id,
                    hl_code_prefix = hl_code_prefix
                }).ToList();

                //if (customer_request_list != null && customer_request_list.Count != 0)
                //{
                //    customer_request_list = customer_request_list.Where(
                //        x => x.pkg_id == test_pkg_id).ToList();
                //}

                if (customer_request_list != null && customer_request_list.Count != 0)
                    count = customer_request_list.Count;                    
                return count;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetTodaysCustomerRequestsCount(): {exc.InnerException}");
                return 0;
            }
        }

        public int GetTodaysTotalRequestsCount(int customer_id, string hl_code_prefix)
        {
            try
            {
                int count = 1, walkin_req_count = 0, proj_req_count = 0;
                //IEnumerable<orderdetailsview> filtered_list;
                List<orderdetailsview> order_item_list = _hlabOrderRepo.GetOrderItems(
                    new orderdetailsview
                    {
                        order_date = DateTime.Now.Date,
                        trans_id = 0,
                        order_id = 0,
                        customer_id = 0,                       
                        hl_code_prefix = hl_code_prefix
                    }
                ).ToList();

                List<temporaryprojectrequestsview> proj_req_list = _projectRequestHelper.ListProjectRequestRecords(new ProjectRequestPageObject {
                    selected_project_id = 0,
                    selected_payment_type_id = 0,
                    selected_testpkg_id = 0,
                    _RequestDate = DateTime.Now.Date,
                    proj_form_id = 0
                }).ToList();

                //if (customer_id != 0) order_item_list = order_item_list.Where(x => x.customer_id != customer_id).ToList();

                //if (order_item_list != null && order_item_list.Count != 0)
                //count = order_item_list.Select(x => x.customer_id).Distinct().ToList().Count();

                if (order_item_list != null && order_item_list.Count != 0)
                {
                    walkin_req_count = order_item_list.Where(x => x.proc_status == true).GroupBy(y => y.order_id).ToList().Count;
                    proj_req_count = proj_req_list.Where(x => x.hl_code_prefix == hl_code_prefix).GroupBy(y => y.project_id).ToList().Count;
                    count = walkin_req_count + proj_req_count + 1;
                }
                //count = CountTodaysTotalRequestsCount(order_item_list, customer_id);
                                    
                return count;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetTodaysTotalRequestsCount(): {exc.InnerException}");
                return 0;
            }
        }

        private int CountTodaysTotalRequestsCount(List<orderdetailsview> order_item_list, int customer_id)
        {
            int count = 0;
            count = order_item_list.Where(x => x.customer_id == customer_id).ToList().Count;

            if (count > 0)
            {
                count = order_item_list.GroupBy(x => new { x.hl_code_prefix, x.customer_id }).ToList().Count;
            }
            else
            {
                count = order_item_list.GroupBy(x => new { x.hl_code_prefix, x.customer_id }).ToList().Count + 1;
            }

            return count;
        }

        public string GetCustomerPrimaryPhone(int customerid)
        {
            try
            {
                string primary_phone = "";
                List<hlab_customer_phone> customer_phone_list = _hlabCustomers.GetCustomerPhone(new hlab_customers { customer_id = customerid }).ToList();
                if (customer_phone_list.Count > 0)
                {
                    primary_phone = customer_phone_list.FirstOrDefault().phone;
                }
                return primary_phone;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetCustomerPrimaryPhone(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public string GetCustomerPrimaryEmail(int customerid)
        {
            try
            {
                string primary_email = "";
                List<hlab_customer_email> customer_email_list = _hlabCustomers.GetCustomerEmail(new hlab_customers { customer_id = customerid }).ToList();
                if(customer_email_list.Count > 0)
                {
                    primary_email = customer_email_list.Where(x => x.is_primary = true).FirstOrDefault().email;
                }
                return primary_email;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetCustomerPrimaryPhone(): {exc.InnerException}");
                throw exc.InnerException;
            }            
        }

        public bool IsCustomerNotEmptyAndHasRequests(OrderPageForm request_view_model)
        {
            if (
                request_view_model.hlab_order_log.customer_id != null 
                && request_view_model.hlab_order_log.customer_id != 0 
                && request_view_model.hlab_order_items.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsCustomerHasValidCoupon(int input_customerid, int input_coupon)
        {
            try
            {
                List<sp_getcustomercouponrecords> couponrec = new List<sp_getcustomercouponrecords>();
                List<testtransactionsview> transactionrecords = new List<testtransactionsview>();

                couponrec = _hlabCouponLog.GetCustomerCouponRecord(new couponrecord { customerid = input_customerid })
                    .Where(x => x.coupon == input_coupon && x.coupon_issued_date.Value.AddYears(1) >= DateTime.Now).ToList(); //check if coupon exists && if coupon is not expired
                                                                                                                                                     //couponrec = couponrec.Where(x => x.coupon_issued_date.AddYears(1) >= DateTime.Now).ToList(); //check if coupon is not expired
                transactionrecords = _hlabTestTransRepo.GetAllTransactions(new test_transaction { assigned_coupon = Convert.ToInt32(input_coupon) }).ToList(); //check if coupon was assigned to other records

                if (transactionrecords.Count == 0 && couponrec.Count != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > IsCustomerHasValidCoupon(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<horizonlabcustomerview> GetCustomersForSelectList(ordersummaryview request)
        {
            try
            {
                List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
                if (request != null)
                {
                    customer_list = GetFilteredCustomersList(new horizonlabcustomerview { customer_id = request.customer_id, status = true });
                    if (customer_list.Count > 0) customer_list[0].first_name = customer_list[0].first_name + " " + customer_list[0].last_name;
                }
                return customer_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > AssignDashboardCustomerSearchParameters(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public horizonlabcustomerview GetDbCustomerInformation(int customerid=0)
        {
            try
            {
                horizonlabcustomerview customer = new horizonlabcustomerview();
                if (customerid != 0)
                {
                    customer = _hlabCustomers.GetCustomersDetails(new hlab_customers { customer_id = customerid} );
                }
                return customer;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetDbCustomerInformation(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_customer_phone> GetCustomerPhoneListFromDb(int customerid)
        {
            try
            {
                List<hlab_customer_phone> phonelist = new List<hlab_customer_phone>();
                phonelist = _hlabCustomers.GetCustomerPhone(new hlab_customers { customer_id = customerid }).ToList();
                return phonelist;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetCustomerPhoneListFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<horizonlabcustomerview> ApplyPaginationToCustomerList(List<horizonlabcustomerview> customer_list)
        {
            try
            {               
                rec_count = customer_list.Count;
                rec_end = rec_count > _batchRecord ? _batchRecord - 1 : rec_count;
                rec_start = _sessionHelper.GetStartCustomerRecordSession();
                if(_sessionHelper.GetEndCustomerRecordSession()!=0) rec_end = _sessionHelper.GetEndCustomerRecordSession();

                if (rec_end == 0) rec_end = rec_count > _batchRecord ? _batchRecord - 1 : rec_count;
                if (rec_end > rec_count)
                {
                    customer_list = customer_list.GetRange(rec_start, rec_count - rec_start);
                    rec_end = rec_count;
                }
                else
                {
                    customer_list = customer_list.GetRange(rec_start, rec_end - rec_start);
                }
                return customer_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > ConverCustomerListToPagination(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<hlab_customer_email> GetCustomerEmailListFromDb(int customerid)
        {
            try
            {
                List<hlab_customer_email> emaillist = new List<hlab_customer_email>();
                emaillist = _hlabCustomers.GetCustomerEmail(new hlab_customers { customer_id = customerid }).ToList();
                return emaillist;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GetCustomerPhoneListFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateCustomerRecordChanges(customerparameters customer)
        {
            try
            {
                return _hlabCustomers.UpdateCustomer(customer);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > UpdateCustomerRecordChanges(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool ActivateCustomerRecord(int customerid)
        {
            try
            {
                customerparameters customparam = new customerparameters();
                bool record_status = true;
                customparam = CreateCustomerDbObject(customerid, record_status);
                return _hlabCustomers.UpdateCustomer(customparam);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > ActivateCustomerRecord(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool DeactivateCustomerRecord(int customerid)
        {
            try
            {
                return _hlabCustomers.DeleteCustomer(new hlab_customers { customer_id = customerid});
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > ActivateCustomerRecord(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public customerparameters CreateCustomerDbObject(int customerid, bool record_status)
        {
            try
            {
                horizonlabcustomerview customerview = new horizonlabcustomerview();
                customerparameters customparam = new customerparameters();
                customerview = _hlabCustomers.GetCustomersDetails(new hlab_customers { customer_id = customerid });
                customparam.hlab_customers = new hlab_customers
                {
                    customer_id = customerview.customer_id,
                    producer_number = customerview.producer_number,
                    company_name = customerview.company_name,
                    first_name = customerview.first_name,
                    last_name = customerview.last_name,
                    street = customerview.street,
                    city_id = customerview.city_id,
                    province_id = customerview.province_id,
                    postal_code = customerview.postal_code,
                    fax = customerview.fax,
                    is_public = customerview.is_public,
                    status = record_status,
                    is_semi_public = customerview.is_semi_public,
                    is_approve_financing = customerview.is_approve_financing,
                    env_distr = customerview.env_distr,
                    dw_officer = customerview.dw_officer,
                    dw_phone = customerview.dw_phone,
                    com_code = customerview.com_code,
                    //coupon = customerview.coupon,
                };
                customparam.phone_list = GetCustomerPhoneListFromDb(customerid);
                customparam.email_list = GetCustomerEmailListFromDb(customerid);
                return customparam;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > CreateCustomerDbObject(int): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public customerparameters CreateCustomerDbObject(WaterBacteriaCsvFile csv_item)
        {
            customerparameters new_customer_object = new customerparameters
            {
                hlab_customers = new hlab_customers
                {
                    first_name = csv_item.FirstName,
                    last_name = csv_item.LastName,
                    postal_code = csv_item.Postal,
                    status = true
                },
                email_list = new List<hlab_customer_email>(),
                phone_list = new List<hlab_customer_phone>()
            };
            if (!string.IsNullOrEmpty(csv_item.PrimaryEmail)) new_customer_object.email_list.Add(new hlab_customer_email { email = csv_item.PrimaryEmail, is_primary = true });
            if (!string.IsNullOrEmpty(csv_item.PrimaryPhone)) new_customer_object.phone_list.Add(new hlab_customer_phone { phone = csv_item.PrimaryPhone });

            return new_customer_object;
        }

        public customerparameters PrepareCustomerHTMLPageFieldsData(int customerid)
        {
            try
            {                
                customerparameters customer = new customerparameters();
                customerparameters customer_contact_object = new customerparameters();
                List<horizonlabcustomerview> dbcustomer_list = new List<horizonlabcustomerview>();
                int phone_list_count = 4, email_list_count = 4, selected_city_id = 0, selected_province_id = 0;
                string selected_public = "", selected_semi_publc = "", selected_approve_financing = "", selected_real_estate = "";
                if (customerid == 0) return customer;
                hlab_customers customer_obj = new hlab_customers { customer_id = customerid };

                customer.PublicSelectList = _utility.GenerateTrueFalseSelectList();
                customer.SemiPublicSelectList = _utility.GenerateTrueFalseSelectList();
                customer.ApproveFinancingSelectList = _utility.GenerateTrueFalseSelectList();
                customer.RealEstateSelectList = _utility.GenerateTrueFalseSelectList();

                dbcustomer_list = _hlabCustomers.GetAllCustomers(new horizonlabcustomerview { status = true }).ToList();
                customer.horizonlabcustomerview = _hlabCustomers.GetCustomersDetails(customer_obj);
                customer.next_customer_id = GetNextCustomerId(customerid, dbcustomer_list);
                customer.previous_customer_id = GetPreviousCustomerId(customerid, dbcustomer_list);
                customer_contact_object = PopulateCustomerEmailPhoneSelectList(customerid);
                customer.phone_list = customer_contact_object.phone_list;
                customer.email_list = customer_contact_object.email_list;                          
                                
                selected_city_id = customer.horizonlabcustomerview.city_id;
                selected_province_id = customer.horizonlabcustomerview.province_id;
                selected_public = customer.horizonlabcustomerview.is_public.ToString();
                selected_semi_publc = customer.horizonlabcustomerview.is_semi_public.ToString().ToLower();
                selected_approve_financing = customer.horizonlabcustomerview.is_approve_financing.ToString().ToLower();
                selected_real_estate = customer.horizonlabcustomerview.is_realestate.ToString().ToLower();

                customer.CitySelectList = _utility.GenerateSelectListItem(_city.GetAllCities(3).OrderBy(x => x.city).ToList(), "id", "city");
                customer.CitySelectList.Add(new SelectListItem { Selected = false, Text = "", Value = "0" });
                customer.CitySelectList = _utility.SetSelectedItemFromList(customer.CitySelectList, selected_city_id.ToString());

                customer.ProvinceSelectList = _utility.GenerateSelectListItem(_province.GetAllProvinces().ToList(), "id", "province");
                if (selected_province_id == 0) selected_province_id = _mb_province_id;
                customer.ProvinceSelectList = _utility.SetSelectedItemFromList(customer.ProvinceSelectList, selected_province_id.ToString());
                

                customer.PublicSelectList = _utility.SetSelectedItemFromList(customer.PublicSelectList, selected_public);
                customer.SemiPublicSelectList = _utility.SetSelectedItemFromList(customer.SemiPublicSelectList, selected_semi_publc);
                customer.ApproveFinancingSelectList = _utility.SetSelectedItemFromList(customer.ApproveFinancingSelectList, selected_approve_financing);
                customer.RealEstateSelectList = _utility.SetSelectedItemFromList(customer.RealEstateSelectList, selected_real_estate);

                return customer;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > PrepareCustomerHTMLPageFieldsData(int): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public customerparameters PrepareCustomerHTMLPageFieldsData()
        {
            try
            {
                customerparameters customer = new customerparameters();
                int manitoba_province_id = 3;
                
                customer.PublicSelectList = _utility.GenerateTrueFalseSelectList();
                customer.PublicSelectList = _utility.SetSelectedItemFromList(customer.PublicSelectList, "false");

                customer.SemiPublicSelectList = _utility.GenerateTrueFalseSelectList();
                customer.SemiPublicSelectList = _utility.SetSelectedItemFromList(customer.SemiPublicSelectList, "false");

                customer.ApproveFinancingSelectList = _utility.GenerateTrueFalseSelectList();
                customer.ApproveFinancingSelectList = _utility.SetSelectedItemFromList(customer.ApproveFinancingSelectList, "false");

                customer.RealEstateSelectList = _utility.GenerateTrueFalseSelectList();
                customer.RealEstateSelectList = _utility.SetSelectedItemFromList(customer.RealEstateSelectList, "false");

                customer.ProvinceSelectList = _utility.GenerateSelectListItem(_province.GetAllProvinces().ToList(), "id", "province");
                customer.ProvinceSelectList = _utility.SetSelectedItemFromList(customer.ProvinceSelectList, "3");

                customer.CitySelectList = _utility.GenerateSelectListItem(_city.GetAllCities(manitoba_province_id).OrderBy(x => x.city).ToList(), "id", "city");
                customer.CitySelectList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });
                
                return customer;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > PrepareCustomerHTMLPageFieldsData(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public customerparameters PopulateCustomerEmailPhoneSelectList(int customerid)
        {
            try
            {
                customerparameters customer = new customerparameters();
                int phone_list_count = 4, email_list_count = 4;
                if (customerid == 0) return customer;
                hlab_customers customer_obj = new hlab_customers { customer_id = customerid };

                customer.phone_list = _hlabCustomers.GetCustomerPhone(customer_obj).ToList();
                customer.email_list = _hlabCustomers.GetCustomerEmail(customer_obj).ToList();

                for (int p = 0; p <= phone_list_count; p++)
                {
                    if (p >= customer.phone_list.Count)
                    {
                        customer.phone_list.Add(new hlab_customer_phone
                        {
                            customer_id = 0,
                            id = 0,
                            phone = ""
                        });
                    }
                }

                for (int e = 0; e <= email_list_count; e++)
                {
                    if (e >= customer.email_list.Count)
                    {
                        customer.email_list.Add(new hlab_customer_email
                        {
                            customer_id = 0,
                            id = 0,
                            email = ""
                        });
                    }
                }

                return customer;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > PopulateCustomerEmailPhoneSelectList(customerid): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsCustomerEmailExists(customerparameters customer)
        {
            try
            {
                return _hlabCustomers.CheckIfEmailAssigned(customer);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > IsCustomerEmailExists(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public int SaveNewCustomerToDb(customerparameters new_customer_obj)
        {
            try
            {                
                int new_customerid = 0;
                new_customer_obj.hlab_customers.status = true;
                new_customer_obj.hlab_customers.date_registered = DateTime.Now;
                new_customerid = _hlabCustomers.AddCustomer(new_customer_obj);
                if (new_customerid != 0)
                {
                    InsertNewCustomerResultMessage = "Customer: " + new_customer_obj.hlab_customers.first_name + " " + new_customer_obj.hlab_customers.last_name + " was successfully added.";
                }
                else
                {
                    InsertNewCustomerResultMessage = "Unable to insert Customer: " + new_customer_obj.hlab_customers.first_name + " " + new_customer_obj.hlab_customers.last_name + " , Please contact administrator.";
                }
                return new_customerid;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > SaveNewCustomerToDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public int? GenerateNewCoupon()
        {
            try
            {
                return _hlabCouponLog.GenerateCoupon();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCustomer > GenerateNewCoupon(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public int GetNextCustomerId(int customerid, List<horizonlabcustomerview> dbcustomer_list)
        {            
            try
            {
                int next_customer_id = 0;
                next_customer_id = dbcustomer_list.Where(x => x.customer_id > customerid).FirstOrDefault().customer_id;
                return next_customer_id;
            }
            catch (Exception exc)
            {
                return 0;
            }
        }

        public int GetPreviousCustomerId(int customerid, List<horizonlabcustomerview> dbcustomer_list)
        {
            try
            {
                int prev_customer_id = 0;            
                prev_customer_id = dbcustomer_list.Where(x => x.customer_id < customerid).LastOrDefault().customer_id;
                return prev_customer_id;
            }
            catch (Exception exc)
            {
                return 0;
            }
        }
    }
}

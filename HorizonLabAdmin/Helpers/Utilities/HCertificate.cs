using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities.Session;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
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
    public class HCertificate : ICertificate
    {
        private readonly IUtility _utility;
        private readonly IRequest _request;
        private readonly INavigation _navigation;
        private readonly Interface_EmailSender _hlabEmailSender;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_test_transactions _hlabTestTransRepo;
        private readonly Interface_test_results _hlabTestResult;
        private readonly Interface_test_package _hlabPkgCtgry;
        private readonly Interface_hlab_customers _hlabCustomers;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_test_projects _hlabTestProject;
        private readonly ILogger<HCertificate> _logger;
        private readonly int _recordPerBatch = 100;
        private readonly int _MiscClassId = 2, _HiddenClassId = 3, _B1SubsidyId = 1, _B1NonSubsidyId = 10, _B1PA = 9, _B2=2;
        public HCertificate(
            Interface_test_transactions hlabTestTransRepo,
            Interface_test_package hlabPkgCtgry,
            Interface_test_results hlabTestResult,
            Interface_hlab_customers hlabCustomers, 
            Interface_EmailSender hlabEmailSender, 
            IHttpContextAccessor httpContextAccessor, 
            IHorizonLabSession sessionHelper,
            ILogger<HCertificate> logger,
            Interface_hlab_orders hlabOrderRepo,
            IRequest request,
            INavigation navigation,
            Interface_test_projects hlabTestProject,
            IUtility utility)
        {
            _hlabEmailSender = hlabEmailSender;
            _sessionHelper = sessionHelper;
            _utility = utility;
            _hlabTestResult = hlabTestResult;
            _hlabTestTransRepo = hlabTestTransRepo;
            _hlabCustomers = hlabCustomers;
            _hlabPkgCtgry = hlabPkgCtgry;
            _logger = logger;
            _hlabOrderRepo = hlabOrderRepo;
            _navigation = navigation;
            _request = request;
            _hlabTestProject = hlabTestProject;
        }

        public List<watercertificatesummaryview> BatchCertificateRecordList(List<watercertificatesummaryview> request_records, int rec_start = 0, int rec_end = 0)
        {
            try
            {
                int rec_count = request_records.Count;
                request_records = request_records.GetRange(rec_start, rec_end - rec_start);                
                if (rec_end > rec_count)
                {
                    request_records = request_records.GetRange(rec_start, rec_count - rec_start);
                    rec_end = rec_count;
                }
                return request_records;
            }
            catch(Exception exc)
            {
                _logger.LogError($"HCertificate > BatchCertificateRecordList(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public TestResultPageViewModel GenerateB1NSCertificate(TestResultPageViewModel page_model, List<int> request_ids)
        {
            try
            {
                foreach (var id in request_ids)
                {
                    page_model = CreateB1NSCertificateData(page_model, id);
                }

                return page_model;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > GenerateB1NSCertificate(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        private TestResultPageViewModel CreateB1NSCertificateData(TestResultPageViewModel page_model, int requestid)
        {           
            int customer_id = 0;            
            List<orderdetailsview> request_items = new List<orderdetailsview>();
            //if(page_model.b1ns_details==null && page_model.b1ns_details.Count == 0) page_model.b1ns_details = new List<b1ns_details>();
            try
            {
                request_items = _hlabOrderRepo.GetOrderItems(new orderdetailsview { order_id = requestid }).ToList();
                
                if (request_items == null &&  request_items.Count < 0) return page_model;
                request_items = request_items.Where(x => x.pkg_id == _B1SubsidyId 
                    || x.pkg_id == _B1NonSubsidyId 
                    || x.pkg_id == _B1PA
                    || x.pkg_id == _B2).ToList();

                if (request_items == null && request_items.Count < 0) return page_model;
                request_items = request_items.Where(x => x.pkg_id == page_model.selected_test_pkg_id).ToList();
                request_items = request_items.OrderBy(x => x.order_item_id).ToList();

                if (request_items == null && request_items.Count < 0) return page_model;
                foreach (var item in request_items)
                {
                    b1ns_details b1ns = new b1ns_details();
                    if (item.trans_id == 0) break;
                    b1ns.trans_details = _hlabTestTransRepo.GetTransactionDetails((int)item.trans_id);
                    b1ns.result_list = _hlabTestResult.GetAllTestResults(new testresultsview { trans_id = (int)item.trans_id }).ToList();
                    customer_id = (int)b1ns.trans_details.customer_id;
                    b1ns.customer_info = _hlabCustomers.GetCustomersDetails(new hlab_customers { customer_id = customer_id });
                    b1ns.phone_list = _hlabCustomers.GetCustomerPhone(new hlab_customers { customer_id = customer_id }).ToList();
                    b1ns.email_list = _hlabCustomers.GetCustomerEmail(new hlab_customers { customer_id = customer_id }).ToList();
                    b1ns.testpackage = _hlabPkgCtgry.GetTestPackages(1).Where(x => x.id == b1ns.trans_details.test_pkg_id).FirstOrDefault();
                    page_model.b1ns_details.Add(b1ns);
                }

                return page_model;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > CreateB1NSCertificateData(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public string GenerateB1NSCertificateRequestURL(string request_scheme, string request_host, int requestid, int test_pkg_id)
        {
            try
            {
                string signature = "", user_first_name = "", user_last_name = "";
                signature = _sessionHelper.GetSignatureImgFromSessionWhenEmpty("");
                user_first_name = _sessionHelper.GetUserFirstNameFromSessionWhenEmpty("");
                user_last_name = _sessionHelper.GetUserLastNameFromSessionWhenEmpty("");
                return $"{request_scheme}://{request_host}/Certificate/b1ns2?requestid={requestid.ToString()}&test_pkg_id={test_pkg_id.ToString()}&SignatureImage={signature}&UserFirstName={user_first_name}&UserLastName={user_last_name}";
            }
            catch(Exception exc)
            {
                _logger.LogError($"HCertificate > GenerateB1NSCertificateRequestURL(): {exc.Message}");
                throw exc.InnerException;
            }
        }
        public WaterCertificateListWithCustomerId GetCustomerCertificateWithId(int id)
        {
            try
            {
                WaterCertificateListWithCustomerId certificatelist= new WaterCertificateListWithCustomerId();
                List<watercertificatesummaryview> requestlist = new List<watercertificatesummaryview>();
                
                requestlist = _hlabOrderRepo.GetAllCertificateWithCustomerId(id);
                certificatelist.certificateList = requestlist;
                return certificatelist;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > GetCustomerCertificateWithId(): {exc.Message}");
                throw exc.InnerException;
            }
        }
        public TestTransactionSearchParameters PrepareWaterTestCertificatePageData(string filter="all")
        {
            try
            {
                TestTransactionSearchParameters output_parameter = new TestTransactionSearchParameters();
                List<watercertificatesummaryview> requestlist = new List<watercertificatesummaryview>();
                ordersearch search_request = new ordersearch();

                output_parameter.sortByList = _navigation.ListSortByItems();
                output_parameter.sortOptionList = _navigation.ListSortOptionItems();
                output_parameter.projectName = _utility.GenerateSelectListItem(_hlabTestProject.GetAllTestProjects().ToList(), "id", "project");

                if (_sessionHelper.IsTestTransactionDisplayOptionHasValue())
                {
                    search_request = _request.GenerateRequestSearchParameter();

                    requestlist = _hlabOrderRepo.GetAllWaterCertificates(search_request).Where(x => x.CountWithTrans > 0).ToList();
                    if (requestlist != null && requestlist.Count > 0)
                    {
                        if (filter == "unsent")
                        {
                            requestlist = requestlist.Where(x => string.IsNullOrEmpty(x.date_email_sent)).ToList();
                        }
                        else if (filter == "sent")
                        {
                            requestlist = requestlist.Where(x => !string.IsNullOrEmpty(x.date_email_sent)).ToList();
                        }
                    }
                    output_parameter.rec_count = requestlist.Count;

                    //identify rec start and end
                    output_parameter.rec_end = _navigation.SetRecordEnd(output_parameter.rec_count, _recordPerBatch);
                    if (_sessionHelper.IsCertStartRecordHasValue()) output_parameter.rec_start = _sessionHelper.GetStartCertificateRecordSession();
                    if (_sessionHelper.IsCertEndRecordHasValue()) output_parameter.rec_end = _sessionHelper.GetEndCertificateRecordSession();

                    if (output_parameter.rec_end == 0 || output_parameter.rec_end < _recordPerBatch) output_parameter.rec_end = output_parameter.rec_count > _recordPerBatch ? _recordPerBatch : output_parameter.rec_count;

                    if (output_parameter.rec_end == 0)
                    {
                        output_parameter.rec_end = output_parameter.rec_count > _recordPerBatch ? _recordPerBatch - 1 : output_parameter.rec_count;
                    }

                    requestlist = BatchCertificateRecordList(requestlist, output_parameter.rec_start, output_parameter.rec_end);

                    if (_sessionHelper.IsSearchSortByHasValue() && _sessionHelper.IsSearchSortByOptionHasValue())
                    {
                        requestlist = SortCertificatetRecordList(requestlist);
                    }                   
                    

                    if (_sessionHelper.IsSearchSortByHasValue())
                    {                        
                        output_parameter.sortByString = _navigation.GetSelectedItemFromSortByStringList(output_parameter.sortByList);
                        output_parameter.sortByList = _navigation.SetSelectedItemFromSortByStringList(output_parameter.sortByList);
                    }
                    
                    if (_sessionHelper.IsSearchSortByOptionHasValue())
                    {                        
                        output_parameter.sortByOption = _navigation.GetSelectedItemFromSortByOptionList(output_parameter.sortOptionList);
                        output_parameter.sortOptionList = _navigation.SetSelectedItemFromSortByOptionList(output_parameter.sortOptionList);
                    }

                }

                output_parameter.searchRequestId = search_request.order_id;
                output_parameter.watercertificatelist = requestlist;
                output_parameter.customerId = search_request.customer_id;
                output_parameter.searchRequestDateStart = search_request.start_order_date ?? DateTime.MinValue;
                output_parameter.searchRequestDateEnd = search_request.end_order_date ?? DateTime.MinValue;
                return output_parameter;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > PrepareWaterTestCertificatePageData(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<watercertificatesummaryview> SortCertificatetRecordList(List<watercertificatesummaryview> requestlist)
        {
            try
            {
                string sortByString = _sessionHelper.GetSortByValue();
                string sortByOption = _sessionHelper.GetSortByOptionValue();
                if (sortByOption == "asc")
                {
                    requestlist = requestlist.OrderBy(x => x.GetType().GetProperty(sortByString).GetValue(x, null)).ToList();
                }
                else
                {
                    requestlist = requestlist.OrderByDescending(x => x.GetType().GetProperty(sortByString).GetValue(x, null)).ToList();
                }
                return requestlist;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > SortCertificatetRecordList(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public string GenerateCouponCertificateRequestURL(string request_scheme, string request_host, int coupon)
        {
            try
            {
                return $"{request_scheme}://{request_host}/Certificate/CouponCertificate?coupons={coupon.ToString()}";
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > GenerateCouponCertificateRequestURL(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public string GenerateSubsidyImageRequestURL(string request_scheme, string request_host, string filename)
        {
            try
            {
                return $"{request_scheme}://{request_host}/scan_subsidy_forms/{filename.ToString()}";
            }
            catch (Exception exc)
            {
                _logger.LogError($"HCertificate > GenerateCouponCertificateRequestURL(): {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

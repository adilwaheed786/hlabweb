using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary;
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
    [Route("hlab_order_api")]
    [ApiController]

    public class OrderApiController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly IUtility _utilityHelper;
        private readonly IRequest _requestHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly ITestPackage _testPackageHelper;
        private readonly ICustomer _customerHelper;

        public OrderApiController(
            IConfiguration appConfig,
            ILogger<OrderController> logger,
            IHostingEnvironment env,
            ITestPackage testPackageHelper,
            IUtility utilityHelper,
            IRequest requestHelper,
            ICustomer customerHelper,
            IRequestItem requestItemHelper
            )
        {
            _env = env;
            _appConfig = appConfig;
            _logger = logger;                        
            _utilityHelper = utilityHelper;
            _requestHelper = requestHelper;
            _testPackageHelper = testPackageHelper;
            _customerHelper = customerHelper;
            _requestItemHelper = requestItemHelper;
        }

        [HttpGet("gettestpackages")]
        public List<hlab_test_pkgs> GetTestPackages(int classid)
        {
            try
            {
                List<hlab_test_pkgs> pkglist = new List<hlab_test_pkgs>();
                pkglist = _testPackageHelper.GetTestPackageListByClass(classid).ToList();
                return pkglist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpGet("gettestpackagesbycategory")]
        public List<sp_gettestpackagesbycategory> GetTestPackagesByCategory(int categoryid)
        {
            try
            {
                List<sp_gettestpackagesbycategory> pkglist = new List<sp_gettestpackagesbycategory>();
                pkglist = _testPackageHelper.GetTestPackageListByCategory(categoryid);
                return pkglist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpGet("getpackagedetails")]
        public hlab_test_pkgs GetPackageDetails(int pkgid)
        {
            try
            {
                hlab_test_pkgs package = new hlab_test_pkgs();
                package = _testPackageHelper.GetTestPackageInfo(pkgid);
                return package;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
        
        [HttpGet("getcustomerlist")]
        public List<horizonlabcustomerview> GetCustomerList(string searchvalue, string searchfilter)
        {
            try
            {
                List<horizonlabcustomerview> customer_list = new List<horizonlabcustomerview>();
                horizonlabcustomerview customer = new horizonlabcustomerview();

                if (searchfilter == "fn") customer.first_name = searchvalue;
                if (searchfilter == "ln") customer.last_name = searchvalue;
                if (searchfilter == "ci") customer.customer_id = Convert.ToInt32(searchvalue);
                customer.status = true;
                customer_list = _customerHelper.GetFilteredCustomersList(customer,"asc", "asc");
                return customer_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpGet("getorderitemdetails")]
        public List<orderdetailsview> GetOrderItemDetails(int orderid = 0)
        {
            try
            {
                List<orderdetailsview> itemlist = new List<orderdetailsview>();
                itemlist = _requestItemHelper.ListTestRequestItems(new orderdetailsview { order_id = orderid}).ToList();
                if(itemlist!=null && itemlist.Count > 0) itemlist = itemlist.OrderBy(x => x.hl_code).ToList();
                return itemlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpGet("getcustomertestrequests")]
        public List<ordersummaryview> GetCustomerRequests(int customerid = 0)
        {
            try
            {
                List<ordersummaryview> request_list = new List<ordersummaryview>();
                request_list = _requestHelper.ListAllRequests(new ordersearch { customer_id = customerid}).OrderByDescending(x => x.order_id).ToList();
                return request_list;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpGet("getcustomertestrequestdetails")]
        public ordersummaryview GetCustomerRequestDetails(int requestid = 0)
        {
            try
            {       
                ordersummaryview testrequest = new ordersummaryview();
                testrequest = _requestHelper.GetRequestInformationDetails(requestid);
                return testrequest;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        [HttpPost("savenewtestrequest")]
        public int savenewtestrequest(JsonTestRequestParametercs request)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                int requestid = _requestHelper.AddNewTestRequest(request);
                return requestid;
            }
            catch (Exception xc)
            {
                return 0;
            }
        }

        [HttpPost("updaterequestitemuid")]
        public bool updaterequestitemuid(JsonTestRequestParametercs request)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                bool result = _requestItemHelper.UpdateTestRequestItemUID(request.hlab_order_item);
                return result;
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpGet("delete_customer_request_items_transactions")]
        public bool delete_customerrequest_items_transactions(int requestid)
        {
            try
            {
                bool overall_result = false;
                if (!ModelState.IsValid) return false;
                bool request_delete_result = _requestHelper.DeleteRequest(requestid);

                if (request_delete_result)
                {
                    overall_result = true;
                    _requestItemHelper.DeleteAllRequestItems(requestid);                    
                }

                return overall_result;
            }
            catch (Exception xc)
            {
                return false;
            }
        }
    }
}
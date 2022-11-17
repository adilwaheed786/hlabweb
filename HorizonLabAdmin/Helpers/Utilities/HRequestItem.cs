using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HRequestItem : IRequestItem
    {
        private readonly IUtility _utility;
        private readonly ITestPackage _testpackage;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly ILogger<HRequestItem> _logger;

        public HRequestItem(IHorizonLabSession sessionHelper, IUtility utility, Interface_hlab_orders hlabOrderRepo, ILogger<HRequestItem> logger, ITestPackage testpackage)
        {
            _sessionHelper = sessionHelper;
            _utility = utility;
            _hlabOrderRepo = hlabOrderRepo;
            _logger = logger;
            _testpackage = testpackage;
        }

        public string InsertResult { get; set; }

        public DashboardDataView AssignDashboardRequestItemSearchParameters(DashboardDataView view_data)
        {
            try
            {
                if (view_data.search_transaction_id == 0)
                {
                    view_data.search_transaction_id = _sessionHelper.GetIntSearchWaterBacteriaTransactionId();
                }

                if (view_data.search_transaction_id != 0)
                {
                    List<orderdetailsview> request_item_list = new List<orderdetailsview>();
                    orderdetailsview request_item = new orderdetailsview();
                    ordersummaryview request = new ordersummaryview();

                    request_item_list = _hlabOrderRepo.GetOrderItems(new orderdetailsview { trans_id = view_data.search_transaction_id }).ToList();

                    if (request_item_list.Count > 0)
                    {
                        request_item = request_item_list.Where(x => x.trans_id == view_data.search_transaction_id).FirstOrDefault();
                        request = _hlabOrderRepo.GetOrderInfo(request_item.order_id);

                        view_data.search_customer_id = request.customer_id;
                        view_data.search_request_id = request_item.order_id;
                        view_data.search_customer_firstname = "";
                        view_data.search_customer_lastname = "";
                    }

                }

                return view_data;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > AssignDashboardRequestItemSearchParameters(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool DeleteAllRequestItems(int requestid)
        {
            List<orderdetailsview> request_item_list = new List<orderdetailsview>();
            try
            {
                return _hlabOrderRepo.DeleteOrderItems(requestid); ;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > DeleteAllRequestItems(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool InsertCsvRequestItemToDatabase(WaterBacteriaObject water_bacteria_csv)
        {
            try
            {
                WaterBacteriaCsvFile csv_row = water_bacteria_csv.current_csv_row;
                hlab_test_pkgs test_package = water_bacteria_csv.test_package_object.lab_package;
                if (water_bacteria_csv.test_package_object == null)
                {
                    InsertResult = $"{csv_row.InsertResult} Water Sample not inserted. Can't identify LabCode: {csv_row.LabPackage}";
                    return false;
                }
                if (test_package == null)
                {
                    //START: Add order item                    
                    if (water_bacteria_csv.proceed_csv_process)
                    {
                        _hlabOrderRepo.AddNewOrderItem(new hlab_order_items {
                            amount = test_package.price,
                            test_pkg_id = test_package.id,
                            order_id = water_bacteria_csv.request_id,
                            trans_id = water_bacteria_csv.transaction_id,
                            coupon = 0
                        });
                        InsertResult = $"{csv_row.InsertResult} Inserting Request Item was successful.";
                    }
                    else
                    {
                        InsertResult = $"{csv_row.InsertResult} Inserting Request Item cancelled.";
                    }
                    return true;
                }
                else
                {
                    
                    return false;
                }

            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > InsertCsvRequestItemToDatabase(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool IsRequestItemSelectedByUser(int requestitemid)
        {
            if (requestitemid > 0) return true;
            return false;
        }

        public List<orderdetailsview> ListTestRequestItems(orderdetailsview parameter)
        {
            List<orderdetailsview> request_item_list = new List<orderdetailsview>();
            try
            {
                request_item_list = _hlabOrderRepo.GetOrderItems(parameter).OrderBy(x => x.hl_code).ToList();
                return request_item_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > ListTestRequestItems(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateTestRequestItemDb(hlab_order_items request_item)
        {            
            try
            {
                return _hlabOrderRepo.UpdateOrderItem(request_item);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > UpdateTestRequestItemDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public string UpdateCustomerRequestItems(OrderPageForm request_view_model)
        {
            try
            {
                string OrderListPageMessage = "";
                hlab_test_pkgs pkg = new hlab_test_pkgs();
                List<hlab_test_pkgs> allPkglist = new List<hlab_test_pkgs>();
                allPkglist = _testpackage.GetAllTestPackageList();
                //remove all order items first
                _hlabOrderRepo.DeleteOrderItems(request_view_model.hlab_order_log.order_id);

                //verify if order items were deleted
                var itemcount = _hlabOrderRepo.GetOrderItems(new orderdetailsview { order_id = request_view_model.hlab_order_log.order_id }).ToList().Count;
                if (itemcount == 0) return OrderListPageMessage;

                //then add order items
                foreach (var item in request_view_model.hlab_order_items)
                {
                    if (item.test_pkg_id != null && item.test_pkg_id != 0)
                    {
                        pkg = allPkglist.Where(x => x.id == item.test_pkg_id).FirstOrDefault();
                        item.order_id = request_view_model.hlab_order_log.order_id;
                        item.trans_id = 0;
                        item.amount = pkg.price;
                        _hlabOrderRepo.AddNewOrderItem(item);
                    }
                }
                OrderListPageMessage = "success:Saving changes were successful!";
                return OrderListPageMessage;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > UpdateCustomerRequestItems(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public string GetHLCode(int requestid = 0, int requestitemid = 0)
        {
            List<orderdetailsview> request_item_list = new List<orderdetailsview>();
            try
            {
                string hl_code = "UID not defined";
                List<orderdetailsview> request_items = new List<orderdetailsview>();
                if (requestid != 0 && requestitemid != 0)
                {                    
                    request_item_list = ListTestRequestItems(new orderdetailsview { order_id = requestid });
                    request_item_list = request_item_list.Where(x => x.order_item_id == requestitemid).ToList();
                    hl_code = request_item_list.FirstOrDefault().hl_code;
                }
                return hl_code;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > GetHLCode(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public orderdetailsview GetRequestItemInfo(int request_id, int requestitem_id)
        {
            orderdetailsview request_item = new orderdetailsview();
            try
            {
                List<orderdetailsview> request_items = new List<orderdetailsview>();
                if (request_id != 0 && requestitem_id != 0)
                {
                    request_item = _hlabOrderRepo.GetOrderItems(new orderdetailsview { order_id = request_id }).Where(y => y.order_item_id == requestitem_id).FirstOrDefault();
                }
                return request_item;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > RequestItemInfo(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public List<orderdetailsview> FilterListRequestItems(List<orderdetailsview> orderitems, int test_pkg_id = 0)
        {
            try
            {
                if (orderitems.Count > 0)
                {
                    orderitems = orderitems.Where(x => x.trans_id == 0 && x.pkg_id == test_pkg_id).ToList();
                }
                return orderitems;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > FilterListRequestItems(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateTestRequestItemUID(hlab_order_items request_item)
        {
            try
            {
                return _hlabOrderRepo.UpdateOrderItemUID(request_item);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequestItem > UpdateTestRequestItemUID(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}

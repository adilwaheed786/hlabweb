using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabOrderRepository : Interface_hlab_orders
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private HorizonLabOrderLibrary _hllOrderLibrary = new HorizonLabOrderLibrary();
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;

        public HlabOrderRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
        }

        public int? AddNewOrder(hlab_order_logs log)
        {
            var result = _hllOrderLibrary.AddNewOrder(log, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(result))
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            return 0;
        }

        public bool AddNewOrderItem(hlab_order_items item)
        {
            var result = _hllOrderLibrary.AddNewOrderItem(item, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteOrder(int orderid)
        {
            var result = _hllOrderLibrary.DeleteOrder(orderid, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteOrderItems(int order_id)
        {
            var result = _hllOrderLibrary.DeleteOrderItem(order_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public IEnumerable<ordersummaryview> GetAllOrders(ordersearch log)
        {
            var jsonList = _hllOrderLibrary.GetOrderList(log, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var orderlist = JsonConvert.DeserializeObject<List<ordersummaryview>>(jsonList);
            return orderlist;
        }

        public ordersummaryview GetOrderInfo(int order_id)
        {
            var jsonList = _hllOrderLibrary.GetOrderInfo(order_id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var order = JsonConvert.DeserializeObject<ordersummaryview>(jsonList);
            return order;
        }

        public IEnumerable<orderdetailsview> GetOrderItems(orderdetailsview log)
        {
            var jsonList = _hllOrderLibrary.GetOrderItems(log, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var orderitemlist = JsonConvert.DeserializeObject<List<orderdetailsview>>(jsonList);
            return orderitemlist;
        }

        public bool UpdateOrder(hlab_order_logs log)
        {
            var result = _hllOrderLibrary.UpdateOrder(log, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool UpdateOrderItem(hlab_order_items item)
        {
            var result = _hllOrderLibrary.UpdateOrderItem(item, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public IEnumerable<watercertificatesummaryview> GetAllWaterCertificates(ordersearch log)
        {
            var jsonList = _hllOrderLibrary.GetWaterCertificateList(log, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var watercertlist = JsonConvert.DeserializeObject<List<watercertificatesummaryview>>(jsonList);
            return watercertlist;
        }

        public bool UpdateOrderItemSentEmail(hlab_order_items item)
        {
            var result = _hllOrderLibrary.UpdateOrderItemSentEmail(item, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool UpdateOrderItemUID(hlab_order_items item)
        {
            var result = _hllOrderLibrary.UpdateOrderItemUID(item, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public int? CountTodaysRequests(DateTime date_request, string hl_code_prefix)
        {
            orderdetailsview request = new orderdetailsview();
            request.order_date = date_request;
            request.hl_code_prefix = hl_code_prefix;
            var jsonList = _hllOrderLibrary.CountTodaysRequests(request, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var count = JsonConvert.DeserializeObject<int>(jsonList);
            return count;
        }

        public List<watercertificatesummaryview> GetAllCertificateWithCustomerId(int id)
        {
            var jsonList = _hllOrderLibrary.GetAllCertificateWithCustomerId(id, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var watercertlist = JsonConvert.DeserializeObject<List<watercertificatesummaryview>>(jsonList);
            return watercertlist;
        }
    }
}

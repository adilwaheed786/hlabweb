using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary
{
    public class HorizonLabOrderLibrary
    {
        private HorizonLabLibrary.WebApiLibrary _hllWebApi = new HorizonLabLibrary.WebApiLibrary();
        private string hlab_api_controller_name = "/hlab_order";

        public string GetOrderList(ordersearch order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getorderlist/", ApiKey, ApiHeader);
        }

        public string GetWaterCertificateList(ordersearch order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getvatercertificatelist/", ApiKey, ApiHeader);
        }

        public string CountTodaysRequests(orderdetailsview param, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(param);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/counttodaysrequest/", ApiKey, ApiHeader);
        }

        public string GetOrderItems(orderdetailsview order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.GetRecordsPost(dataAsString, baseUrl + hlab_api_controller_name + "/getorderlistitems/", ApiKey, ApiHeader);
        }

        public string GetOrderInfo(int orderid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/getorderdetails?orderid=" + orderid, ApiKey, ApiHeader);
        }

        public string AddNewOrder(hlab_order_logs order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.CommitPostActionWithReturn(dataAsString, baseUrl + hlab_api_controller_name + "/addneworder/", ApiKey, ApiHeader);
        }

        public string AddNewOrderItem(hlab_order_items orderitem, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(orderitem);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/addneworderitem/", ApiKey, ApiHeader);
        }

        public string UpdateOrder(hlab_order_logs order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateorder/", ApiKey, ApiHeader);
        }

        public string UpdateOrderItemSentEmail(hlab_order_items order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateorderitemsentemail/", ApiKey, ApiHeader);
        }

        public string UpdateOrderItem(hlab_order_items order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateorderitem/", ApiKey, ApiHeader);
        }

        public string UpdateOrderItemUID(hlab_order_items order, string baseUrl, string ApiKey, string ApiHeader)
        {
            var dataAsString = JsonConvert.SerializeObject(order);
            return _hllWebApi.CommitPostAction(dataAsString, baseUrl + hlab_api_controller_name + "/updateorderitemuid/", ApiKey, ApiHeader);
        }

        public string DeleteOrder(int orderid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deleteorder?orderid=" + orderid, ApiKey, ApiHeader);
        }

        public string DeleteOrderItem(int orderid, string baseUrl, string ApiKey, string ApiHeader)
        {
            return _hllWebApi.GetRecords(baseUrl + hlab_api_controller_name + "/deleteorderitems?orderid=" + orderid, ApiKey, ApiHeader);
        }
    }
}

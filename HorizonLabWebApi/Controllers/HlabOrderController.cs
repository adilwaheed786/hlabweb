using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_order")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabOrderController : ControllerBase
    {
        private Interface_hlab_orders _hlabOrders;
        private readonly ILogger<HlabOrderController> _logger;

        public HlabOrderController(Interface_hlab_orders hlabOrders, ILogger<HlabOrderController> logger)
        {
            _hlabOrders = hlabOrders;
            _logger = logger;
        }

        [HttpPost("getorderlist")]
        public List<ordersummaryview> getorderlist(ordersearch order)
        {
            try
            {
                List<ordersummaryview> orderlist = new List<ordersummaryview>();
                orderlist = _hlabOrders.GetAllOrders(order).ToList();
                return orderlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }
        [HttpGet("getallcertificatewithcustomerid")]
        public List<watercertificatesummaryview> getallcertificatewithcustomerid(int custid)
        {
            try
            {
                List<watercertificatesummaryview> certlist = new List<watercertificatesummaryview>();
                certlist = _hlabOrders.GetAllCertificateWithCustomerId(custid).ToList();
                return certlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }
        [HttpPost("getvatercertificatelist")]
        public List<watercertificatesummaryview> getvatercertificatelist(ordersearch order)
        {
            try
            {
                List<watercertificatesummaryview> certlist = new List<watercertificatesummaryview>();
                certlist = _hlabOrders.GetAllWaterCertificates(order).ToList();
                return certlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("getorderlistitems")]
        public List<orderdetailsview> getorderlistitems(orderdetailsview orderitem)
        {
            try
            {
                List<orderdetailsview> orderitemlist = new List<orderdetailsview>();
                orderitemlist = _hlabOrders.GetOrderItems(orderitem).ToList();
                return orderitemlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getorderdetails")]
        public ordersummaryview getorderdetails(int orderid)
        {
            try
            {
                ordersummaryview order = _hlabOrders.GetOrderInfo(orderid);
                return order;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("addneworder")]
        public int? addneworder(hlab_order_logs log)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                return _hlabOrders.AddNewOrder(log);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return 0;
            }
        }

        [HttpPost("addneworderitem")]
        public bool addneworderitem(hlab_order_items orderitem)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabOrders.AddNewOrderItem(orderitem);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("updateorder")]
        public bool updateorder(hlab_order_logs log)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabOrders.UpdateOrder(log);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("updateorderitem")]
        public bool updateorderitem(hlab_order_items item)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabOrders.UpdateOrderItem(item);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("updateorderitemuid")]
        public bool updateorderitemuid(hlab_order_items item)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabOrders.UpdateOrderItemUID(item);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("updateorderitemsentemail")]
        public bool updateorderitemsentemail(hlab_order_items request_item)
        {
            try
            {
                return _hlabOrders.UpdateOrderItemSentEmail(request_item);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpGet("deleteorder")]
        public bool deleteorder(int orderid)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabOrders.DeleteOrder(orderid);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpGet("deleteorderitems")]
        public bool deleteorderitems(int orderid)
        {
            try
            {
                return _hlabOrders.DeleteOrderItems(orderid);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return false;
            }
        }

        [HttpPost("counttodaysrequest")]
        public int? counttodaysrequest(orderdetailsview param)
        {
            try
            {
                int? count = 0;
                count = _hlabOrders.CountTodaysRequests(param.order_date.Value, param.hl_code_prefix);
                return count;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return 0;
            }
        }
    }
}
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_orders
    {
        IEnumerable<ordersummaryview> GetAllOrders(ordersearch log);
        List<watercertificatesummaryview> GetAllCertificateWithCustomerId(int id);

        IEnumerable<watercertificatesummaryview> GetAllWaterCertificates(ordersearch log);
        ordersummaryview GetOrderInfo(int order_id);
        IEnumerable<orderdetailsview> GetOrderItems(orderdetailsview log);        
        //IEnumerable<orderpaymentsview> GetOrderPayments(orderdetailsview log);
        int? AddNewOrder(hlab_order_logs log);
        int? CountTodaysRequests(DateTime date_request, string hl_code_prefix);
        bool AddNewOrderItem(hlab_order_items item);
        bool UpdateOrderItemSentEmail(hlab_order_items order_item);
        bool UpdateOrder(hlab_order_logs log);
        bool UpdateOrderItemUID(hlab_order_items item);
        bool UpdateOrderItem(hlab_order_items item);
        bool DeleteOrder(int orderid);
        bool DeleteOrderItems(int order_id);
    }
}

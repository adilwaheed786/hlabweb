using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IRequestItem
    {
        string InsertResult { get; set; }
        string UpdateCustomerRequestItems(OrderPageForm request_view_model);
        DashboardDataView AssignDashboardRequestItemSearchParameters(DashboardDataView view_data);
        bool IsRequestItemSelectedByUser(int requestitemid);
        bool InsertCsvRequestItemToDatabase(WaterBacteriaObject water_bacteria_obj);
        bool UpdateTestRequestItemDb(hlab_order_items request_item);
        bool UpdateTestRequestItemUID(hlab_order_items request_item);
        bool DeleteAllRequestItems(int requestid);
        List<orderdetailsview> ListTestRequestItems(orderdetailsview parameter);
        List<orderdetailsview> FilterListRequestItems(List<orderdetailsview> orderitems, int test_pkg_id = 0);
        orderdetailsview GetRequestItemInfo(int request_id, int requestitem_id);
        string GetHLCode(int requestid = 0, int requestitemid = 0);
    }
}

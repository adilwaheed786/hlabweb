using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IRequest
    {
        bool proceed_csv_process { get; set; }
        string InsertResult { get; set; }
        SelectList GenerateRequestFilterSelectList();
        ordersearch GenerateRequestSearchParameter();
        bool IsRequestIdNotEmpty(string requestid);
        bool IsRequestSelectedByUser(int requestid);
        bool IsSearchRequestSet();
        bool IsRequestInFinalStatus(ordersummaryview request);
        bool DeleteRequest(int requestid);
        bool UpdateRequestChanges(hlab_order_logs request);
        int AddNewTestRequest(JsonTestRequestParametercs request);
        int? InsertCsvTestRequestToDatabase(WaterBacteriaObject water_bacteria_csv_object);
        DashboardDataView AssignDashboardRequestSearchParameters(DashboardDataView view_data);
        orderdetailsview GetRequestInfoOfaTransaction(int transactionid);
        ordersummaryview GetRequestInformationDetails(int requestid);        
        List<ordersummaryview> ListAllRequests(ordersearch order);                                      
        hlab_order_items ConvertRequestItemObjectToDbObject(orderdetailsview request_item_object);
        hlab_order_logs ConvertRequestDbObject(ordersummaryview request);
        OrderPageForm PrepareOrderListPageData(OrderListPageParameter parameter, OrderPageForm order);        
        string UploadSubsidyImageAndSaveToDb(FileUploadParameter input_file, int transactionid);
    }
}

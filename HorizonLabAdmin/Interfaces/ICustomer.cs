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
    public interface ICustomer
    {
        int rec_start { get; set; }
        int rec_end { get; set; }
        int rec_count { get; set; }
        string InsertNewCustomerResultMessage { get; set; }

        int GetCustomerIdFromPagePostData(TestResultPageViewModel pagepostdata);
        int GetTodaysCustomerRequestsCount(int customer_id, int package_class_id, string hl_code_prefix);
        int GetTodaysTotalRequestsCount(int package_class_id, string hl_code_prefix);
        int SaveNewCustomerToDb(customerparameters new_customer_obj);
        int? GenerateNewCoupon();
        int GetNextCustomerId(int customerid, List<horizonlabcustomerview> customer_list);
        int GetPreviousCustomerId(int customerid, List<horizonlabcustomerview> customer_list);
        List<horizonlabcustomerview> GetFilteredCustomersList(horizonlabcustomerview customer, string sort_name, string sort_town);
        List<horizonlabcustomerview> GetCustomersForSelectList(ordersummaryview request);
        List<horizonlabcustomerview> ApplyPaginationToCustomerList(List<horizonlabcustomerview> customer_list);
        List<hlab_customer_phone> GetCustomerPhoneListFromDb(int customerid);
        List<hlab_customer_email> GetCustomerEmailListFromDb(int customerid);
        horizonlabcustomerview GetDbCustomerInformation(int customerid);
        bool IsCustomerSelected(int customerid = 0);
        bool IsCustomerNotEmptyAndHasRequests(OrderPageForm request_view_model);
        bool AreAllCustomerSearchParameterEmpty(hlab_customers customer);
        bool IsCustomerHasValidCoupon(int customerid, int coupon);
        bool UpdateCustomerRecordChanges(customerparameters customer);
        bool DeactivateCustomerRecord(int customerid);
        bool ActivateCustomerRecord(int customerid);
        bool IsCustomerEmailExists(customerparameters customer);
        DashboardDataView AssignDashboardCustomerSearchParameters(DashboardDataView view_data);
        TestResultPageViewModel GetCustomerCouponRecords(TestResultPageViewModel page_data, int? customer_id);
        DashboardDataView PopulateDashboardCustomerSelectItemList(DashboardDataView view_data);
        customerparameters PopulateCustomerEmailPhoneSelectList(int customerid);
        customerparameters PrepareCustomerHTMLPageFieldsData(int customerid);
        customerparameters PrepareCustomerHTMLPageFieldsData();
        customerparameters CreateCustomerDbObject(WaterBacteriaCsvFile csv_item);
        customerparameters CreateCustomerDbObject(int customerid, bool record_status);
        string GetCustomerPrimaryPhone(int customerid);
        string GetCustomerPrimaryEmail(int customerid);
    }
}

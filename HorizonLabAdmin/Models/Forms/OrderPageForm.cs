using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class OrderPageForm
    {
        public hlab_order_logs hlab_order_log { get; set; }
        public ordersummaryview request_view { get; set; }
        public List<hlab_order_logs> hlab_order_list { get; set; }
        public List<hlab_order_items> hlab_order_items { get; set; }
        public List<hlab_test_payments> hlab_test_payments { get; set; }
        public List<orderdetailsview> request_item_list { get; set; }
        public List<orderpaymentsview> request_payment_list { get; set; }
        public List<hlab_supplies> TestPackageSupplyList { get; set; }
        public List<hlab_transaction_supplies> TransactionSupplyIdList { get; set; }
        public List<testpackagesupplyview> FilteredTestPackageSupplyList { get; set; }
        public ordersearch order_search { get; set; }
        public List<SelectListItem> selectCustomerList {get;set;}
        public List<SelectListItem> selectClassList { get;set;}
        public List<SelectListItem> selectPkgList { get;set;}
        public List<SelectListItem> selectPaymentTypeList { get; set; }
        public List<SelectListItem> selectCategoryList { get; set; }
        public List<SelectListItem> selectRush { get; set; }
        public List<SelectListItem> selectConditionsMet { get; set; }
        public bool save_proceed { get; set; } 
        public int SelectedTransId { get; set; }
        public int SelectedPackageId { get; set; }
        public int SelectedRequestId { get; set; }
        public string SelectedUID { get; set; }
    }
}

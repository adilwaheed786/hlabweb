using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class DashboardDataView
    {
        public List<horizonlabcustomerview> customer_list { get; set; }
        public List<ordersummaryview> request_list { get; set; }
        public List<orderdetailsview> request_detail_list { get; set; }
        public sp_gethorizonlabtransactiondetails test_transaction { get; set; }
        public List <sp_gettestresults> test_result_list { get; set; }

        public int search_customer_id { get; set; }
        public string search_customer_firstname { get; set; }
        public string search_customer_lastname { get; set; }
        public int search_request_id { get; set; }
        public int search_transaction_id { get; set; }
        public int intDefaultPkgClassId { get; set; }

        public List<SelectListItem> customer_select_list_item { get; set; }
        public List<SelectListItem> request_select_list_item { get; set; }
        public List<SelectListItem> request_item_select_list_item { get; set; }
        public List<SelectListItem> city_selectlist_item { get; set; }
        public List<SelectListItem> province_selectlist_item { get; set; }
        public List<SelectListItem> truefalse_selectlist_item { get; set; }
        public List<SelectListItem> package_category_selectlist_item { get; set; }
        public List<SelectListItem> payment_type_selectlist_item { get; set; }
    }
}

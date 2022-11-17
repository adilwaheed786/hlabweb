using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class TestResultPageViewModel
    {
        public List<testresultsview> result_list { get; set; }
        public List<hlab_test_results> hlab_test_result_list { get; set; }
        public List<sp_gethorizonlabtransactioninvoices> hlab_invoice_list { get; set; }
        public List<orderpaymentsview> customer_order_payment_list { get; set; }

        public sp_gethorizonlabtransactiondetails trans_details { get; set; }
        public hlab_test_transactions hlab_test_transactions { get; set; }
        public hlab_test_results hlab_test_result { get; set; }
        public ordersummaryview orderview { get; set; }
        public orderdetailsview orderitemview { get; set; }
        public hlab_order_items hlab_order_items { get; set; }
        public horizonlabcustomerview customer_info { get; set; }
        public bool generate_coupon { get; set; }
        public bool remove_coupon { get; set; }
        public bool ispageframe { get; set; }
        public bool isForPageDahsboardFrame { get; set; }

        public string selected_customer_firstname { get; set; }
        public string selected_customer_lastname { get; set; }
        public string goto_viewpage { get; set; }
        public string customer_primary_phone { get; set; }
        public string customer_primary_email { get; set; }
        public int selected_customer_id { get; set; }
        public int selected_request_id { get; set; }
        public int selected_request_item_id { get; set; }
        public int selected_transaction_id { get; set; }
        public int selected_test_pkg_id { get; set; }
        public int refresh { get; set; }
                
        public IFormFile water_chem_upload { get; set; }

        public List<hlab_chem_water_results_set_a> list_waterchemresults_a { get; set; }
        public List<hlab_chem_water_results_set_b> list_waterchemresults_b { get; set; }
        public List<hlab_trace_metal_results> list_tracemetal_results { get; set; }
        public List<b1ns_details> b1ns_details { get; set; }
        public List<hlab_customer_phone> customer_phone_list { get; set; }
        public List<hlab_customer_email> customer_email_list { get; set; }

        public List<SelectListItem> isPaid { get; set; }
        public List<SelectListItem> isGoodCondition { get; set; }
        public List<SelectListItem> isFloodSample { get; set; }
        public List<SelectListItem> selectProjectList { get; set; }
        public List<SelectListItem> selectSampleTypeList { get; set; }
        public List<SelectListItem> selectTestPackageList { get; set; }
        public List<SelectListItem> selectReportTypeList { get; set; }
        public List<SelectListItem> selectMunicipalityList { get; set; }
        public List<SelectListItem> selectTestParameters { get; set; }
        public List<SelectListItem> selectUnitMeasurements { get; set; }
        public List<SelectListItem> selectPaymentOptionList { get; set; }
        public List<SelectListItem> selectPaymentTypeList { get; set; }
        public List<SelectListItem> selectReceiversList { get; set; }
        public List<SelectListItem> selectTranTypeList { get; set; }
        public List<SelectListItem> selectTestDescriptions { get; set; }
        public List<SelectListItem> selectCustomerCoupon { get; set; }
        public List<SelectListItem> selectExistence { get; set; }
        public List<SelectListItem> selectTown { get; set; }
    }
}

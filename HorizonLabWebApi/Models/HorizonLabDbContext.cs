using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HorizonLabDbContext:DbContext
    {
        public HorizonLabDbContext(DbContextOptions<HorizonLabDbContext> dboptions):base(dboptions)
        {

        }

        public DbSet<hlab_users> hlab_users { get; set; }
        public DbSet<hlab_user_access> hlab_user_access { get; set; }
        public DbSet<hlab_web_gallery> hlab_web_gallery { get; set; }
        public DbSet<hlab_web_news> hlab_web_news { get; set; }
        public DbSet<hlab_services> hlab_services { get; set; }
        public DbSet<hlab_service_details> hlab_service_details { get; set; }
        public DbSet<hlab_web_services_intro> hlab_web_services_intro { get; set; }
        public DbSet<hlab_api_keys> hlab_api_keys { get; set; }
        public DbSet<hlab_cities> hlab_cities { get; set; }
        public DbSet<hlab_provinces> hlab_provinces { get; set; }
        public DbSet<hlab_test_payment_types> hlab_test_payment_types { get; set; }
        public DbSet<hlab_test_pkgs_category> hlab_test_pkgs_category { get; set; }
        public DbSet<hlab_test_projects> hlab_test_projects { get; set; }
        public DbSet<hlab_test_report_types> hlab_test_report_types { get; set; }
        public DbSet<hlab_test_results> hlab_test_results { get; set; }
        public DbSet<hlab_test_sample_types> hlab_test_sample_types { get; set; }
        public DbSet<hlab_test_transaction_types> hlab_test_transaction_types { get; set; }
        public DbSet<hlab_test_transactions> hlab_test_transactions { get; set; }
        public DbSet<hlab_invoice> hlab_invoice { get; set; }
        public DbSet<hlab_customers> hlab_customers { get; set; }
        public DbSet<hlab_customer_phone> hlab_customer_phone { get; set; }
        public DbSet<hlab_test_pkgs> hlab_test_pkgs { get; set; }
        public DbSet<hlab_rural_municipalities> hlab_rural_municipalities { get; set; }
        public DbSet<hlab_test_params> hlab_test_params { get; set; }
        public DbSet<hlab_test_measurement_units> hlab_test_measurement_units { get; set; }
        public DbSet<hlab_email_log> hlab_email_log { get; set; }
        public DbSet<sp_gethorizonlabtransactiondetails> test_transaction_details { get; set; }
        public DbSet<sp_getdefaultpackageparameters> default_package_parameters { get; set; }
        public DbSet<sp_gettestresults> testresults { get; set; }
        public DbSet<sp_gethorizonlabtransactioninvoices> transactioninvoicelist { get; set; }
        public DbSet<sp_gettestpackagesbycategory> sp_gettestpackagesbycategory { get; set; }
        public DbSet<sp_getcustomercouponrecords> sp_getcustomercouponrecords { get; set; }
        public DbSet<sp_getsemipublicreport> sp_getsemipublicreport { get; set; }
        public DbSet<sp_getmonthlysubsidyreport> sp_getmonthlysubsidyreport { get; set; }
        public DbSet<sp_CountTodaysRequests> sp_CountTodaysRequests { get; set; }
        public DbSet<hlab_test_payment_options> hlab_test_payment_options { get; set; }
        public DbSet<hlab_test_coupon_logs> hlab_test_coupon_logs { get; set; }
        public DbSet<hlab_order_logs> hlab_order_logs { get; set; }
        public DbSet<hlab_order_items> hlab_order_items { get; set; }
        public DbSet<hlab_test_pkgs_class> hlab_test_pkgs_class { get; set; }
        public DbSet<hlab_receivers> hlab_receivers { get; set; }
        public DbSet<hlab_test_payments> hlab_test_payments { get; set; }
        public DbSet<hlab_test_default_pkg_params> hlab_test_default_pkg_params { get; set; }
        public DbSet<hlab_test_descriptions> hlab_test_descriptions { get; set; }
        public DbSet<hlab_customer_email> hlab_customer_email { get; set; }
        public DbSet<hlab_chem_water_results_set_a> hlab_chem_water_results_set_a { get; set; }
        public DbSet<hlab_chem_water_results_set_b> hlab_chem_water_results_set_b { get; set; }
        public DbSet<hlab_trace_metal_results> hlab_trace_metal_results { get; set; }
        public DbSet<hlab_test_default_parameter_category> hlab_test_default_parameter_category { get; set; }
        public DbSet<hlab_water_subsidy_form_images> hlab_water_subsidy_form_images { get; set; }
        public DbSet<hlab_email_file_attachments> hlab_email_file_attachments { get; set; }
        public DbSet<hlab_email_templates> hlab_email_templates { get; set; }
        public DbSet<hlab_supplies> hlab_supplies { get; set; }
        public DbSet<hlab_test_proj_supplies> hlab_test_proj_supplies { get; set; }
        public DbSet<hlab_test_pkgs_forms> hlab_test_pkgs_forms { get; set; }
        public DbSet<hlab_test_pkg_supplies> hlab_test_pkg_supplies { get; set; }
        public DbSet<hlab_transaction_supplies> hlab_transaction_supplies { get; set; }
        public DbSet<hlab_temp_requests> hlab_temp_requests { get; set; }
        public DbSet<hlab_test_project_forms> hlab_test_project_forms { get; set; }

        public DbQuery<semipublicview> semipublicview { get; set; }
        public DbQuery<projectrequestsformview> projectrequestsformview { get; set; }
        public DbQuery<temporaryprojectrequestsview> temporaryprojectrequestsview { get; set; }
        public DbQuery<projectrequestsupplyview> projectrequestsupplyview { get; set; }
        public DbQuery<testrequestsupplyview> testrequestsupplyview { get; set; }
        public DbQuery<testtransactionsview> testtransactionsview { get; set; }
        public DbQuery<testpackagesupplyview> testpackagesupplyview { get; set; }
        public DbQuery<customercouponview> customercouponview { get; set; }
        public DbQuery<testresultsview> testresultsview { get; set; }
        public DbQuery<horizonlabcustomerview> horizonlabcustomerview { get; set; }
        public DbQuery<ordersummaryview> ordersummaryview { get; set; }
        public DbQuery<watercertificatesummaryview> watercertificatesummaryview { get; set; }
        public DbQuery<orderdetailsview> orderdetailsview { get; set; }
        public DbQuery<orderpaymentsview> orderpaymentsview { get; set; }
        public DbQuery<emaillogview> emaillogview { get; set; }

    }
}

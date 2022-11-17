using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class TestTransactionSearchParameters
    {
        public string display { get; set; }
        public int searchTransId { get; set; }
        public int searchRequestId { get; set; }
        public int customerId { get; set; }
        public string searchCustomerFirstName { get; set; }
        public string searchCustomerLastName { get; set; }
        public string str_sortBy { get; set; }
        public string str_sortOption { get; set; }
        public string filter { get; set; }
        public List<SelectListItem> projectName { get; set; }
       
        public int searchPackage { get; set; }
        public int searchPackageClassId { get; set; }
        public int rec_start { get; set; }
        public int rec_end { get; set; }
        public int rec_count { get; set; }
        public int pkg_class_id { get; set; }

        public DateTime searchSubmtDateStart { get; set; }
        public DateTime searchSubmtDateEnd { get; set; }
        public DateTime searchRcvdDateStart { get; set; }
        public DateTime searchRcvdDateEnd { get; set; }
        public DateTime searchTestDateStart { get; set; }
        public DateTime searchTestDateEnd { get; set; }
        public DateTime searchRequestDateStart { get; set; }
        public DateTime searchRequestDateEnd { get; set; }
        public DateTime searchProjectDateStart { get; set; }
        public DateTime searchProjectDateEnd { get; set; }
        public string str_searchProjectDateStart { get; set; }
        public string str_searchProjectDateEnd { get; set; }
        public string str_searchSubmtDateStart { get; set; }
        public string str_searchSubmtDateEnd { get; set; }
        public string str_searchRcvdDateStart { get; set; }
        public string str_searchRcvdDateEnd { get; set; }
        public string str_searchTestDateStart { get; set; }
        public string str_searchTestDateEnd { get; set; }
        public string str_searchRequestDateStart { get; set; }
        public string str_searchRequestDateEnd { get; set; }
        public string request_scheme { get; set; }
        public string request_host { get; set; }

        public int email_template_id { get; set; }
        public int selectprojectid { get; set; }
        public string sortByString { get; set; }
        public string sortByOption { get; set; } //asc or desc
        public List<testtransactionsview> transList { get; set; }
        public List<sp_getsemipublicreport> semipublic_transaction_list { get; set; }
        public List<sp_getmonthlysubsidyreport> monthly_subsidy_transaction_list { get; set; }
        public List<sp_gethorizonlabtransactiondetails> sp_transaction_details{ get; set; }
        public List<ordersummaryview> requestlist { get; set; }
        public List<watercertificatesummaryview> watercertificatelist { get; set; }
        public List<orderdetailsview> requestitemlist { get; set; }

        public List<SelectListItem> selectPackageList { get; set; }
        public List<SelectListItem> sortByList { get; set; }
        public List<SelectListItem> sortOptionList { get; set; }
    }
}

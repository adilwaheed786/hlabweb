using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class F054Data
    {
        public List<testrequestsupplyview> RequestSupplyList { get; set; }
        public testrequestsupplyview ColilertSupply { get; set; }
        public testrequestsupplyview ComparatorSupply { get; set; }
        public testrequestsupplyview Simplate { get; set; }
        public sp_gethorizonlabtransactiondetails TestTransaction { get; set; }
        public ordersummaryview RequestInformation { get; set; }
        public List<orderdetailsview> RequestDetailList { get; set; }
        public bool is_condition_met { get; set; }
        public bool is_rush { get; set; }
        public string hl_code { get; set; }
    }
}

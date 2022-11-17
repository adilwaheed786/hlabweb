using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class b1ns_details
    {
        public sp_gethorizonlabtransactiondetails trans_details { get; set; }
        public List<testresultsview> result_list { get; set; }
        public horizonlabcustomerview customer_info { get; set; }
        public List<hlab_customer_phone> phone_list { get; set; }
        public List<hlab_customer_email> email_list { get; set; }
        public hlab_test_pkgs testpackage { get; set; }
    }
}

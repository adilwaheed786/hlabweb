using HorizonLabAdmin.Helpers.Containers;
using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class ViewBagValues
    {
        public HorizonLabMenu hlab_menu { get; set; }
        public hlab_test_pkgs test_packages { get; set; }
        public string signature_image_file_name { get; set; }
        public string user_first_name { get; set; }
        public string user_last_name { get; set; }
        List<hlab_customer_phone> customer_phone_list { get; set; }
        List<hlab_customer_email> customer_email_list { get; set; }
    }
}

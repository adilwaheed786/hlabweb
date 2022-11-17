using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class TestPackageObject
    {
        public hlab_test_pkgs lab_package { get; set; }
        public List<hlab_test_pkgs> lab_package_list { get; set; }
        public int lab_package_id;
        public decimal? lab_package_fee;
    }
}

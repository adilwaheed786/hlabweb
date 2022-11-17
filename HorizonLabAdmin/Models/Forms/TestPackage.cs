using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HorizonLabAdmin.Models.Forms
{
    public class TestPackage
    {
        public List<hlab_test_pkgs_category> test_categories { get; set;}
        public List<sp_gettestpackagesbycategory> packagelist_bycategory { get; set; }
        public hlab_test_pkgs_category test_category { get; set; }
        public hlab_test_pkgs test_package { get; set; }
        public List<hlab_test_pkgs> test_packages { get; set; }
        public List<SelectListItem> selectTestPackageForm { get; set; }
    }
}

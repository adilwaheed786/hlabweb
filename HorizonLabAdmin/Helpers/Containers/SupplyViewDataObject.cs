using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class SupplyViewDataObject
    {
        public List<hlab_test_pkgs> TestPackageList { get; set; }
        public List<hlab_supplies> TestPackageSupplyList { get; set; }
        public List<testpackagesupplyview> AssignedTestPakcageSupplyList { get; set; }
        public List<SelectListItem> TestPackageSelectItemList { get; set; }
        public List<SelectListItem> SupplyStatusSelectItemList { get; set; }
        public int SelectedTestPackage_id { get; set; }
        public int SelectedSupply_id { get; set; }
        public string SelectedTestPackage { get; set; }
        public hlab_supplies hlab_supplies { get; set; }
    }
}

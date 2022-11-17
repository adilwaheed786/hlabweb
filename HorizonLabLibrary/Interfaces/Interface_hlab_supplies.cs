using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_supplies
    {
        IEnumerable<hlab_supplies> GetAllTestPackageSupplies();
        IEnumerable<testpackagesupplyview> GetFilteredTestPackageSupplies(testpackagesupplyview object_parameter);
        bool AddNewSupply(hlab_supplies object_parameter);
        bool UpdateSupply(hlab_supplies object_parameter);
        bool DeleteTestPackageSupplies(int test_pkg_id);
        bool DeleteSupply(int supplyid);
        bool AddTestPackageSupplies(test_pkg_supply_param parameter);
    }
}

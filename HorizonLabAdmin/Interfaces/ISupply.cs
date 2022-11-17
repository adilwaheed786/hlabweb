using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ISupply
    {
        List<hlab_supplies> GetAllTestPackageSupplies();
        List<testpackagesupplyview> GetFilteredTestPackageSupplies(testpackagesupplyview supply);
        bool SaveNewSupply(hlab_supplies supply);
        bool UpdateSupplyChanges(hlab_supplies supply);
        bool AssignTestPackageSupplyList(test_pkg_supply_param supply_param);
        bool DeleteSupply(int supplyid);
        bool DeleteTestPackage(int supplyid);
    }
}

using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ITestProjectSupply
    {
        bool AddProjectSupplies(project_supply_form param);
        bool DeleteProjectSupplies(int proj_form_id);
    }
}

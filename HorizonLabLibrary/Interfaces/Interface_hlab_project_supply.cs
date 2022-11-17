using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_project_supply
    {
        bool AddProjectSupplies(project_supply_form param);
        bool DeleteProjectSupplies(int proj_form_id);
    }
}

using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IDashboard
    {
        DashboardDataView GenerateDashboardViewDataObject(DashboardDataView view_data);      
        void SetSearchDashboardValuesToSession(DashboardDataView view_data);
        DashboardDataView GenerateHTMLDropDownItems(DashboardDataView view_data);
        DashboardDataView AssignDashboardSelectedSearchValues(DashboardDataView data_view);
    }
}

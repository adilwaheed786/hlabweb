using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_services
    {
        IEnumerable<hlab_services> GetActiveServices();
        List<hlab_web_services_intro> GetServiceHeader();
        hlab_services GetServiceInfo(int id);        

        int AddNewService(hlab_services service_info);
        bool UpdateService(hlab_services service_info);
        bool DeleteService(hlab_services service_info);
        bool UpdateServiceHeader(IEnumerable<hlab_web_services_intro> introList);
    }
}

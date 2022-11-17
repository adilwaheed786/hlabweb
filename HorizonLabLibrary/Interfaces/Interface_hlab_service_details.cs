using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_service_details
    {
        IEnumerable<hlab_service_details> GetAllServiceDetails(int service_id);
        hlab_service_details GetServiceDetailInfo(int id);

        bool AddNewServiceDetail(hlab_service_details service_info);
        bool UpdateServiceDetail(hlab_service_details service_info);
        bool DeleteServiceDetail(hlab_service_details service_info);
    }
}

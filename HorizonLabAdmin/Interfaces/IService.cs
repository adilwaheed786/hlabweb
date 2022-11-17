using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IService
    {
        string _ServiceMessage { get; set; }
        string _ImageName { get; set; }
        bool _IsOkToProceed { get; set; }

        ServiceForm PrepareServiceHomePageData();
        hlab_services GetServiceInfoFromDb(int service_id);
        hlab_service_details GetServiceDetailFromDb(int detail_id);
        bool DeleteServiceFromDb(int service_id);
        bool DeleteServiceDetailFromDb(int detail_id);
        bool UpdateServiceHeaderDb(List<hlab_web_services_intro> header_list);
        bool UpdateServiceDb(hlab_services service, IFormFile image_file);
        bool UpdateServiceDetailDb(int detail_id, string detail);
        bool AddNewServiceDetailToDb(int service_id, string detail);
        bool AddNewServiceToDb(ServiceForm serviceform);
        string GetImageDb(int id);
        bool CheckIfImageIsJpgPng(string modal_service_name, IFormFile image_file);
    }
}

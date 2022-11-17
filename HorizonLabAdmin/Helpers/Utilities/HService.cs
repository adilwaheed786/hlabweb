using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HService : IService
    {
        public string _ServiceMessage { get; set; }
        public string _ImageName { get; set; }
        public bool _IsOkToProceed { get; set; }

        private readonly ILogger<HService> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IUtility _utility;
        private readonly Interface_hlab_services _hlabServiceRepo;
        private readonly Interface_hlab_service_details _hlabServiceDetailRepo;


        public HService(
            Interface_hlab_services hlabServiceRepo, 
            Interface_hlab_service_details hlabServiceDetailRepo,
            IUtility utility,
            IHostingEnvironment env,
            ILogger<HService> logger)
        {
            _hlabServiceRepo = hlabServiceRepo;
            _hlabServiceDetailRepo = hlabServiceDetailRepo;
            _logger = logger;
            _utility = utility;
            _env = env;
        }

        public string GetImageDb(int id)
        {
            try
            {
                return _hlabServiceRepo.GetServiceInfo(id).image_file_name;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > GetImageDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public ServiceForm PrepareServiceHomePageData()
        {
            try
            {
                List<hlab_services> service_list = new List<hlab_services>();
                List<hlab_service_details> detail_list = new List<hlab_service_details>();
                ServiceForm form = new ServiceForm();

                form.header_list = _hlabServiceRepo.GetServiceHeader();
                service_list = _hlabServiceRepo.GetActiveServices().ToList();
                form.service_list = new List<Service>();
                foreach (var svc in service_list)
                {
                    detail_list = _hlabServiceDetailRepo.GetAllServiceDetails(svc.id).ToList();
                    form.service_list.Add(new Service
                    {
                        service = svc,
                        detail_list = new List<hlab_service_details>(detail_list)
                    });
                }
                return form;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > PrepareServiceHomePageData(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateServiceDb(hlab_services service, IFormFile image_file)
        {
            try
            {
                bool IsUpdateSuccessful = true;
                string savepath = _env.WebRootPath + "\\images";
                //if image file is blank assign the current image file 
                if (string.IsNullOrEmpty(service.image_file_name)) service.image_file_name = GetImageDb(service.id);
                IsUpdateSuccessful = _hlabServiceRepo.UpdateService(service);
                _ServiceMessage = "Error:Saving Service failed, please contact administrator!";

                if (IsUpdateSuccessful)
                {
                    _ServiceMessage = "Success:Saving service " + service.service_name + "was successful!";
                    _utility.UploadFile(new FileUploadParameter
                    {
                        file = image_file,
                        save_path = savepath + "\\" + service.image_file_name
                    });
                }
                return IsUpdateSuccessful;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > UpdateServiceDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateServiceHeaderDb(List<hlab_web_services_intro> header_list)
        {
            try
            {
                return _hlabServiceRepo.UpdateServiceHeader(header_list);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > UpdateServiceHeaderDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool CheckIfImageIsJpgPng(string modal_service_name, IFormFile image_file)
        {
            try
            {
                string png_file_extension = ".png";
                string jpg_file_extension = ".jpg";
                bool IsJpgFormat = true, IsPngFormat = true;
                _IsOkToProceed = true;
                _ServiceMessage = "";
                _ImageName = "";

                if (image_file == null) return false;
                
                IsPngFormat = _utility.IsFileOnRequiredFormat(image_file, png_file_extension);
                IsJpgFormat = _utility.IsFileOnRequiredFormat(image_file, jpg_file_extension);                

                if (!IsPngFormat && !IsJpgFormat)
                {
                    _ImageName = _utility.GetFileNameFromFormFile(image_file);
                    _IsOkToProceed = false;
                    _ServiceMessage = "Error:" + modal_service_name + " image icon file should be in .png or .jpg format!";
                    return false;
                }
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > CheckIfImageIsJpgPng(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public hlab_services GetServiceInfoFromDb(int service_id)
        {
            try
            {
                return _hlabServiceRepo.GetServiceInfo(service_id);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > GetServiceInfoFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool DeleteServiceFromDb(int service_id)
        {
            try
            {
                hlab_services service = _hlabServiceRepo.GetServiceInfo(service_id);
                return _hlabServiceRepo.DeleteService(service);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > DeleteServiceFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool AddNewServiceDetailToDb(int service_id, string detail)
        {
            try
            {
                hlab_service_details service_item = new hlab_service_details
                {
                    description = "",
                    service_detail = detail,
                    service_id = service_id
                };

                if (_hlabServiceDetailRepo.AddNewServiceDetail(service_item)) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > AddNewServiceDetailToDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool UpdateServiceDetailDb(int detail_id, string detail)
        {
            try
            {
                hlab_service_details service_detail = new hlab_service_details
                {
                    id = detail_id,
                    description = "",
                    service_detail = detail,
                    service_id = _hlabServiceDetailRepo.GetServiceDetailInfo(detail_id).service_id
                };
                
                if (_hlabServiceDetailRepo.UpdateServiceDetail(service_detail)) return true;
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > UpdateServiceDetailDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public hlab_service_details GetServiceDetailFromDb(int detail_id)
        {
            try
            {
                hlab_service_details detail = _hlabServiceDetailRepo.GetServiceDetailInfo(detail_id);
                return detail;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > GetServiceDetailFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool DeleteServiceDetailFromDb(int detail_id)
        {            
            try
            {
                hlab_service_details detail = _hlabServiceDetailRepo.GetServiceDetailInfo(detail_id);
                return _hlabServiceDetailRepo.DeleteServiceDetail(detail);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > GetServiceDetailFromDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }

        public bool AddNewServiceToDb(ServiceForm serviceform)
        {
            try
            {
                int new_service_id = 0;
                bool service_detail_add_result = true;
                string savepath = _env.WebRootPath + "\\images";
                string ImageName = _utility.GetFileNameFromFormFile(serviceform.new_service_icon);

                if (string.IsNullOrEmpty(serviceform.new_service_name))
                {
                    _ServiceMessage = "Error: Service name is empty!";
                    return false;
                }

                new_service_id = _hlabServiceRepo.AddNewService(
                        new hlab_services
                        {
                            service_name = serviceform.new_service_name,
                            description = "",
                            status = true,
                            image_file_name = ImageName
                        }
                    );

                if (serviceform.new_service_icon != null && new_service_id!=0)//save image file
                {
                    _utility.UploadFile(new FileUploadParameter
                    {
                        file = serviceform.new_service_icon,
                        save_path = savepath + "\\" + ImageName
                    });
                }

                if (new_service_id == 0)
                {
                    _ServiceMessage = "Error:" + serviceform.new_service_name + " service items failed to save in the database, service id returns 0!";
                    return false;
                }

                foreach (var detail in serviceform.service_object_list)
                {
                    if (string.IsNullOrEmpty(detail.service_detail)) break;
                    service_detail_add_result = true;
                    detail.service_id = new_service_id;
                    service_detail_add_result = _hlabServiceDetailRepo.AddNewServiceDetail(detail);
                    if (!service_detail_add_result)
                    {
                        _ServiceMessage = "Error:" + detail.service_detail + " failed to save to database!";
                    }
                }
                _ServiceMessage = "Success:" + serviceform.new_service_name + " was successfully added!";
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HService > AddNewServiceToDb(): {exc.InnerException}");
                throw exc.InnerException;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HorizonLabAdmin.Controllers
{
    public class ServicesController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _appConfig;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IService _serviceHelper;
        private readonly IUtility _utilityHelper;

        public ServicesController(
            IConfiguration appConfig, 
            IHostingEnvironment env,
            IHorizonLabSession sessionHelper,
            IService serviceHelper,
            IUtility utilityHelper)
        {
            _env = env;
            _appConfig = appConfig;
            _sessionHelper = sessionHelper;
            _serviceHelper = serviceHelper;
            _utilityHelper = utilityHelper;
        }

        public IActionResult Index()
        {
            List<hlab_services> service_list = new List<hlab_services>();
            List<hlab_service_details> detail_list = new List<hlab_service_details>();

            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "web");
            ServiceForm form = new ServiceForm();            

            form = _serviceHelper.PrepareServiceHomePageData();

            ViewBag.menu = _hlabMenu;
            ViewBag.ServiceHeaderMessage = "";
            ViewBag.ServiceMessage = "";
            ViewBag.DirImage = _appConfig["AppSettings:Dir_image"];
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            if (TempData["ServiceHeaderMessage"] != null) ViewBag.ServiceHeaderMessage = TempData["ServiceHeaderMessage"].ToString();
            if (TempData["ServiceMessage"] != null) ViewBag.ServiceMessage = TempData["ServiceMessage"].ToString();
            return View(form);
        }

        [HttpPost]
        public IActionResult SaveIntro(ServiceForm form)
        {
            if (_sessionHelper.IsUserNotLoggedIn())
            {
                return GoToMainPage();
            }
            else
            {
                form.header_list = new List<hlab_web_services_intro> {
                    new hlab_web_services_intro { id = Convert.ToInt32(form.id_1), content = form.line_1 },
                    new hlab_web_services_intro { id = Convert.ToInt32(form.id_2), content = form.line_2 },
                    new hlab_web_services_intro { id = Convert.ToInt32(form.id_3), content = form.line_3 },
                    new hlab_web_services_intro { id = Convert.ToInt32(form.id_4), content = form.line_4 },
                    new hlab_web_services_intro { id = Convert.ToInt32(form.id_5), content = form.line_5 }
                };

                if (_serviceHelper.UpdateServiceHeaderDb(form.header_list))
                {
                    TempData["ServiceHeaderMessage"] = "Changes to the \"Service Header\" were successfully saved!";
                }
                return GoToServiceHomePage();
            }            
        }

        [HttpPost]
        public IActionResult SaveServiceChanges(string modal_service_id, string modal_service_name,  IFormFile modal_image_file)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            bool proceed = true, IsUpdateSuccessful = true;
            string ServiceMessage = "";
            _serviceHelper.CheckIfImageIsJpgPng(modal_service_name, modal_image_file);
            proceed = _serviceHelper._IsOkToProceed;
            ServiceMessage = _serviceHelper._ServiceMessage;

            if (proceed == true)
            {
                hlab_services service = new hlab_services
                {
                    id = Convert.ToInt32(modal_service_id),
                    service_name = modal_service_name,
                    image_file_name = _serviceHelper._ImageName,
                    status = true
                };

                //if image file is blank assign the current image file 
                if (string.IsNullOrEmpty(service.image_file_name)) service.image_file_name = _serviceHelper.GetImageDb(service.id);
                IsUpdateSuccessful = _serviceHelper.UpdateServiceDb(service, modal_image_file);                
                if (IsUpdateSuccessful) ServiceMessage = _serviceHelper._ServiceMessage;
            }

            if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
            return GoToServiceHomePage();
        }

        [HttpPost]
        public IActionResult AddNewService(ServiceForm serviceform)
        {
            string ServiceMessage = "";
            bool service_detail_add_result = false, proceed = true;
            int service_add_result = 0;

            _serviceHelper.CheckIfImageIsJpgPng(serviceform.new_service_name, serviceform.new_service_icon);
            proceed = _serviceHelper._IsOkToProceed;
            ServiceMessage = _serviceHelper._ServiceMessage;

            if (proceed == true)
            {
                _serviceHelper.AddNewServiceToDb(serviceform);
                ServiceMessage = _serviceHelper._ServiceMessage;
            }
          
            if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
            return GoToServiceHomePage();
        }

        [HttpPost]
        public IActionResult DeleteService(string modal_service_id)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            string ServiceMessage = "Error:service record id is missing";
            bool IsDeleteSuccessful = true;
            int int_service_id = 0;
            if (!string.IsNullOrEmpty(modal_service_id))
            {
                int_service_id = Convert.ToInt32(modal_service_id);
                hlab_services service = _serviceHelper.GetServiceInfoFromDb(int_service_id);
                IsDeleteSuccessful = _serviceHelper.DeleteServiceFromDb(int_service_id);

                ServiceMessage = "Error:Deleting service " + service.service_name + " failed, please contact administrator!";
                if (IsDeleteSuccessful) ServiceMessage = "Success:Deleting service " + service.service_name + " was successful!";
            }
            if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
            return RedirectToAction("Index", "Services");
        }

        [HttpPost]
        public IActionResult AddNewServiceDetail(string service_id, string detail)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            string ServiceMessage = "";
            int int_service_id = 0;
            bool IsInsertSuccessful = true;

            if (string.IsNullOrEmpty(detail) || string.IsNullOrEmpty(service_id))
            {
                ServiceMessage = "Error:Can't save null service item!";
                if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
                return GoToServiceHomePage();
            }

            int_service_id = Convert.ToInt32(service_id);
            IsInsertSuccessful = _serviceHelper.AddNewServiceDetailToDb(int_service_id, detail);

            if (IsInsertSuccessful) ServiceMessage = "Success: New service item was added!";

            if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
            return GoToServiceHomePage();
        }

        [HttpPost]
        public IActionResult SaveServiceDetailChanges(string detail_id, string detail)
        {
            string ServiceMessage = "SaveServiceDetailChanges Error:code";
            bool IsUpdateSuccess = true;
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            if (string.IsNullOrEmpty(detail) || string.IsNullOrEmpty(detail_id))
            {
                ServiceMessage = "Error:Can't save null service detail item!";
                if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
                return GoToServiceHomePage();
            }

            int int_detail_id = Convert.ToInt32(detail_id);
            IsUpdateSuccess = _serviceHelper.UpdateServiceDetailDb(int_detail_id, detail);
            ServiceMessage = "Error:Saving service " + detail + " failed!";
            if (IsUpdateSuccess) ServiceMessage = "Success:Saving service " + detail + " was successful!";
            return GoToServiceHomePage();
        }

        [HttpPost]
        public IActionResult DeleteServiceDetail(string detail_id)
        {
            string ServiceMessage = "Error:service record id is missing";
            bool IsDeleteSuccess = true;
            int int_detail_id = 0;
            if (!string.IsNullOrEmpty(detail_id))
            {
                int_detail_id = Convert.ToInt32(detail_id);
                IsDeleteSuccess = _serviceHelper.DeleteServiceDetailFromDb(int_detail_id);
                hlab_service_details detail = _serviceHelper.GetServiceDetailFromDb(int_detail_id);
                ServiceMessage = "Error:Deleting sub-service " + detail.service_detail + " failed, please contact administrator!";
                if (IsDeleteSuccess) ServiceMessage = "Success:Deleting sub-service " + detail.service_detail + " was successful!";
            }
            if (!string.IsNullOrEmpty(ServiceMessage)) TempData["ServiceMessage"] = ServiceMessage;
            return RedirectToAction("Index", "Services");
        }
    }
}
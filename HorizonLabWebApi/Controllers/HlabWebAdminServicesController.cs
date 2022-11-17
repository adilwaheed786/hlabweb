using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Cors;

namespace HorizonLabWebApi.Controllers
{    
    [ApiController, Route("hlab_services"), ServiceFilter(typeof(APIKeyHandlers))]
    public class HlabWebAdminServicesController : ControllerBase
    {
        private Interface_hlab_services _hlabServiceRepo;
        private Interface_hlab_service_details _hlabServiceDetailsRepo;

        public HlabWebAdminServicesController(Interface_hlab_services hlabServiceRepo, Interface_hlab_service_details hlabServiceDetailsRepo)
        {
            _hlabServiceRepo = hlabServiceRepo;
            _hlabServiceDetailsRepo = hlabServiceDetailsRepo;
        }

        //Service
        [HttpPost("addnewhorizonlabservicepost")]
        public string AddNewHorizonLabServicePost(hlab_services service)
        {
            try
            {
                if (!ModelState.IsValid) return "Not a valid model";
                if (string.IsNullOrEmpty(service.service_name)) return "Can't add service with name has no value";
                int new_service_id = _hlabServiceRepo.AddNewService(service);
                if (new_service_id != 0) return new_service_id.ToString();
                return "AddNewHorizonLabServicePost Code Error";
            }
            catch (Exception xc)
            {
                return "AddNewHorizonLabServicePost Exception Error: " + xc;
            }            
        }

        [HttpPost("commithorizonlabservicechanges")]
        public ActionResult CommitHorizonLabServiceChanges(hlab_services service)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (string.IsNullOrEmpty(service.service_name)) return BadRequest("Can't update service with name has no value");
                if (_hlabServiceRepo.UpdateService(service)) return Ok();
                return BadRequest("CommitHorizonLabServiceChanges : Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("CommitHorizonLabServiceChanges Exception Error: " + xc);
            }
        }

        [HttpPost]
        public ActionResult DeleteHorizonLabServiceChanges(hlab_services service)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (string.IsNullOrEmpty(service.service_name)) return BadRequest("Can't update service with name has no value");
                if (_hlabServiceRepo.DeleteService(service)) return Ok();
                return BadRequest("CommitHorizonLabServiceChanges : Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("CommitHorizonLabServiceChanges Exception Error: " + xc);
            }
        }

        [HttpGet("getallservices")]
        public IEnumerable<hlab_services> getallservices()
        {
            IEnumerable<hlab_services> serviceList = _hlabServiceRepo.GetActiveServices();
            return serviceList;
        }

        [HttpGet("getallactiveservices")]
        public IEnumerable<hlab_services> getallactiveservices()
        {
            IEnumerable<hlab_services> serviceList = _hlabServiceRepo.GetActiveServices().Where(x => x.status==true).ToList();
            return serviceList;
        }

        //Service Details
        [HttpPost("addnewservicedetailpost")]
        public ActionResult AddNewServiceDetailPost(hlab_service_details detail)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (_hlabServiceDetailsRepo.AddNewServiceDetail(detail)) return Ok();
                return BadRequest("AddNewServiceDetailPost Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddNewServiceDetailPost Exception Error: " + xc);
            }
        }

        [HttpPost("commitservicedetailchanges")]
        public ActionResult CommitServiceDetailChanges(hlab_service_details detail)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (_hlabServiceDetailsRepo.UpdateServiceDetail(detail)) return Ok();
                return BadRequest("CommitServiceDetailChanges : Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("CommitServiceDetailChanges Exception Error: " + xc);
            }
        }

        [HttpPost("deleteservicedetail")]
        public ActionResult DeleteServiceDetail(hlab_service_details detail)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (_hlabServiceDetailsRepo.DeleteServiceDetail(detail)) return Ok();
                return BadRequest("DeleteServiceDetail : Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("DeleteServiceDetail Exception Error: " + xc);
            }
        }

        [HttpGet("getallservicedetails")]
        public IEnumerable<hlab_service_details> getallservicedetails(string service_id)
        {
            IEnumerable<hlab_service_details> detailList = _hlabServiceDetailsRepo.GetAllServiceDetails(Convert.ToInt32(service_id));
            return detailList;
        }

        [HttpGet("getdetailinfo")]
        public hlab_service_details getdetailinfo(string detail_id)
        {
            hlab_service_details detail = _hlabServiceDetailsRepo.GetServiceDetailInfo(Convert.ToInt32(detail_id));
            return detail;
        }

        //Service Header Intro
        [HttpPost("commithorizonlabserviceheaderchanges")]
        public ActionResult CommitHorizonLabServiceHeaderChanges(IEnumerable<hlab_web_services_intro> introList)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                if (_hlabServiceRepo.UpdateServiceHeader(introList)) return Ok();
                return BadRequest("CommitHorizonLabServiceHeaderChanges : Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("CommitHorizonLabServiceHeaderChanges Exception Error: " + xc);
            }
        }

        [HttpGet("getserviceheader")]
        public IEnumerable<hlab_web_services_intro> getserviceheader()
        {
            IEnumerable<hlab_web_services_intro> serviceList = _hlabServiceRepo.GetServiceHeader().ToList();
            return serviceList;
        }
    }
}
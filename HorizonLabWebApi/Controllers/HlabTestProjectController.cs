using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_test_project")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]

    public class HlabTestProjectController : ControllerBase
    {
        private Interface_test_projects _hlabTestProjects;
        private readonly ILogger<HlabTestProjectController> _logger;

        public HlabTestProjectController(Interface_test_projects hlabTestProjects, ILogger<HlabTestProjectController> logger)
        {
            _hlabTestProjects = hlabTestProjects;
            _logger = logger;
        }

        [HttpPost("addnewproject")]
        public ActionResult AddNewProject(hlab_test_projects new_project)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                bool result = _hlabTestProjects.AddNewTestPorject(new_project);
                if (result) return Ok();
                return BadRequest("AddNewUser Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("AddNewUser Exception Error: " + xc);
            }
        }

        [HttpPost("deleteaddprojectsupplies")]
        public ActionResult DeleteAddProjectSupplies(project_form_supply_param param)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Not a valid model");
                bool result = _hlabTestProjects.DeleteAddTestProjectSupplies(param); ;
                if (result) return Ok();
                return BadRequest("DeleteAddProjectSupplies Code Error");
            }
            catch (Exception xc)
            {
                return BadRequest("DeleteAddProjectSupplies Exception Error: " + xc);
            }
        }

        [HttpGet("getprojectsupplies")]
        public List<projectrequestsupplyview> GetProjectSupplies(int proj_form_id)
        {
            try
            {
                return _hlabTestProjects.GetAllProjectRequestSupplies(proj_form_id).ToList();
            }
            catch (Exception xc)
            {
                _logger.LogError($"GetProjectSupplies() : {xc.ToString()}");
                return null;
            }
        }

        [HttpPost("gettemporaryprojectrequests")]
        public List<hlab_temp_requests> gettemporaryprojectrequests(hlab_temp_requests param)
        {
            try
            {
                List<hlab_temp_requests> requestlist = new List<hlab_temp_requests>();
                requestlist = _hlabTestProjects.GetTemporaryProjectRequests(param).ToList();
                return requestlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("gettemporaryprojectrequestsview")]
        public List<temporaryprojectrequestsview> gettemporaryprojectrequestsview(temporaryprojectrequestsview request)
        {
            try
            {
                List<temporaryprojectrequestsview> requestlist = new List<temporaryprojectrequestsview>();
                requestlist = _hlabTestProjects.GetTemporaryProjectRequestsView(request).ToList();
                return requestlist;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpPost("updatebulkrequest")]
        public bool updatebulkrequest(BulkRequestInsertParameter param)
        {
            try
            {
                return _hlabTestProjects.UpdateBulkRequest(param);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("deletetemporaryrequests")]
        public bool deletetemporaryrequests(BulkRequestInsertParameter param)
        {
            try
            {
                return _hlabTestProjects.DeleteTemporaryRequests(param);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("insertbulkrequest")]
        public bool insertbulkrequest(BulkRequestInsertParameter param)
        {
            try
            {
                return _hlabTestProjects.InsertBulkRequestToDb(param);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }

        [HttpPost("bulkrequestinsert")]
        public bool bulkrequestinsert(bulkrequest_params param)
        {
            try
            {
                if (param.row_count == 0) return true;
                return _hlabTestProjects.BulkCreateTemporaryRequest(param);
            }
            catch (Exception xc)
            {
                _logger.LogError(xc.ToString());
                return false;
            }
        }
    }
}
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabWebApi.ApiFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HorizonLabWebApi.Controllers
{
    [Route("hlab_test_project_form")]
    [ApiController, ServiceFilter(typeof(APIKeyHandlers))]

    public class HlabTestProjectFormController : ControllerBase
    {
        private Interface_hlab_test_project_form _hlabTestProjectsForm;
        private readonly ILogger<HlabTestProjectFormController> _logger;

        public HlabTestProjectFormController(Interface_hlab_test_project_form hlabTestProjectsForm, ILogger<HlabTestProjectFormController> logger)
        {
            _hlabTestProjectsForm = hlabTestProjectsForm;
            _logger = logger;
        }

        [HttpPost("addnewprojectform")]
        public int AddNewProjectForm(hlab_test_project_forms new_form)
        {
            try
            {
                if (!ModelState.IsValid) return 0;
                int id = _hlabTestProjectsForm.AddNewTestPorjectForm(new_form);
                return id;
            }
            catch (Exception xc)
            {
                return 0;
            }
        }

        [HttpPost("updateprojectform")]
        public bool UpdateProjectForm(hlab_test_project_forms new_form)
        {
            try
            {
                if (!ModelState.IsValid) return false;
                return _hlabTestProjectsForm.UpdateTestProjForm(new_form);
            }
            catch (Exception xc)
            {
                return false;
            }
        }

        [HttpPost("getprojectrequestsforms")]
        public List<projectrequestsformview> getprojectrequestsforms(projectrequestsformview param)
        {
            try
            {
                List<projectrequestsformview> records = new List<projectrequestsformview>();
                records = _hlabTestProjectsForm.ListProjectRequestFormInfo(param).ToList();
                return records;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return null;
            }
        }

        [HttpGet("getprojectforminfo")]
        public projectrequestsformview GetProjectFormInfo(int proj_form_id)
        {
            try
            {
                return _hlabTestProjectsForm.GetProjectRequestFormInfo(proj_form_id);
            }
            catch (Exception xc)
            {
                _logger.LogError($"GetProjectFormInfo() : {xc.ToString()}");
                return null;
            }
        }
    }
}

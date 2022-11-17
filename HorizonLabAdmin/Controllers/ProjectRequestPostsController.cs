using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    [RequestFormLimits(ValueCountLimit = 20000)]
    public class ProjectRequestPostsController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private readonly ITestProject _projectRequestHelper;
        private readonly ITransaction _transactionHelper;
        private readonly IRequestItem _requestItemHelper;
        private readonly IRequest _requestHelper;
        private readonly ILogger<ProjectRequestPostsController> _logger;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly ITestProjectForm _testProjForm;

        public ProjectRequestPostsController(
            ITestProject projectRequestHelper, 
            ILogger<ProjectRequestPostsController> logger, 
            IHorizonLabSession sessionHelper, 
            ITransaction transactionHelper,
            IRequestItem requestItemHelper,
            IRequest requestHelper,
            ITestProjectForm testProjForm)
        {
            _sessionHelper = sessionHelper;
            _projectRequestHelper = projectRequestHelper;
            _logger = logger;
            _transactionHelper = transactionHelper;
            _requestItemHelper = requestItemHelper;
            _requestHelper = requestHelper;
            _testProjForm = testProjForm;
        }

        [HttpPost]
        public IActionResult ProcessNewProjectRequest(ProjectRequestPageObject project)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

            TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            project.RequestDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetZone).ToString("dd/MM/yyyy");
            if(project.InputDateRequest!=null) project.RequestDate = project.InputDateRequest.Value.ToString("dd/MM/yyyy");

            project.ReceivedBy = _sessionHelper.GetSessionUserName();
            TempData["BulkInsertRequestMessage"] = null;
            if (_projectRequestHelper.ProcessBulkRequests(project))
            {
                TempData["BulkInsertRequestMessage"] = "Bulk Test Request Insert was successful";
            }
            else
            {
                TempData["BulkInsertRequestMessage"] = "Bulk Test Request Insert failed, please contact administrator.";
            }
            return GoToProjectRequestPage(project);
        }

        [HttpPost]
        public IActionResult CreateBulkRequest(ProjectRequestPageObject project, List<int> checked_req_ids)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            TempData["BulkInsertRequestMessage"] = null;
            project.ReceivedBy = _sessionHelper.GetSessionUserName();
            project.request_delete_list = checked_req_ids;

            if (checked_req_ids != null && checked_req_ids.Count > 0)
            {
                _projectRequestHelper.DeleteTemporaryRequests(project);

                foreach (var id in checked_req_ids)
                {
                    project.temporary_request_list = _projectRequestHelper.RemoveRequestFromList(project.temporary_request_list, id);
                }
            }

            if (_projectRequestHelper.InsertBulkProjectRequestToDb(project))
            {
                TempData["BulkInsertRequestMessage"] = "Executing Bulk Request Insert to database was successful";
            }
            else
            {
                TempData["BulkInsertRequestMessage"] = "Executing Bulk Request Insert to database failed, please contact administrator.";
            }
            return GoToProjectRequestPage(project);
        }

        [HttpPost]
        public IActionResult UpdateTempRequest(ProjectRequestPageObject project)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            TempData["BulkInsertRequestMessage"] = null;
            project.ReceivedBy = _sessionHelper.GetSessionUserName();

            if (_projectRequestHelper.UpdateTemporaryRequests(project))
            {
                TempData["BulkInsertRequestMessage"] = "Updating Temporary Request was successful";
            }
            else
            {
                TempData["BulkInsertRequestMessage"] = "Updating Temporary Request failed, please contact administrator.";
            }
            return GoToProjectRequestPage(project);
        }

        [HttpPost]
        public IActionResult CreateProjectForm(ProjectRequestPageObject project, List<int> checked_req_ids, List<int> supply_ids)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            int new_projec_form_id = 0;
            int transid = 0;
            int isRush = 0;
            int isConditionMet = 0;
            int F054_form_id = 1;
            int F125_form_id = 3;
            hlab_test_project_forms hlab_test_project_forms = new hlab_test_project_forms();

            project.ReceivedBy = _sessionHelper.GetSessionUserName();
            hlab_test_project_forms = _projectRequestHelper.ConvertTestProjectToDbObject(project);
            new_projec_form_id = _testProjForm.AddNewTestProjecrFormToDb(hlab_test_project_forms);

            if(new_projec_form_id == 0) return GoToProjectRequestPage(project);

            _projectRequestHelper.DeleteAddProjectSupplies(new project_form_supply_param
            {
                supply_ids = supply_ids,
                proj_form_id = new_projec_form_id
            });

            foreach (int id in checked_req_ids)
            {
                project.temporary_request_list.Where(x => x.id == id).FirstOrDefault().proj_form_id = new_projec_form_id;
            }
            _projectRequestHelper.UpdateTemporaryRequests(project);

            if (project.isRush) isRush = 1;
            if (project.isConditionMet) isConditionMet = 1;
            F054P_paramter url_param = new F054P_paramter
            {
                pid = new_projec_form_id,
                fid = project.form_id,
                transid = transid,
                rush = isRush,
                condition = isConditionMet
            };

            if (project.form_id == F054_form_id) return GoToF054PForm(url_param);
            if (project.form_id == F125_form_id) return GoToF125PForm(url_param);
            return GoToProjectRequestPage(project);            
        }

        [HttpPost]
        public IActionResult DeleteProjectTempRequest(ProjectRequestPageObject project, List<int> checked_req_ids)
        {
            int request_id = 0;
            if(checked_req_ids.Count > 0)
            {              
                foreach (var id in checked_req_ids)
                {
                    request_id = project.temporary_request_list.Where(x => x.id == id).FirstOrDefault().request_id;
                    if(request_id!=0) _requestHelper.DeleteRequest(request_id);
                }

                _projectRequestHelper.DeleteTemporaryRequests(new ProjectRequestPageObject {
                    request_delete_list = checked_req_ids
                });
            }
            return GoToProjectRequestPage(project);
        }
    }
}
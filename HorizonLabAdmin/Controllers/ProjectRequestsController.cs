using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class ProjectRequestsController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private readonly ITestProject _projectRequestHelper;        
        private readonly IUtility _utilityHelper;        
        private readonly ILogger<ProjectRequestsController> _logger;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly Interface_receivers _Receiver;

        public ProjectRequestsController(ITestProject projectRequestHelper, ILogger<ProjectRequestsController> logger, IHorizonLabSession sessionHelper, IUtility utilityHelper, Interface_receivers Receiver)
        {
            _sessionHelper = sessionHelper;
            _projectRequestHelper = projectRequestHelper;
            _logger = logger;
            _Receiver = Receiver;
            _utilityHelper = utilityHelper;
        }

        [HttpGet]
        public IActionResult AddProjectRequest(int project_id = 0, int row_count = 0)
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            ProjectRequestPageObject project = new ProjectRequestPageObject
            {
                selected_project_id = project_id,
                row_count = row_count,
            };
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            try
            {
                project = _projectRequestHelper.PrepareProjectRequestData(project);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }
            ViewBag.menu = _hlabMenu;
            return View(project);
        }

        [HttpGet]
        public IActionResult RecordPage(int row_count, int payment_id, int test_pkg_id, int project_id, string date = "", string filter="a", int start=0, int end=100)
        {
            int record_count = 0;
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "order");
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

            string Message = "";
            if (TempData["BulkInsertRequestMessage"] != null)
            {
                Message = TempData["BulkInsertRequestMessage"].ToString();
                TempData["BulkInsertRequestMessage"] = null;
            }

            ProjectRequestPageObject project = new ProjectRequestPageObject
            {
                selected_project_id = project_id,
                selected_payment_type_id = payment_id,
                selected_testpkg_id = test_pkg_id,
                row_count = row_count,
                Message = Message,
                RequestDate = date
            };

            try
            {
                project = _projectRequestHelper.PrepareProjectRequestData(project);
                project.ProjectRequestRecords = _projectRequestHelper.ListProjectRequestRecords(project);

                project.FilterOptions = new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = "All", Value = "a"},
                    new SelectListItem { Selected = false, Text = "Processed", Value = "p"},
                    new SelectListItem { Selected = false, Text = "Un-Processed", Value = "u"}
                };
                project.FilterOptions = _utilityHelper.SetSelectedItemFromList(project.FilterOptions, filter);
                project.SelectedFilter = filter;

                project.ReceiverSelectList = _utilityHelper.GenerateSelectListItem(_Receiver.GetAllReceivers().ToList(), "id", "receiver").ToList();
                project.ReceiverSelectList.Add(new SelectListItem { Selected = true, Text = "", Value = "0" });

                record_count = project.ProjectRequestRecords.Count;
                if (start >= 0 && end > 0 && record_count > 0) project.ProjectRequestRecords = project.ProjectRequestRecords.GetRange(start, end-start);
                if (filter == "p" && project.ProjectRequestRecords != null && project.ProjectRequestRecords.Count > 0)
                {
                    project.ProjectRequestRecords = project.ProjectRequestRecords.Where(x => x.proj_form_id!=0).ToList();
                }
                else if (filter == "u" && project.ProjectRequestRecords != null && project.ProjectRequestRecords.Count > 0)
                {
                    project.ProjectRequestRecords = project.ProjectRequestRecords.Where(x => x.proj_form_id == 0).ToList();
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            ViewBag.start = start;
            ViewBag.end = end;
            ViewBag.record_count = record_count; ;
            return View(project);
        }
    }
}
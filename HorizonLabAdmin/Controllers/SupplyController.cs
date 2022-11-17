using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HorizonLabAdmin.Controllers
{
    public class SupplyController : HController
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();
        private readonly IConfiguration _appConfig;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly IUtility _utilityHelper;
        private readonly ISupply _supplyHelper;
        private readonly ITestPackage _testPackage;
        private readonly ITestProjectSupply _testProjectSupply;
        private readonly ITestProjectForm _testProjectForm;
        private HorizonLabTableReferenceApiLibrary _hllTableReference = new HorizonLabTableReferenceApiLibrary();
        private readonly ILogger<SupplyController> _logger;

        public SupplyController(
            IConfiguration appConfig,
            ILogger<SupplyController> logger,
            IHorizonLabSession sessionHelper,
            IUtility utilityHelper,
            ICustomer customerHelper,
            ISupply supplyHelper,
            ITestPackage testPackage,
            ITestProjectSupply testProjectSupply,
            ITestProjectForm testProjectForm
            )
        {
            _appConfig = appConfig;
            _supplyHelper = supplyHelper;
            _logger = logger;
            _testPackage = testPackage;
            _sessionHelper = sessionHelper;
            _utilityHelper = utilityHelper;
            _testProjectSupply = testProjectSupply;
            _testProjectForm = testProjectForm;
        }

        public IActionResult MainPage(int sid=0)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "supply");
            ViewData["UserName"] = _sessionHelper.GetSessionUserName();
            ViewBag.menu = _hlabMenu;
            
            int PackageMiscId = 2, PackageHiddenId = 3;
            SupplyViewDataObject viewdata = new SupplyViewDataObject();
            List<hlab_test_pkgs> test_package_list = new List<hlab_test_pkgs>();
            test_package_list = _testPackage.GetAllTestPackageList().Where(x => x.pkg_class_id != PackageMiscId && x.pkg_class_id != PackageHiddenId).ToList();
            
            try
            {
                viewdata.TestPackageSupplyList = _supplyHelper.GetAllTestPackageSupplies();
                if (sid != 0) {                    
                    viewdata.TestPackageList = _testPackage.GetAllTestPackageList().Where(x => x.pkg_class_id != PackageMiscId && x.pkg_class_id != PackageHiddenId).ToList();
                    viewdata.AssignedTestPakcageSupplyList = _supplyHelper.GetFilteredTestPackageSupplies(new testpackagesupplyview { supply_id = sid });
                    viewdata.SelectedSupply_id = sid;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"SupplyController > Index(): {exc.InnerException}");
            }
            return View(viewdata);
        }

        [HttpPost]
        public IActionResult SaveNewSupply(SupplyViewDataObject data)
        {
            int supply_id = 0;
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            if (data.hlab_supplies == null) return GoToSupplyMainPage(supply_id);
            supply_id = data.SelectedSupply_id;

            try
            {
                _supplyHelper.SaveNewSupply(data.hlab_supplies);
            }
            catch (Exception exc)
            {
                _logger.LogError($"SupplyController > SaveNewSupply(): {exc.InnerException}");
            }

            return GoToSupplyMainPage(supply_id);
        }

        [HttpPost]
        public IActionResult UpdateSupplies(SupplyViewDataObject data, List<int> pkg_id_list)
        {
            int supply_id = 0;
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            if (data.TestPackageSupplyList == null) return GoToSupplyMainPage(supply_id);
            supply_id = data.SelectedSupply_id;

            try
            {
                foreach(var supply in data.TestPackageSupplyList)
                {
                    _supplyHelper.UpdateSupplyChanges(supply);
                }  
                
                if(pkg_id_list.Count > 0)
                {
                    _supplyHelper.AssignTestPackageSupplyList(new test_pkg_supply_param() {
                        test_pkg_id_list = pkg_id_list,
                        supply_id = supply_id
                    });
                }
                else if(pkg_id_list.Count == 0)
                {
                    _supplyHelper.DeleteTestPackage(supply_id);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"SupplyController > UpdateSupplies(): {exc.InnerException}");
            }

            return GoToSupplyMainPage(supply_id);
        }

        [HttpGet]
        public IActionResult DeleteSupply(int sid = 0)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();
            if (sid == 0) return GoToSupplyMainPage(sid);

            _supplyHelper.DeleteSupply(sid);

            return GoToSupplyMainPage(sid);
        }

        [HttpPost]
        public IActionResult UpdateProjectTestSupplies(ProjectRequestPageObject data, List<int> supply_ids)
        {
            if (_sessionHelper.IsUserNotLoggedIn()) return GoToMainPage();

            try
            {
                _testProjectForm.UpdateNewTestProjecrFormToDb(new hlab_test_project_forms {
                    id = data.selected_project_form_id,
                    form_id = data.ProjectRequestFormInfo.form_id,
                    project_id = data.ProjectRequestFormInfo.project_id,
                    date_created = data.ProjectRequestFormInfo.date_created,
                    incubation_date_time_in = data.ProjectRequestFormInfo.incubation_date_time_in,
                    incubation_date_time_out = data.ProjectRequestFormInfo.incubation_date_time_out,
                    incubation_temp = data.ProjectRequestFormInfo.incubation_temp,
                    created_by = data.ProjectRequestFormInfo.created_by,
                    is_rush = data.ProjectRequestFormInfo.is_rush,
                    is_condition_met = data.ProjectRequestFormInfo.is_condition_met
                });
                _testProjectSupply.DeleteProjectSupplies(data.selected_project_form_id);
                _testProjectSupply.AddProjectSupplies(new project_supply_form
                {
                    supply_id_list = supply_ids,
                    proj_form_id = data.selected_project_form_id
                });
            }
            catch (Exception exc)
            {
                _logger.LogError($"SupplyController > UpdateProjectTestSupplies(): {exc.InnerException}");
            }

            return GoToWaterBacteriaProjectFormsPage(data);
        }
    }
}
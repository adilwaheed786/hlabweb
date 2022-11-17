using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HTestProject : ITestProject
    {
        private readonly IUtility _utility;
        private readonly Interface_test_projects _hlabTestProject;
        private readonly ILogger<HTestProject> _logger;
        private readonly Interface_test_package _testPackageRepo;
        private readonly Interface_hlab_test_payments _hlabPayment;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly ITestPackage _testPackageHelper;
        private readonly ISupply _supplyHelper;
        private readonly IHorizonLabSession _sessionHelper;

        public HTestProject(
            Interface_hlab_orders hlabOrderRepo,
            Interface_hlab_test_payments hlabPayment, 
            Interface_test_package testPackageRepo, 
            ILogger<HTestProject> logger, 
            Interface_test_projects hlabTestProject, 
            IUtility utility,
            ITestPackage testPackageHelper,
            ISupply supplyHelper,
            IHorizonLabSession sessionHelper)
        {
            _hlabOrderRepo = hlabOrderRepo;
            _hlabPayment = hlabPayment;
            _testPackageRepo = testPackageRepo;
            _logger = logger;
            _hlabTestProject = hlabTestProject;
            _utility = utility;
            _testPackageHelper = testPackageHelper;
            _supplyHelper = supplyHelper;
            _sessionHelper = sessionHelper;
        }

        public ProjectRequestPageObject PrepareProjectRequestData(ProjectRequestPageObject input_project)
        {
            try
            {
                int selected_project_id = input_project.selected_project_id;
                int selected_testpkg_id = input_project.selected_testpkg_id;
                int selected_payment_id = input_project.selected_payment_type_id;
                int row_count = input_project.row_count;
                string F054 = "1";

                List<hlab_supplies> supply_list = new List<hlab_supplies>();
                supply_list = _supplyHelper.GetAllTestPackageSupplies().ToList();

                List<SelectListItem> rush_list = _utility.GenerateYesNoSelectList().ToList();
                rush_list = _utility.SetSelectedItemFromList(rush_list, "False");

                List<SelectListItem> conditionmet_list = _utility.GenerateYesNoSelectList().ToList();
                conditionmet_list = _utility.SetSelectedItemFromList(conditionmet_list, "True");

                List<SelectListItem> form_list = _utility.GenerateSelectListItem(_testPackageHelper.GetAllTestPackageForms().ToList(), "id", "form_name").ToList();
                form_list = _utility.SetSelectedItemFromList(form_list, F054);

                List<SelectListItem> projects_list = _utility.GenerateSelectListItem(_hlabTestProject.GetAllTestProjects().ToList(), "id", "project").ToList();
                if (selected_project_id != 0) projects_list = _utility.SetSelectedItemFromList(projects_list, selected_project_id.ToString());

                List<SelectListItem> test_package_list = _utility.GenerateSelectListItem(_testPackageRepo.GetAllTestPackages().ToList(), "id", "lab_code").ToList();
                if (selected_testpkg_id != 0) test_package_list = _utility.SetSelectedItemFromList(test_package_list, selected_testpkg_id.ToString());

                List<SelectListItem> payment_list = _utility.GenerateSelectListItem(_hlabPayment.GetAllPaymentTypes().ToList(), "id", "payment").ToList();
                if (selected_payment_id != 0) payment_list = _utility.SetSelectedItemFromList(payment_list, selected_payment_id.ToString());

                ProjectRequestPageObject output_project = new ProjectRequestPageObject
                {
                    selected_project_id = selected_project_id,
                    selected_payment_type_id = selected_payment_id,
                    selected_testpkg_id = selected_testpkg_id,
                    Message = input_project.Message,
                    RequestDate = input_project.RequestDate,
                    row_count = row_count,
                    FormSelectList = form_list,
                    ProjectSelectList = projects_list,
                    TestPackageSelectList = test_package_list,
                    PaymentSelectList = payment_list,
                    IsConditionMetList = conditionmet_list,
                    RushList = rush_list,
                    TestPackageSupplyList = supply_list,
                    DateCreated = input_project.DateCreated,
                    selected_project_form_id = input_project.selected_project_form_id
                };
                return output_project;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HRequest > PrepareProjectRequestData(): {exc.Message}");
                return null;
            }
        }

        public bool ProcessBulkRequests(ProjectRequestPageObject project)
        {
            try
            {
                bool bulk_temp_request_result = false;
                bool bulk_request_result = false;
                bulkrequest_params bulk_request_param = new bulkrequest_params
                {
                    row_count = project.row_count,
                    payment_id = project.selected_payment_type_id,
                    test_pkg_id = project.selected_testpkg_id,
                    project_id = project.selected_project_id,
                    date_request = DateTime.Parse(project.RequestDate).Date
                };

                BulkRequestInsertParameter bulk_insert_param = new BulkRequestInsertParameter
                {
                    received_by = project.ReceivedBy,
                    request_date = DateTime.Parse(project.RequestDate).Date,
                    project_id = project.selected_project_id
                };

                bulk_temp_request_result = _hlabTestProject.BulkCreateTemporaryRequest(bulk_request_param);
                if (bulk_temp_request_result) bulk_request_result = _hlabTestProject.InsertBulkRequestToDb(bulk_insert_param);
                return bulk_request_result;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > ProcessBulkRequests(): {exc.Message}");
                return false;
            }
        }

        public List<temporaryprojectrequestsview> ListProjectRequestRecords(ProjectRequestPageObject request)
        {
            try
            {
                DateTime? RequestDate = null;
                if (!string.IsNullOrEmpty(request.RequestDate)) RequestDate = _utility.FormatStringToDateTime(request.RequestDate);
                if (request._RequestDate != null) RequestDate = request._RequestDate;

                temporaryprojectrequestsview param = new temporaryprojectrequestsview
                {
                    project_id = request.selected_project_id,
                    payment_id = request.selected_payment_type_id,
                    test_pkg_id = request.selected_testpkg_id,
                    date_insert = RequestDate,
                    proj_form_id = request.proj_form_id
                };

                return _hlabTestProject.GetTemporaryProjectRequestsView(param).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > ProcessBulkRequests(): {exc.Message}");
                return null;
            }
        }

        public bool InsertBulkProjectRequestToDb(ProjectRequestPageObject project)
        {
            try
            {
                DateTime request_date = DateTime.Now.Date;
                if (!string.IsNullOrEmpty(project.RequestDate)) request_date = DateTime.Parse(project.RequestDate).Date;
                BulkRequestInsertParameter param = new BulkRequestInsertParameter
                {
                    project_id = project.selected_project_id,
                    request_date = request_date,
                    received_by = project.ReceivedBy,
                    temporary_request_list = project.temporary_request_list
                };

                bool update_result = false;
                bool result = false;

                if (project.temporary_request_list != null && project.temporary_request_list.Count != 0)
                    update_result = _hlabTestProject.UpdateBulkRequest(param);

                if (update_result) result = _hlabTestProject.InsertBulkRequestToDb(param);

                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > InsertBulkProjectRequestToDb(): {exc.Message}");
                return false;
            }
        }

        public bool DeleteTemporaryRequests(ProjectRequestPageObject project)
        {
            try
            {
                BulkRequestInsertParameter param = new BulkRequestInsertParameter();
                param.request_delete_list = project.request_delete_list;
                return _hlabTestProject.DeleteTemporaryRequests(param);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > DeleteTemporaryRequests(): {exc.Message}");
                return false;
            }
        }

        public List<hlab_temp_requests> RemoveRequestFromList(List<hlab_temp_requests> request_list, int request_id_to_del)
        {
            try
            {
                hlab_temp_requests request_to_delete = new hlab_temp_requests();
                request_to_delete = request_list.Where(x => x.id == request_id_to_del).FirstOrDefault();
                request_list.Remove(request_to_delete);
                return request_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > RemoveRequestFromList(): {exc.Message}");
                return null;
            }
        }

        public bool UpdateTemporaryRequests(ProjectRequestPageObject project)
        {
            try
            {
                bool update_result = false;

                BulkRequestInsertParameter sp_param = new BulkRequestInsertParameter
                {
                    temporary_request_list = project.temporary_request_list,
                    project_id = project.selected_project_id,
                    request_date = DateTime.Parse(project.RequestDate).Date
                };

                if (project.temporary_request_list != null && project.temporary_request_list.Count != 0)
                    update_result = _hlabTestProject.UpdateBulkRequest(sp_param);

                return update_result;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > UpdateTemporaryRequests(): {exc.Message}");
                return false;
            }
        }

        public bool DeleteAddProjectSupplies(project_form_supply_param param)
        {
            try
            {
                return _hlabTestProject.DeleteAddTestProjectSupplies(param);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > DeleteAddProjectSupplies(): {exc.Message}");
                return false;
            }
        }

        public List<projectrequestsupplyview> GetProjectRequestSuppliesFromDb(int proj_form_id)
        {
            try
            {
                return _hlabTestProject.GetAllProjectRequestSupplies(proj_form_id).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > GetAllProjectRequestSupplies(): {exc.Message}");
                return null;
            }
        }

        public List<hlab_temp_requests> ListProjectRequestRecords(hlab_temp_requests request)
        {
            try
            {
                hlab_temp_requests param = new hlab_temp_requests
                {
                    id = request.id,
                    fname = request.fname,
                    lname = request.lname,
                    test_pkg_id = request.test_pkg_id,
                    proj_form_id = request.proj_form_id
                };

                return _hlabTestProject.GetTemporaryProjectRequests(param).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > ProcessBulkRequests(hlab_temp_requests): {exc.Message}");
                return null;
            }
        }

        public hlab_test_project_forms ConvertTestProjectToDbObject(ProjectRequestPageObject page_object)
        {
            try
            {
                return new hlab_test_project_forms
                {
                    form_id = page_object.form_id,
                    date_created = DateTime.Now,
                    incubation_date_time_in = page_object.IncubationIn,
                    incubation_date_time_out = page_object.IncubationIn.Value.AddHours(24),
                    incubation_temp = page_object.IncubationTemp,
                    created_by = _sessionHelper.GetSessionUserName(),
                    is_rush = page_object.isRush,
                    is_condition_met = page_object.isConditionMet,
                    project_id = page_object.selected_project_id
                };
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > ConvertTestProjectToDbObject(): {exc.Message}");
                return null;
            }
        }
    }
}

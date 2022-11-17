using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HTestPorjectForm: ITestProjectForm
    {
        private readonly IUtility _utility;
        private readonly ILogger<HTestPorjectForm> _logger;
        private readonly Interface_hlab_test_project_form _hlabTestProjectForm;

        public HTestPorjectForm(
            ILogger<HTestPorjectForm> logger,
            IUtility utility,
            Interface_hlab_test_project_form hlabTestProjectForm
            )
        {
            _logger = logger;
            _hlabTestProjectForm = hlabTestProjectForm;
            _utility = utility;
        }
        
        public int AddNewTestProjecrFormToDb(hlab_test_project_forms form)
        {
            try
            {
                return _hlabTestProjectForm.AddNewTestPorjectForm(form);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > AddNewTestProjecrFormToDb(): {exc.Message}");
                return 0;
            }
        }

        public projectrequestsformview GetProjectRequestFormInfo(int proj_form_id)
        {
            try
            {
                return _hlabTestProjectForm.GetProjectRequestFormInfo(proj_form_id);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > GetProjectRequestFormInfo(): {exc.Message}");
                return null;
            }
        }

        public List<projectrequestsformview> ListProjectRequestFormInfo(projectrequestsformview param)
        {
            try
            {
                return _hlabTestProjectForm.ListProjectRequestFormInfo(param).OrderByDescending(x => x.date_created).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > ListProjectRequestFormInfo(): {exc.Message}");
                return null;
            }
        }

        public bool UpdateNewTestProjecrFormToDb(hlab_test_project_forms form)
        {
            try
            {
                return _hlabTestProjectForm.UpdateTestProjForm(form);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProject > ListProjectRequestFormInfo(): {exc.Message}");
                return false;
            }
        }
    }
}

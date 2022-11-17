using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestProjectForm: Interface_hlab_test_project_form
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestProjectForm> _logger;

        public HlabTestProjectForm(HorizonLabDbContext hlab_db_context, ILogger<HlabTestProjectForm> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public int AddNewTestPorjectForm(hlab_test_project_forms new_form)
        {
            try
            {
                _hlab_Db_Context.hlab_test_project_forms.Add(new_form);
                _hlab_Db_Context.SaveChanges();
                return new_form.id;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > AddNewTestPorjectForm{exc.Message}");
                return 0;
            }
        }

        public bool UpdateTestProjForm(hlab_test_project_forms param)
        {
            try
            {
                _hlab_Db_Context.hlab_test_project_forms.Update(param);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > UpdateTestProjForm{exc.Message}");
                return false;
            }
        }

        public projectrequestsformview GetProjectRequestFormInfo(int proj_form_id)
        {
            try
            {
                return _hlab_Db_Context.projectrequestsformview.Where(x => x.id == proj_form_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > GetProjectRequestFormInfo{exc.Message}");
                return null;
            }
        }

        public List<projectrequestsformview> ListProjectRequestFormInfo(projectrequestsformview param)
        {
            try
            {
                List<projectrequestsformview> record_list = new List<projectrequestsformview>();
                record_list = _hlab_Db_Context.projectrequestsformview.ToList();
                DateTime? add_date_created = null;
                string str_date_created = "";

                if (param.project_id != 0 && param.project_id != null)
                {
                    record_list = record_list.Where(x => x.project_id == param.project_id).ToList();
                }

                if (param.date_created != null)
                {
                    //str_date_created = param.date_created.Value.ToString("dd/MM/yyyy");
                    //param.date_created = DateTime.ParseExact(str_date_created, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    add_date_created = param.date_created.Value.AddDays(1);
                    record_list = record_list.Where(
                        x => x.date_created >= param.date_created
                        && x.date_created <= add_date_created
                        ).ToList();
                }

                return record_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > GetProjectRequestFormInfo{exc.Message}");
                return null;
            }
        }
    }
}

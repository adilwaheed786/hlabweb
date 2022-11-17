using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestProject: Interface_test_projects
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestProject> _logger;

        public HlabTestProject(HorizonLabDbContext hlab_db_context, ILogger<HlabTestProject> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddNewTestPorject(hlab_test_projects new_project)
        {
            try
            {
                _hlab_Db_Context.hlab_test_projects.Add(new_project);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > AddNewTestPorject{exc.Message}");
                return false;
            }
        }

        public bool DeleteAddTestProjectSupplies(project_form_supply_param param)
        {
            try
            {
                _hlab_Db_Context.RemoveRange(
                    _hlab_Db_Context.hlab_test_proj_supplies.Where(
                        x => x.proj_form_id == param.proj_form_id));
                _hlab_Db_Context.SaveChanges();

                foreach (var id in param.supply_ids)
                {
                    _hlab_Db_Context.hlab_test_proj_supplies.Add(new hlab_test_proj_supplies
                    {
                        supply_id = id,
                        proj_form_id = param.proj_form_id
                    });
                    _hlab_Db_Context.SaveChanges();
                }
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"AddTestProjectSupplies(): {exc.Message}");
                return false;
            }
        }

        public IEnumerable<projectrequestsupplyview> GetAllProjectRequestSupplies(int proj_form_id)
        {
            try
            {
                return _hlab_Db_Context.projectrequestsupplyview.Where(x => x.proj_form_id == proj_form_id).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > GetAllProjectRequestSupplies{exc.Message}");
                return null;
            }
        }

        public IEnumerable<hlab_test_projects> GetAllTestProjects()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_projects.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > GetAllTestProjects{exc.Message}");
                return null;
            }
        }

        public IEnumerable<temporaryprojectrequestsview> GetTemporaryProjectRequestsView(temporaryprojectrequestsview param)
        {
            try
            {
                List<temporaryprojectrequestsview> projects_request_list = new List<temporaryprojectrequestsview>();
                if (param.project_id != 0)
                {
                    projects_request_list = _hlab_Db_Context.temporaryprojectrequestsview.Where(x => x.project_id == param.project_id).ToList();
                }
                else if (param.proj_form_id != 0)
                {
                    projects_request_list = _hlab_Db_Context.temporaryprojectrequestsview.Where(x => x.proj_form_id == param.proj_form_id).ToList();
                }

                if(param.project_id == 0 && param.proj_form_id == 0 && param.test_pkg_id == 0 && param.payment_id == 0 && param.date_insert != null)
                {
                    projects_request_list = _hlab_Db_Context.temporaryprojectrequestsview.Where(x => x.date_insert == param.date_insert).ToList();
                }

                if (projects_request_list == null || projects_request_list.Count == 0) return projects_request_list;

                if (param.test_pkg_id != 0)
                {
                    projects_request_list = projects_request_list.Where(x => x.test_pkg_id == param.test_pkg_id).ToList();
                }

                if (param.payment_id != 0)
                {
                    projects_request_list = projects_request_list.Where(x => x.payment_id == param.payment_id).ToList();
                }

                if (param.date_insert != null)
                {
                    projects_request_list = projects_request_list.Where(
                        x => x.date_insert >= param.date_insert
                        && x.date_insert <= param.date_insert.Value.AddDays(1)
                        ).ToList();
                }

                if(projects_request_list!=null && projects_request_list.Count > 0)
                {
                    projects_request_list = projects_request_list.OrderBy(x => x.id).ToList();
                }
                return projects_request_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > GetTemporaryProjectRequests():{exc.Message}");
                return null;
            }
        }

        public bool BulkCreateTemporaryRequest(bulkrequest_params param)
        {
            try
            {
                _hlab_Db_Context.Database.ExecuteSqlCommand("sp_BulkTempRequestInsert @p0, @p1, @p2, @p3, @p4", param.row_count, param.payment_id, param.test_pkg_id, param.project_id, param.date_request);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > BulkCreateTemporaryRequest():{exc.Message}");
                return false;
            }
        }

        public bool InsertBulkRequestToDb(BulkRequestInsertParameter param)
        {
            try
            {
                _hlab_Db_Context.Database.ExecuteSqlCommand("sp_MainBulkInsertRequest @p0, @p1, @p2 ", param.project_id, param.request_date, param.received_by);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > InsertBulkRequestTDb():{exc.Message}");
                return false;
            }
        }

        public bool UpdateBulkRequest(BulkRequestInsertParameter param)
        {
            int row_count = 0;
            try
            {
                _hlab_Db_Context.hlab_temp_requests.UpdateRange(param.temporary_request_list);
                _hlab_Db_Context.SaveChanges();
                _hlab_Db_Context.Database.ExecuteSqlCommand("sp_BulkUpdateRequest @p0, @p1", param.project_id, param.request_date);
                return true;
            }
            catch (Exception exc)
            {
                if (param.temporary_request_list != null) row_count = param.temporary_request_list.Count();
                _logger.LogError($"HlabTestProject > UpdateBulkRequestToDb():{exc.Message}. Number of rows for update:{row_count}");
                return false;
            }
        }

        public bool DeleteTemporaryRequests(BulkRequestInsertParameter param)
        {
            try
            {
                List<int> id_list = param.request_delete_list;
                if (id_list == null && id_list.Count > 0) return false;
                List<hlab_temp_requests> request_delete_list = new List<hlab_temp_requests>();
                foreach (var id in id_list)
                {
                    request_delete_list.Add(_hlab_Db_Context.hlab_temp_requests.Where(x => x.id == id).FirstOrDefault());
                }
                _hlab_Db_Context.RemoveRange(request_delete_list);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > DeleteTemporaryRequests():{exc.Message}");
                return false;
            }
        }

        public IEnumerable<hlab_temp_requests> GetTemporaryProjectRequests(hlab_temp_requests param)
        {
            try
            {
                List<hlab_temp_requests> request_list = new List<hlab_temp_requests>();
                if (param.proj_form_id != 0)
                {
                    request_list = _hlab_Db_Context.hlab_temp_requests.Where(x => x.proj_form_id == param.proj_form_id).ToList();
                }
                return request_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestProject > GetTemporaryProjectRequests():{exc.Message}");
                return null;
            }
        }
    }
}

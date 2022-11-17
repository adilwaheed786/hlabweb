using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestProjectSupplies : Interface_hlab_project_supply
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestProjectSupplies> _logger;

        public HlabTestProjectSupplies(HorizonLabDbContext hlab_db_context, ILogger<HlabTestProjectSupplies> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddProjectSupplies(project_supply_form param)
        {
            try
            {
                foreach (var id in param.supply_id_list)
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
                _logger.LogError($"HlabTestPorjectSupplies > AddProjectSupplies(): {exc.Message}");
                return false;
            }
        }

        public bool DeleteProjectSupplies(int proj_form_id)
        {
            try
            {
                _hlab_Db_Context.RemoveRange(
                    _hlab_Db_Context.hlab_test_proj_supplies.Where(
                        x => x.proj_form_id == proj_form_id));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabTestPorjectSupplies > DeleteProjectSupplies(): {exc.Message}");
                return false;
            }
        }
    }
}

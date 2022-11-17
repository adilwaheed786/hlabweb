using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabWaterChemRepository : Interface_water_chem_result
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabWaterChemRepository> _logger;

        public HlabWaterChemRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabWaterChemRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddTraceMetalResults(hlab_trace_metal_results test_result)
        {
            try
            {
                _hlab_Db_Context.hlab_trace_metal_results.Add(test_result);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool AddWaterChemA(hlab_chem_water_results_set_a test_result)
        {
            try
            {
                _hlab_Db_Context.hlab_chem_water_results_set_a.Add(test_result);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool AddWaterChemB(hlab_chem_water_results_set_b test_result)
        {
            try
            {
                _hlab_Db_Context.hlab_chem_water_results_set_b.Add(test_result);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteReseedTraceMetalResults(int transid)
        {
            try
            {
                _hlab_Db_Context.Database.ExecuteSqlCommand("sp_DeleteTraceMetalResults_withReseed @p0", transid);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteReseedWaterChemA(int transid)
        {
            try
            {
                _hlab_Db_Context.Database.ExecuteSqlCommand("sp_DeleteWaterChemTestResultSetA_withReseed @p0", transid);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteReseedWaterChemB(int transid)
        {
            try
            {
                _hlab_Db_Context.Database.ExecuteSqlCommand("sp_DeleteWaterChemTestResultSetB_withReseed @p0", transid);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<hlab_trace_metal_results> GetTraceMetalResults(int transid)
        {
            try
            {
                return _hlab_Db_Context.hlab_trace_metal_results.Where(x => x.trans_id == transid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public IEnumerable<hlab_chem_water_results_set_a> GetWaterChemResultA(int transid)
        {
            try
            {
                return _hlab_Db_Context.hlab_chem_water_results_set_a.Where(x => x.trans_id == transid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public IEnumerable<hlab_chem_water_results_set_b> GetWaterChemResultB(int transid)
        {
            try
            {
                return _hlab_Db_Context.hlab_chem_water_results_set_b.Where(x => x.trans_id == transid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool UpdateTraceMetalResults(hlab_trace_metal_results test_result)
        {
            try
            {
                _hlab_Db_Context.hlab_trace_metal_results.Update(test_result);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool UpdateWaterChemA(hlab_chem_water_results_set_a test_result)
        {
            try
            {
                _hlab_Db_Context.hlab_chem_water_results_set_a.Update(test_result);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool UpdateWaterChemB(hlab_chem_water_results_set_b test_result)
        {
            try
            {
                _hlab_Db_Context.hlab_chem_water_results_set_b.Update(test_result);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }
    }
}

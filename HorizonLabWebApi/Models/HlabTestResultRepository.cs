using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestResultRepository : Interface_test_results
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestResultRepository> _logger;

        public HlabTestResultRepository(HorizonLabDbContext hlab_db_context, ILogger<HlabTestResultRepository> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddTestResults(hlab_test_results htt)
        {
            try
            {
                _hlab_Db_Context.hlab_test_results.Add(htt);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteTestResults(int id)
        {
            try
            {
                _hlab_Db_Context.hlab_test_results.RemoveRange(_hlab_Db_Context.hlab_test_results.Where(x=>x.result_id == id));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteTestResultsByTransId(int transid)
        {
            try
            {
                _hlab_Db_Context.testresults.FromSql("sp_DeleteTestResultWithReseed " + transid);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<testresultsview> GetAllTestResults(testresultsview htr)
        {
            List<testresultsview> testresultlist = new List<testresultsview>();
            testresultlist = _hlab_Db_Context.testresultsview.ToList();
            testresultlist = testresultlist.Where(x => x.trans_id == htr.trans_id).ToList();
            return testresultlist;
        }

        public IEnumerable<sp_gettestresults> GetTestResults(int transid)
        {
            try
            {
                return _hlab_Db_Context.testresults.FromSql("sp_GetTestResults " + transid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }            
        }

        public hlab_test_results GetTransactionDetails(int result_id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTestResults(hlab_test_results htr)
        {
            try
            {
                if (htr != null)
                {
                    _hlab_Db_Context.hlab_test_results.Update(htr);
                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("MODEL UpdateTestResults : Unable to update test records, hlab_test_results object is null");
                    return false;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }
    }
}

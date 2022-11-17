using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestReport: Interface_test_report_types
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestReport> _logger;

        public HlabTestReport(HorizonLabDbContext hlab_db_context, ILogger<HlabTestReport> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_report_types> GetAllReportTypes()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_report_types.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

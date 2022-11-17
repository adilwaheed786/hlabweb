using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestSampleType: Interface_test_sample_types
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestSampleType> _logger;

        public HlabTestSampleType(HorizonLabDbContext hlab_db_context, ILogger<HlabTestSampleType> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_sample_types> GetAllTestSampleTypes()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_sample_types.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

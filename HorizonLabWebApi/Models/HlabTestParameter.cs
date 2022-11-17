using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestParameter: Interface_test_parameter
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestParameter> _logger;

        public HlabTestParameter(HorizonLabDbContext hlab_db_context, ILogger<HlabTestParameter> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_params> GetAllTestParameters()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_params.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

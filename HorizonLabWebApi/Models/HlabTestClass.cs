using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestClass: Interface_test_class
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestClass> _logger;

        public HlabTestClass(HorizonLabDbContext hlab_db_context, ILogger<HlabTestClass> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_pkgs_class> GetTestClasses()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_pkgs_class.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

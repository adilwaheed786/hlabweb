using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabTestDescription : Interface_test_description
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestDescription> _logger;

        public HlabTestDescription(HorizonLabDbContext hlab_db_context, ILogger<HlabTestDescription> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_descriptions> GetAllTestDescriptions()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_descriptions.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

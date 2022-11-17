using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class Province : Interface_hlab_provinces
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<Province> _logger;

        public Province(HorizonLabDbContext hlab_db_context, ILogger<Province> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_provinces> GetAllProvinces()
        {
            try
            {
                return _hlab_Db_Context.hlab_provinces.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

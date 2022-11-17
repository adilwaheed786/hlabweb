using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabReceivers: Interface_receivers
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabReceivers> _logger;

        public HlabReceivers(HorizonLabDbContext hlab_db_context, ILogger<HlabReceivers> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_receivers> GetAllReceivers()
        {
            try
            {
                return _hlab_Db_Context.hlab_receivers.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

using HorizonLabAdmin.Interfaces;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HTestProjectSupply : ITestProjectSupply
    {
        private Interface_hlab_project_supply _hlabTestProjectSupply;
        private readonly ILogger<HTestProjectSupply> _logger;

        public HTestProjectSupply(Interface_hlab_project_supply hlabTestProjectSupply, ILogger<HTestProjectSupply> logger)
        {
            _hlabTestProjectSupply = hlabTestProjectSupply;
            _logger = logger;
        }

        public bool AddProjectSupplies(project_supply_form param)
        {
            try
            {
                return _hlabTestProjectSupply.AddProjectSupplies(param);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProjectSupply > AddProjectSupplies{exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool DeleteProjectSupplies(int proj_form_id)
        {
            try
            {
                return _hlabTestProjectSupply.DeleteProjectSupplies(proj_form_id);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HTestProjectSupply > AddProjectSupplies{exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HSupply : ISupply
    {
        private readonly IUtility _utility;
        private readonly Interface_hlab_supplies _hlabSupplies;
        private readonly ILogger<HEmail> _logger;
        private readonly IHorizonLabSession _sessionHelper;

        public HSupply(
            ILogger<HEmail> logger, IHorizonLabSession sessionHelper,
            Interface_hlab_supplies hlabSupplies,IUtility utility)
        {            
            _sessionHelper = sessionHelper;
            _utility = utility;
            _logger = logger;
            _hlabSupplies = hlabSupplies;
        }

        public bool AssignTestPackageSupplyList(test_pkg_supply_param supply_param)
        {
            try
            {
                if (_hlabSupplies.DeleteTestPackageSupplies(supply_param.supply_id))
                {
                    _hlabSupplies.AddTestPackageSupplies(supply_param);
                    return true;
                }                
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > AssignTestPackageSupplyList(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool DeleteSupply(int supplyid)
        {
            try
            {
                _hlabSupplies.DeleteSupply(supplyid);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > AssignTestPackageSupplyList(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool DeleteTestPackage(int supply_id)
        {
            try
            {
                _hlabSupplies.DeleteTestPackageSupplies(supply_id);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > DeleteTestPackage(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<hlab_supplies> GetAllTestPackageSupplies()
        {
            try
            {
                List<hlab_supplies> supply_list = _hlabSupplies.GetAllTestPackageSupplies().ToList();                
                return supply_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > GetAllTestPackageSupplies(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<testpackagesupplyview> GetFilteredTestPackageSupplies(testpackagesupplyview supply)
        {
            try
            {
                List<testpackagesupplyview> supply_list = _hlabSupplies.GetFilteredTestPackageSupplies(supply).ToList();
                return supply_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > GetAllTestPackageSupplies(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool SaveNewSupply(hlab_supplies supply)
        {
            try
            {
                return _hlabSupplies.AddNewSupply(supply);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > SaveNewSupply(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool UpdateSupplyChanges(hlab_supplies supply)
        {
            try
            {
                return _hlabSupplies.UpdateSupply(supply);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HSupply > UpdateSupplyChanges(): {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

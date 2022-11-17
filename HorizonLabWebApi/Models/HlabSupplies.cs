using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabSupplies : Interface_hlab_supplies
    {

        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabSupplies> _logger;

        public HlabSupplies(HorizonLabDbContext hlab_db_context, ILogger<HlabSupplies> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AddNewSupply(hlab_supplies object_parameter)
        {
            try
            {
                object_parameter.status = true;
                _hlab_Db_Context.hlab_supplies.Add(object_parameter);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > AddNewSupply: {exc.Message}");
                return false;
            }
        }

        public bool AddTestPackageSupplies(test_pkg_supply_param parameter)
        {
            try
            {
                foreach(var pkgid in parameter.test_pkg_id_list)
                {
                    _hlab_Db_Context.hlab_test_pkg_supplies.Add(new hlab_test_pkg_supplies {
                        pkg_id = pkgid,
                        supply_id = parameter.supply_id
                    });
                    _hlab_Db_Context.SaveChanges();
                }                
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > AddTestPackageSupplies: {exc.Message}");
                return false;
            }
        }

        public bool DeleteSupply(int supplyid)
        {
            try
            {
                bool result = DeleteTestPackageSupplies(supplyid);
                if (result)
                {
                    _hlab_Db_Context.hlab_supplies.RemoveRange(_hlab_Db_Context.hlab_supplies.Where(x => x.id == supplyid));
                    _hlab_Db_Context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > DeleteSupply: {exc.Message}");
                return false;
            }
        }

        public bool DeleteTestPackageSupplies(int supply_id)
        {
            try
            {
                _hlab_Db_Context.hlab_test_pkg_supplies.RemoveRange(_hlab_Db_Context.hlab_test_pkg_supplies.Where(x => x.supply_id == supply_id));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > DeleteTestPackageSupplies: {exc.Message}");
                return false;
            }
        }

        public IEnumerable<hlab_supplies> GetAllTestPackageSupplies()
        {
            try
            {
                return _hlab_Db_Context.hlab_supplies.Where(x => x.status == true).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > GetAllTestPackageSupplies: {exc.Message}");
                return null;
            }
        }

        public IEnumerable<testpackagesupplyview> GetFilteredTestPackageSupplies(testpackagesupplyview object_parameter)
        {
            try
            {
                List<testpackagesupplyview> list_supply = new List<testpackagesupplyview>();
                int pkg_id = object_parameter.pkg_id;
                int supply_id = object_parameter.supply_id;
                string lot = !string.IsNullOrEmpty(object_parameter.lot) ? object_parameter.lot : "";
                string name = !string.IsNullOrEmpty(object_parameter.name) ? object_parameter.name : "";


                if (pkg_id != 0) {
                    list_supply = _hlab_Db_Context.testpackagesupplyview.Where(x => x.pkg_id == pkg_id).ToList();
                    return list_supply;
                }
                if (supply_id != 0)
                {
                    list_supply = _hlab_Db_Context.testpackagesupplyview.Where(x => x.supply_id == supply_id).ToList();
                    return list_supply;
                }
                if (!string.IsNullOrEmpty(object_parameter.name))
                {
                    list_supply = _hlab_Db_Context.testpackagesupplyview.Where(x => x.name.ToLower().Contains(name.ToLower())).ToList();
                    return list_supply;
                }

                if (!string.IsNullOrEmpty(object_parameter.lot))
                {
                    list_supply = _hlab_Db_Context.testpackagesupplyview.Where(x => x.lot.ToLower().Contains(lot.ToLower())).ToList();
                    return list_supply;
                }
                return list_supply;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > GetFilteredTestPackageSupplies: {exc.Message}");
                return null;
            }
        }

        public bool UpdateSupply(hlab_supplies object_parameter)
        {
            try
            {
                _hlab_Db_Context.hlab_supplies.Update(object_parameter);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HlabSupplies > UpdateSupply: {exc.Message}");
                return false;
            }
        }
    }
}

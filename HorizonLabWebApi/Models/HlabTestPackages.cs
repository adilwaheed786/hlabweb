using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorizonLabWebApi.Models
{
    public class HlabTestPackages : Interface_test_package
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabTestPackages> _logger;

        public HlabTestPackages(HorizonLabDbContext hlab_db_context, ILogger<HlabTestPackages> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_pkgs> GetTestPackages(int classid = 0)
        {
            try
            {
                if (classid != 0) return _hlab_Db_Context.hlab_test_pkgs.Where(x => x.pkg_class_id == classid).OrderBy(x => x.lab_code).ToList();
                return _hlab_Db_Context.hlab_test_pkgs.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public hlab_test_pkgs GetTestPackageDetails(int pkg_id)
        {
            try
            {
                return _hlab_Db_Context.hlab_test_pkgs.Where(x => x.id == pkg_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool AddTestPackage(hlab_test_pkgs package)
        {
            try
            {
                _hlab_Db_Context.hlab_test_pkgs.Add(package);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<sp_gettestpackagesbycategory> GetAllPackagesByCategory(int categoryid)
        {
            try
            {
                return _hlab_Db_Context.sp_gettestpackagesbycategory.FromSql("sp_GetTestPackagesByCategory " + categoryid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public IEnumerable<hlab_test_pkgs> GetAllTestPackages()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_pkgs.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool UpdateTestPackage(hlab_test_pkgs package)
        {
            try
            {
                _hlab_Db_Context.hlab_test_pkgs.Update(package);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool AddTestPackageCategorization(int pkgid, int ctgryid)
        {
            try
            {
                hlab_test_default_parameter_category input = new hlab_test_default_parameter_category();
                input.pkg_id = pkgid;
                input.category_id = ctgryid;
                _hlab_Db_Context.hlab_test_default_parameter_category.Add(input);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool AddTestPackageCategory(hlab_test_pkgs_category category)
        {
            try
            {
                _hlab_Db_Context.hlab_test_pkgs_category.Add(category);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteTestPackageCategorization(int pkgid, int ctgryid)
        {
            try
            {
                _hlab_Db_Context.hlab_test_default_parameter_category.RemoveRange(
                    _hlab_Db_Context.hlab_test_default_parameter_category.Where(x => x.pkg_id == pkgid && x.category_id == ctgryid
                    ));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<hlab_test_pkgs_category> GetPackageCategories()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_pkgs_category.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public bool UpdateTestPackageCategory(hlab_test_pkgs_category category)
        {
            try
            {
                _hlab_Db_Context.hlab_test_pkgs_category.Update(category);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public IEnumerable<hlab_test_pkgs_forms> GetTestPackageForms()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_pkgs_forms.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

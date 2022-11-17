using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabDefaultParameter: Interface_hlab_test_default_pkg_params
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<HlabDefaultParameter> _logger;

        public HlabDefaultParameter(HorizonLabDbContext hlab_db_context, ILogger<HlabDefaultParameter>  logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public bool AssignDefaultParam(hlab_test_default_pkg_params param)
        {
            try
            {
                _hlab_Db_Context.hlab_test_default_pkg_params.Add(param);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteDefaultParam(hlab_test_default_pkg_params param)
        {
            try
            {
                _hlab_Db_Context.hlab_test_default_pkg_params.Remove(param);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public List<sp_getdefaultpackageparameters> GetDefaultTestParams(int packageid)
        {
            try
            {
                return _hlab_Db_Context.default_package_parameters.FromSql("sp_GetDefaultPackageParameters " + packageid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

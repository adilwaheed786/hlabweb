using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class Municipality: Interface_rural_municipality
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<Municipality> _logger;

        public Municipality(HorizonLabDbContext hlab_db_context, ILogger<Municipality> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public int AddRuralMunicipality(hlab_rural_municipalities municipality)
        {
            try{
                municipality.province_id = 3; //automatically set the new city to Manitoba (id:3)
                _hlab_Db_Context.hlab_rural_municipalities.Add(municipality);
                _hlab_Db_Context.SaveChanges();

                return municipality.id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return 0;
            }
        }

        public IEnumerable<hlab_rural_municipalities> GetRuralMunicipalities()
        {
            try
            {
                return _hlab_Db_Context.hlab_rural_municipalities.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

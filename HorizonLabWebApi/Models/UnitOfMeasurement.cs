using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class UnitOfMeasurement: Interface_unit_of_measurement
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<UnitOfMeasurement> _logger;

        public UnitOfMeasurement(HorizonLabDbContext hlab_db_context, ILogger<UnitOfMeasurement> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_test_measurement_units> GetAllUnitMeasurements()
        {
            try
            {
                return _hlab_Db_Context.hlab_test_measurement_units.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public int AddNewUnitofMeasurement(hlab_test_measurement_units unit)
        {
            try
            {
                _hlab_Db_Context.hlab_test_measurement_units.Add(unit);
                _hlab_Db_Context.SaveChanges();

                return unit.id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return 0;
            }

        }
    }
}

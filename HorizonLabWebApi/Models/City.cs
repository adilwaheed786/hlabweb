using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class City : Interface_hlab_cities
    {
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private readonly ILogger<City> _logger;

        public City(HorizonLabDbContext hlab_db_context, ILogger<City> logger)
        {
            _hlab_Db_Context = hlab_db_context;
            _logger = logger;
        }

        public IEnumerable<hlab_cities> GetAllCities(int provinceid)
        {
            try
            {
                return _hlab_Db_Context.hlab_cities.Where(x => x.province_id==provinceid).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public int AddNewCity(hlab_cities new_city)
        {
            try
            {
                new_city.province_id = 3; //automatically set the new city to Manitoba (id:3)
                _hlab_Db_Context.hlab_cities.Add(new_city);
                _hlab_Db_Context.SaveChanges();

                return new_city.id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return 0;
            }

        }
    }
}

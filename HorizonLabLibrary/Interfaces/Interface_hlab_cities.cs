using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_cities
    {
        IEnumerable<hlab_cities> GetAllCities(int provinceid);
        int AddNewCity(hlab_cities new_city);
    }
}

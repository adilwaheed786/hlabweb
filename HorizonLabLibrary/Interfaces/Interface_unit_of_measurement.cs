using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_unit_of_measurement
    {
        IEnumerable<hlab_test_measurement_units> GetAllUnitMeasurements();
        int AddNewUnitofMeasurement(hlab_test_measurement_units unit);
    }
}

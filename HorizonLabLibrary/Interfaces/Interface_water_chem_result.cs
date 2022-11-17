using System;
using System.Collections.Generic;
using System.Text;
using HorizonLabLibrary.Entities;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_water_chem_result
    {
        IEnumerable<hlab_chem_water_results_set_a> GetWaterChemResultA(int transid);
        IEnumerable<hlab_chem_water_results_set_b> GetWaterChemResultB(int transid);
        IEnumerable<hlab_trace_metal_results> GetTraceMetalResults(int transid);

        bool AddWaterChemA(hlab_chem_water_results_set_a test_result);
        bool AddWaterChemB(hlab_chem_water_results_set_b test_result);
        bool AddTraceMetalResults(hlab_trace_metal_results test_result);

        bool UpdateWaterChemA(hlab_chem_water_results_set_a test_result);
        bool UpdateWaterChemB(hlab_chem_water_results_set_b test_result);
        bool UpdateTraceMetalResults(hlab_trace_metal_results test_result);

        bool DeleteReseedWaterChemA(int transid);
        bool DeleteReseedWaterChemB(int transid);
        bool DeleteReseedTraceMetalResults(int transid);
    }
}

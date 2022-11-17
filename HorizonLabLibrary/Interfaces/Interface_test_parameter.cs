using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_test_parameter
    {
        IEnumerable<hlab_test_params> GetAllTestParameters();
    }
}

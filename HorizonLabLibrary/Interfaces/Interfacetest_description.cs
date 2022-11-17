using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interfacetest_description
    {
        IEnumerable<hlab_test_descriptions> GetAllTestDescriptions();
    }
}

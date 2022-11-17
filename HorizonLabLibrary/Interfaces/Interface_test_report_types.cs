using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_test_report_types
    {
        IEnumerable<hlab_test_report_types> GetAllReportTypes();
    }
}

using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_test_default_pkg_params
    {
        List<sp_getdefaultpackageparameters> GetDefaultTestParams(int packageid);
        bool AssignDefaultParam(hlab_test_default_pkg_params param);
        bool DeleteDefaultParam(hlab_test_default_pkg_params param);
    }
}

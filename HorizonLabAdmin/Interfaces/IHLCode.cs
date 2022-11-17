using HorizonLabAdmin.Helpers.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IHLCode
    {
        string GenerateHLCode(HLCodeParameters hlcode_param);
    }
}

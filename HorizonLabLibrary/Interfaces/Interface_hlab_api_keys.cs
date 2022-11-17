using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_api_keys
    {
        hlab_api_keys GetApi(string api);
    }
}

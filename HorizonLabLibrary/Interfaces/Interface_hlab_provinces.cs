﻿using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_provinces
    {
        IEnumerable<hlab_provinces> GetAllProvinces();
    }
}

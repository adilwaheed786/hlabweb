using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_receivers
    {
        IEnumerable<hlab_receivers> GetAllReceivers();
    }
}

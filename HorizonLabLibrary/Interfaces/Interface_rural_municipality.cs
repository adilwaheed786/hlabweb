using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_rural_municipality
    {
        IEnumerable<hlab_rural_municipalities> GetRuralMunicipalities();
        int AddRuralMunicipality(hlab_rural_municipalities municipality);
    }
}

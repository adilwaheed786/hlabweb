using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface ISelectHtmlToPDFConverter
    {
        MemoryStream ConvertHtmlURLToPDFMemoryStream(string URL);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class WaterBacteriaBulkUploadObject
    {
        public List<WaterBacteriaCsvFile> wbf_list { get; set; }
        public string csv_filename { get; set; }
    }
}

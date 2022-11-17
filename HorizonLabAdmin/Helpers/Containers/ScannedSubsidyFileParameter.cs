using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class ScannedSubsidyFileParameter
    {
        public IFormFile scanned_pdf_file { get; set; }
        public hlab_water_subsidy_form_images database_image_file { get; set; }
        public int subsidy_image_id { get; set; }
        public string output_message { get; set; }
        public string saved_path { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class FileUploadParameter
    {
        public IFormFile file { get; set; }
        public string required_file_format { get; set; }
        public string save_path { get; set; }
        public int trans_id { get; set; }
        public int image_db_id { get; set; }
    }
}

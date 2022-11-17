using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Entities
{
    public class hlab_services
    {
        [Required, Key]
        public int id { get; set; }

        [Required]
        public string service_name { get; set; }

        public string description { get; set;}

        [Required]
        public bool status { get; set; }

        public string image_file_name { get; set; }
    }
}

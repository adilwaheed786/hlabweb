using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HorizonLabLibrary.Entities
{
    public class hlab_web_gallery
    {
        [Required, Key]
        public int id { get; set; }

        [Required]
        public string image_file_name { get; set; }

        [Required]
        public DateTime upload_date { get; set; }

        [Required]
        public int upload_by { get; set; }

        [Required]
        public byte status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Entities
{
    public class hlab_web_news
    {
        [Required, Key]
        public int id { get; set; }

        [Required]
        public string section_name { get; set; }

        [Required]
        public string section_content { get; set; }
    }
}

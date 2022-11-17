using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_cities
    {
        [Required, Key]
        public int id { get; set; }
        public string city { get; set; }
        public int province_id { get; set; }
    }
}

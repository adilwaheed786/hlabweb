using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_descriptions
    {
        [Required, Key]
        public int id { get; set; }
        public string description { get; set; }
    }
}

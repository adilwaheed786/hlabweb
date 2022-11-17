using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_sample_types
    {
        [Required, Key]
        public int id { get; set; }
        public string sample_type { get; set; }
    }
}

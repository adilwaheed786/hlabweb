using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_default_parameter_category
    {
        [Required, Key]
        public int id { get; set; }
        public int pkg_id { get; set; }
        public int category_id { get; set; }
    }
}

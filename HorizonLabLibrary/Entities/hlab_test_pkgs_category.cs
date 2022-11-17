using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_pkgs_category
    {
        [Required, Key]
        public int category_id { get; set; }
        public string package_ctgry { get; set; }
        public bool status { get; set; }
    }
}

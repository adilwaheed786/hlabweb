using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_pkgs_class
    {
        [Required, Key]
        public int class_id { get; set; }
        public string pkg_class { get; set; }
        public string hl_code_prefix { get; set; }
    }
}

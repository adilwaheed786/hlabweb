using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_pkgs_forms
    {
        [Required, Key]
        public int id { get; set; }
        public string form_name { get; set; }
    }
}

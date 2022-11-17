using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_params
    {
        [Required, Key]
        public int param_id { get; set; }        
        public string param_name { get; set; }        
        public int pkg_class_id { get; set; }
    }
}

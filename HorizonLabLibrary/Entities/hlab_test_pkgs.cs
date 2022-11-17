using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_pkgs
    {
        [Required, Key]
        public int id { get; set; }
        public string lab_code { get; set; }
        public decimal? price { get; set; }
        public decimal? lab_fee { get; set; }
        public string analysis { get; set; }
        public string sample_container { get; set; }
        public string description { get; set; }
        public int pkg_class_id { get; set; }
        public int form_id { get; set; }
        public bool status { get; set; }
        public string gl_accnt_num { get; set; }        
        public string hl_code_prefix { get; set; }        
    }
}

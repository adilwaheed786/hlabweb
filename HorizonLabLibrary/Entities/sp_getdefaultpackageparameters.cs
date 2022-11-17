using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class sp_getdefaultpackageparameters
    {
        [Required, Key]
        public int row_id { get; set; }
        public int defaultparam_id { get; set; }
        public int package_id { get; set; }
        public string lab_code { get; set; }
        public int param_id { get; set; }
        public int unit_measurement_id { get; set; }
        public string param_name { get; set; }
        public string unit_of_measurement { get; set; }
    }
}

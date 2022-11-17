using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_default_pkg_params
    {
        [Required, Key]
        public int id { get; set; }
        public int param_id { get; set; }
        public int pkg_id { get; set; }
        public int unit_measurement_id { get; set; }

    }
}

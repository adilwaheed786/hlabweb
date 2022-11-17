using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_measurement_units
    {
        [Required, Key]
        public int id { get; set; }
        public string unit_of_measurement { get; set; }
    }
}

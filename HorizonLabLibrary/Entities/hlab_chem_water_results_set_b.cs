using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_chem_water_results_set_b
    {
        [Required, Key]
        public int id { get; set; }
        public int trans_id { get; set; }
        public string description { get; set; }
        public string nitrate_nitrite_info { get; set; }
        public string ph_info { get; set; }
        public string conductivity_info { get; set; }
        public string sodium_info { get; set; }
        public string flouride_info { get; set; }
        public string chloride_info { get; set; }
        public string sulfate_info { get; set; }

    }
}

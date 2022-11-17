using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_chem_water_results_set_a
    {
        [Required, Key]
        public int id { get; set; }
        public string description { get; set; }
        public string nitrate { get; set; }
        public string nitrite { get; set; }
        public string ph { get; set; }
        public string conductivity { get; set; }
        public string sodium { get; set; }
        public string flouride { get; set; }
        public string chloride_tr { get; set; }
        public string chloride_df { get; set; }
        public string chloride_fr { get; set; }
        public string sulfate_tr { get; set; }
        public string sulfate_df { get; set; }
        public string sulfate_fr { get; set; }
        public int trans_id { get; set; }

    }
}

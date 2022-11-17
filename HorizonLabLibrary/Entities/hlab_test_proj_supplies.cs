using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_proj_supplies
    {
        [Required, Key]
        public int id { get; set; }
        public int proj_form_id { get; set; }
        public int supply_id { get; set; }
    }
}

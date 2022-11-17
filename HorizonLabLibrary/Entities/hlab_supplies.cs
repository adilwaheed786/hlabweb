using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_supplies
    {
        [Required, Key]
        public int id { get; set; }

        public string name { get; set; }
	    public string lot { get; set; }
        public DateTime expiry_date { get; set; }
        public int? hours_tolerance_start { get; set; }
        public int? hours_tolerance_end { get; set; }
        public bool status { get; set; }
    }
}

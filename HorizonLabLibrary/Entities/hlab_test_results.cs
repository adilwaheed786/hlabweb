using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_results
    {
        [Required, Key]
        public int result_id { get; set; }
        public int param_id { get; set; }
        public string result { get; set; }
        public int unit_id { get; set; }
        public int trans_id { get; set; }
        public string test_note { get; set; }
        public bool is_failed { get; set; }
        //public bool status { get; set; }
    }
}

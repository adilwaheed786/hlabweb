﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class testresultsview
    {
        public int result_id { get; set; }
        public int? param_id { get; set; }
        public string param_name { get; set; }
        public string result { get; set; }
        public int? unit_id { get; set; }
        public string unit_of_measurement { get; set; }
        public int trans_id { get; set; }
        public string test_note { get; set; }
        public string idnty_location { get; set; }
        public bool is_failed { get; set; }
        public bool status { get; set; }
    }
}

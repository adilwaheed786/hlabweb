using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class testrequestsupplyview
    {
        public int transaction_id { get; set; }
        public int supply_id { get; set; }
        public string name { get; set; }
        public string lot { get; set; }
        public DateTime? expiry_date { get; set; }
        public int form_id { get; set; }
        public int order_id { get; set; }
        public int hours_tolerance_start { get; set; }
        public int hours_tolerance_end { get; set; }
    }
}

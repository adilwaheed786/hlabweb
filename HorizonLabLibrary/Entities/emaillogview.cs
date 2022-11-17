using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class emaillogview
    {
        public int id { get; set; }
        public int email_template_id { get; set; }
        public string email_recepient { get; set; }
        public DateTime date_sent { get; set; }
        public string remarks { get; set; }
        public bool status { get; set; }
        public string template_name { get; set; }
        public string template_subject { get; set; }
    }
}

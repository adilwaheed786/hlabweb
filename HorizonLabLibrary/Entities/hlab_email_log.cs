using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_email_log
    {
        [Key,Required]
        public int id { get; set;}
        public int email_template_id { get; set;}
        public string email_recepient { get; set; }
        public DateTime date_sent { get; set; }
        public string remarks { get; set; }
        public bool status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_email_templates
    {
        [Required, Key]
        public int id { get; set; }
        public string template_name { get; set; }
        public string template_message { get; set; }
        public DateTime date_last_modified { get; set; }
        public bool status { get; set; }
        public string template_subject { get; set; }
    }
}

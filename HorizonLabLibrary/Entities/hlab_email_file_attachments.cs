using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_email_file_attachments
    {
        [Required, Key]
        public int id { get; set; }
        public int email_template_id { get; set; }
        public string file_name { get; set; }
    }
}
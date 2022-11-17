using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class EmailTemplate
    {
        public List<hlab_email_templates> hlab_email_template_list { get; set; }
        public hlab_email_templates hlab_email_template { get; set; }
        public hlab_email_templates new_hlab_email_template { get; set; }
        public List<hlab_email_file_attachments> file_attachment_list { get; set; }
    }
}

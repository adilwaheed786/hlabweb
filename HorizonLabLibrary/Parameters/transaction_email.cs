using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class transaction_email: testtransactionsview
    {
        public string subject { get; set; }
        public string body { get; set; }
        public List<Attachment> file_attachments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class emaildetails
    {
        public List<testtransactionsview> transactionlist { get; set; }
        public List<transaction_email> transaction_email { get; set; }
        public List<Attachment> filelist { get; set; }
        public string message { get; set; }
        public string subject { get; set; }
        public string email { get; set; }        
    }
}

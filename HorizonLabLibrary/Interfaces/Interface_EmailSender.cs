using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_EmailSender
    {
        bool SendEMailByTestTransaction(emaildetails emaildetails);
        bool SendEMail(emaildetails emaildetails);
        List<emaillogview> GetAllEmailLogs();
        void LogEmail(hlab_email_log emaillog);
        IEnumerable<hlab_email_templates> GetAllEmailTemplates();
        IEnumerable<hlab_email_file_attachments> GetEmailFileAttachments(hlab_email_file_attachments efa);
        int InsertNewEmailTemplate(hlab_email_templates template);
        bool InsertFileAttachment(hlab_email_file_attachments file);
        bool UpdateEmailTemplate(hlab_email_templates template);
        bool DeleteAllFileAttachments(hlab_email_file_attachments efa);
    }
}

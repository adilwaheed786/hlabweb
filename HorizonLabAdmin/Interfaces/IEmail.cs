using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Interfaces
{
    public interface IEmail
    {
        SelectList GenerateEmailTemplateSelectListItems();
        hlab_email_templates GetDatabaseEmailTemplates(int email_template_id);
        List<hlab_email_file_attachments> GetDatabaseDefaultEmailAttachments(int template_id);
        bool IsEmailNotEmpty(string email);
        bool ProcessRequestItemsForEmail(TestTransactionSearchParameters transaction_param, List<orderdetailsview> request_item_list);
        bool SendEmailTestTransaction(emaildetails email);
        transaction_email PrepareTestTransactionEmailsToSend(TransactionEmailParam param);
    }
}

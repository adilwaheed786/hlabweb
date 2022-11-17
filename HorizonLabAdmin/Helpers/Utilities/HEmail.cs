using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Interfaces;
using HorizonLabAdmin.Interfaces.Session;
using HorizonLabAdmin.Models.Forms;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public class HEmail: IEmail
    {
        private readonly IUtility _utility;
        private readonly ICertificate _certificate;
        private readonly Interface_EmailSender _hlabEmailSender;
        private readonly IHorizonLabSession _sessionHelper;
        private readonly ITransaction _transactionHelper;
        private readonly IHostingEnvironment _hosting_environment;
        private readonly ISelectHtmlToPDFConverter _HtmlToPDF;
        private readonly ILogger<HEmail> _logger;
        private readonly Interface_hlab_orders _hlabOrderRepo;
        private readonly Interface_test_transactions _hlabTransRepo;

        public HEmail(
            ILogger<HEmail> logger, IHostingEnvironment hosting_environment, 
            ITransaction transactionHelper, Interface_EmailSender hlabEmailSender, 
            IHttpContextAccessor httpContextAccessor, IHorizonLabSession sessionHelper, 
            IUtility utility, ICertificate certificate, Interface_hlab_orders hlabOrderRepo,
            Interface_test_transactions hlabTransRepo, ISelectHtmlToPDFConverter HtmlToPDF)
        {
            _hlabEmailSender = hlabEmailSender;
            _sessionHelper = sessionHelper;
            _transactionHelper = transactionHelper;
            _hosting_environment = hosting_environment;
            _utility = utility;
            _certificate = certificate;
            _hlabOrderRepo = hlabOrderRepo;
            _logger = logger;
            _hlabTransRepo = hlabTransRepo;
            _HtmlToPDF = HtmlToPDF;
        }

        public SelectList GenerateEmailTemplateSelectListItems()
        {
            try
            {
                List<hlab_email_templates> email_templates = _hlabEmailSender.GetAllEmailTemplates().Where(x => x.status == true).ToList();
                if (email_templates != null && email_templates.Count > 0) email_templates = email_templates.OrderBy(x => x.template_name).ToList();
                SelectList selectEmailTemplate = new SelectList(email_templates, "id", "template_name");
                return selectEmailTemplate;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > GenerateEmailTemplateSelectListItems(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public List<hlab_email_file_attachments> GetDatabaseDefaultEmailAttachments(int email_template_id)
        {
            try
            {
                List<hlab_email_file_attachments> attachment_list = new List<hlab_email_file_attachments>();
                hlab_email_file_attachments email_file_attachment = new hlab_email_file_attachments();
                email_file_attachment.email_template_id = email_template_id;
                attachment_list = _hlabEmailSender.GetEmailFileAttachments(email_file_attachment).ToList();
                return attachment_list;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > GetDatabaseDefaultEmailAttachments(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public hlab_email_templates GetDatabaseEmailTemplates(int email_template_id)
        {
            try
            {
                return _hlabEmailSender.GetAllEmailTemplates().Where(x => x.id == email_template_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > GetDatabaseEmailTemplates(): {exc.Message}");
                throw exc.InnerException;
            }            
        }

        public bool IsEmailNotEmpty(string email)
        {
            return !string.IsNullOrEmpty(email);
        }

        public transaction_email PrepareTestTransactionEmailsToSend(TransactionEmailParam param)
        {
            try
            {
                transaction_email transaction_email_param = new transaction_email();
                MemoryStream memory_stream = new MemoryStream();
                List<hlab_email_file_attachments> attachment_list = new List<hlab_email_file_attachments>();
                string attachment_files_loc = Directory.GetCurrentDirectory() + "\\wwwroot\\email_file_attachments\\";
                string subsidy_files_loc = Directory.GetCurrentDirectory() + "\\wwwroot\\scan_subsidy_forms\\";
                string reportname = "", CertRequestURL="";

                CertRequestURL = _certificate.GenerateB1NSCertificateRequestURL(param.request_scheme, param.reques_host, param.requets_id, param.test_pkg_id);
                transaction_email_param = _transactionHelper.InitTransactionParameter(param.transactiondetails, param.email_template);
                attachment_list = GetDatabaseDefaultEmailAttachments(param.email_template.id);

                reportname = $"water_sample_certificate_{transaction_email_param.trans_id.ToString()}";

                memory_stream = _HtmlToPDF.ConvertHtmlURLToPDFMemoryStream(CertRequestURL);
                memory_stream.Position = 0;

                Attachment file = new Attachment(memory_stream, $"{reportname}.pdf", "application/pdf");
                transaction_email_param.file_attachments = new List<Attachment>();
                transaction_email_param.file_attachments.Add(file);

                //if(param.transactiondetails.gen_coupon!=null && param.transactiondetails.gen_coupon != 0)
                foreach(var coupon in param.coupons)
                {
                    Attachment coupon_attachment_file = GenerateCouponAttachment(param.request_scheme, param.reques_host, (int)coupon);
                    transaction_email_param.file_attachments.Add(coupon_attachment_file);
                }

                //if (param.transactiondetails.subsidyimage_id != 0 && !string.IsNullOrEmpty(param.transactiondetails.scan_image_filename))
                foreach(var subsidy in param.subsidy_files)
                {
                    //Attachment subsidy_attachment_file = GenerateSubsidyAttachment(param.request_scheme, param.reques_host, param.transactiondetails.scan_image_filename, param.transactiondetails.trans_id);
                    Attachment subsidy_attachment_file = new Attachment($"{subsidy_files_loc}{subsidy}", "application/pdf");
                    transaction_email_param.file_attachments.Add(subsidy_attachment_file);
                }

                foreach (var attachment_file in attachment_list)
                {
                    try
                    {
                        Attachment fa = new Attachment($"{attachment_files_loc}{attachment_file.file_name}", "application/pdf");
                        transaction_email_param.file_attachments.Add(fa);
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError($"EMAIL WARNING: attachment {attachment_file.file_name} doesn't exists in the server!");
                        throw exc.InnerException;
                    }
                }
                return transaction_email_param;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > PrepareTestTransactionEmailsToSend(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        private Attachment GenerateCouponAttachment(string scheme, string host, int coupon_id)
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream();
                string coupon_pdf_name = "", RequestURL = "";
                coupon_pdf_name = $"coupon_{coupon_id.ToString()}";
                RequestURL = _certificate.GenerateCouponCertificateRequestURL(scheme, host, coupon_id);
                memory_stream = _HtmlToPDF.ConvertHtmlURLToPDFMemoryStream(RequestURL);
                memory_stream.Position = 0;
                Attachment file = new Attachment(memory_stream, $"{coupon_pdf_name}.pdf", "application/pdf");
                return file;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > GenerateCouponAttachment(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        private Attachment GenerateSubsidyAttachment(string scheme, string host, string subsidy_file_name, int transactionid)
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream();
                string new_subsidy_pdf = "", RequestURL = "";
                new_subsidy_pdf = $"subsidy_{transactionid.ToString()}";
                RequestURL = _certificate.GenerateSubsidyImageRequestURL(scheme, host, subsidy_file_name);                
                memory_stream = _HtmlToPDF.ConvertHtmlURLToPDFMemoryStream(RequestURL);
                memory_stream.Position = 0;
                Attachment file = new Attachment(memory_stream, $"{new_subsidy_pdf}.pdf", "application/pdf");
                return file;
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > GenerateCouponAttachment(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool ProcessRequestItemsForEmail(TestTransactionSearchParameters transaction_param, List<orderdetailsview> request_item_list)
        {
            try
            {
                emaildetails emaildetails = new emaildetails();
                hlab_email_templates email_template = new hlab_email_templates();
                List<hlab_email_file_attachments> attachment_list = new List<hlab_email_file_attachments>();
                orderdetailsview email_request_item = new orderdetailsview();
                int email_template_id = 0;
                int? transaction_id = 0;
                string RequestURL = "";
                bool isEmailReady = false;
                emaildetails.transaction_email = new List<transaction_email>();
                List<string> subsidy_files =new List<string>();
                List<int> coupons = new List<int>();

                if (request_item_list == null && request_item_list.Count < 0) return false;

                foreach (var rqst in request_item_list)
                {
                    transaction_id = rqst.trans_id;
                    var transactiondetails = _transactionHelper.GetTransactionInformationFromSp((int)transaction_id);

                    if (!string.IsNullOrEmpty(transactiondetails.scan_image_filename)) subsidy_files.Add(transactiondetails.scan_image_filename);
                    if (transactiondetails.gen_coupon != null && transactiondetails.gen_coupon != 0) coupons.Add((int)transactiondetails.gen_coupon);
                }

                transaction_id = 0;
                foreach (var item in request_item_list)
                {
                    email_template_id = item.email_template_id;
                    if (item.trans_id == 0 || item.trans_id == null) break;
                    transaction_id = item.trans_id;
                    email_template = GetDatabaseEmailTemplates(email_template_id);
                    attachment_list = GetDatabaseDefaultEmailAttachments(email_template_id);
                    var transactiondetails = _transactionHelper.GetTransactionInformationFromSp((int)transaction_id);

                    _hlabOrderRepo.UpdateOrderItemSentEmail(new hlab_order_items {
                        id = item.order_item_id,
                        date_email_sent = DateTime.Now,
                        email_tpl_id = email_template_id
                    });
                    if(item.trans_id!=null && item.trans_id!=0) _hlabTransRepo.PublishTestTransaction((int)item.trans_id);

                    if (IsEmailNotEmpty(transactiondetails.email) && !isEmailReady)
                    {
                        //RequestURL = _certificate.GenerateB1NSCertificateRequestURL(transaction_param.request_scheme, transaction_param.request_host, item.order_id, (int)item.pkg_id);
                        emaildetails.transaction_email.Add(PrepareTestTransactionEmailsToSend(new TransactionEmailParam
                        {
                            transactiondetails = transactiondetails,
                            email_template = email_template,
                            request_scheme = transaction_param.request_scheme,
                            reques_host = transaction_param.request_host,
                            requets_id = item.order_id,
                            test_pkg_id = (int)item.pkg_id,
                            subsidy_files = subsidy_files,
                            coupons = coupons
                        }));
                        isEmailReady = true;
                    }
                }

                return SendEmailTestTransaction(emaildetails);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > ProcessRequestItemsForEmail(): {exc.Message}");
                throw exc.InnerException;
            }
        }

        public bool SendEmailTestTransaction(emaildetails email)
        {
            try
            {
                return _hlabEmailSender.SendEMailByTestTransaction(email);
            }
            catch (Exception exc)
            {
                _logger.LogError($"HEmail > SendEmailTestTransaction(): {exc.Message}");
                throw exc.InnerException;
            }
        }
    }
}

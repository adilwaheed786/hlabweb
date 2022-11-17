using HorizonLabLibrary;
using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class HlabEmailSender : Interface_EmailSender
    {
        private WebApiLibrary _hllWebApi = new WebApiLibrary();
        private readonly ILogger<HlabEmailSender> _logger;
        private HorizonLabCustomerLibrary _hllCustomerLib = new HorizonLabCustomerLibrary();
        private HorizonLabTableReferenceApiLibrary _hllTableReference = new HorizonLabTableReferenceApiLibrary();
        private HorizonLabEmail _hlabEmail = new HorizonLabEmail();
        private readonly Interface_hlab_customers _hlabCustomerRepo;
        private IConfiguration _appConfig { get; }
        private string _webApibaseUrl;
        string _hlabApiKey;
        string _ApiHeader;
        private string _smtpServer;
        private string _email;
        private string _password;
        private string _testemail;
        private string _testemail2;
        private int _port;

        public HlabEmailSender(IConfiguration appConfig, ILogger<HlabEmailSender> logger, Interface_hlab_customers hlabCustomerRepo)
        {
            _appConfig = appConfig;
            _logger = logger;
            _hlabCustomerRepo = hlabCustomerRepo;
            _webApibaseUrl = _appConfig["AppSettings:HlabWebApiBaseUrl"];
            _hlabApiKey = _appConfig["AppSettings:HlabApiKey"];
            _ApiHeader = _appConfig["AppSettings:ApiHeaderKey"];
            _smtpServer = _appConfig["AppSettings:SMTPServer"];
            _email = _appConfig["AppSettings:email"];
            _password = _appConfig["AppSettings:password"];
            _testemail = _appConfig["AppSettings:testemail"];
            _testemail2 = _appConfig["AppSettings:testemail2"];
            _port = Convert.ToInt32(_appConfig["AppSettings:port"]);
        }

        public bool SendEMailByTestTransaction(emaildetails emaildetails)
        {
            try
            {
                foreach (var transaction in emaildetails.transaction_email)
                {
                    try
                    {
                        List<hlab_customer_email> email_list = new List<hlab_customer_email>();
                        List<string> str_email_list = new List<string>();

                        if (!string.IsNullOrEmpty(transaction.email))
                        {
                            var credentials = new NetworkCredential(new MailAddress(_email).Address, _password);
                            var mail = new MailMessage()
                            {
                                From = new MailAddress(_email),
                                Subject = transaction.subject,
                                Body = transaction.body
                            };

                            mail.IsBodyHtml = true;                         

                            if(!string.IsNullOrEmpty(_testemail) && !string.IsNullOrEmpty(_testemail2))
                            {
                                //mail.To.Add(new MailAddress(transaction.email));
                                //for testing only                            
                                mail.To.Add(new MailAddress(_testemail));
                                mail.To.Add(new MailAddress(_testemail2));

                                str_email_list.Add(_testemail);
                                str_email_list.Add(_testemail2);
                            }
                            else
                            {
                                email_list = _hlabCustomerRepo.GetCustomerEmail(new hlab_customers { customer_id = transaction.customer_id }).ToList();
                                if (email_list != null && email_list.Count > 0)
                                {
                                    foreach (var email_item in email_list)
                                    {
                                        if (!string.IsNullOrEmpty(email_item.email))
                                        {
                                            mail.To.Add(new MailAddress(email_item.email));
                                            str_email_list.Add(email_item.email);
                                        }                                            
                                    }
                                }
                            }

                            if (transaction.file_attachments.Count > 0)
                            {
                                foreach (var file in transaction.file_attachments)
                                {
                                    mail.Attachments.Add(file);
                                }
                            }

                            var client = new SmtpClient()
                            {
                                Host = _smtpServer,
                                Port = _port,
                                EnableSsl = false,
                                UseDefaultCredentials = false,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                Credentials = credentials
                            };

                            hlab_email_log emaillog = new hlab_email_log();
                            try
                            {
                                client.Send(mail);
                                emaillog.remarks = $"Transaction ID: {transaction.trans_id} | Email Successfully Sent!";
                                emaillog.status = true;
                            }
                            catch (Exception exc)
                            {
                                emaillog.remarks = $"Transaction ID: {transaction.trans_id} | Sending Email Failed: " + exc.Message;
                                emaillog.status = false;
                            }

                            //log emails
                            foreach (var str_email in str_email_list)
                            {
                                emaillog.date_sent = DateTime.Now;
                                emaillog.email_recepient = str_email;
                                emaillog.email_template_id = transaction.email_template_id;
                                _hlabEmail.LogEmail(emaillog, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError("HlabEmailSender > SendEMailByTestTransaction Error: " + exc.Message + " | " + exc.InnerException);
                        return false;
                    }
                }                

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool SendEMail(emaildetails emaildetails)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<hlab_email_templates> GetAllEmailTemplates()
        {
            var jsonList = _hlabEmail.GetAllEmailTemplates(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var templates = JsonConvert.DeserializeObject<List<hlab_email_templates>>(jsonList);
            return templates;
        }

        public IEnumerable<hlab_email_file_attachments> GetEmailFileAttachments(hlab_email_file_attachments efa)
        {
            var jsonList = _hlabEmail.GetEmailFileAttachments(efa, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            var attachments = JsonConvert.DeserializeObject<List<hlab_email_file_attachments>>(jsonList);
            return attachments;
        }

        public int InsertNewEmailTemplate(hlab_email_templates template)
        {
            var result = _hlabEmail.AddNewTemplate(template, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            return Convert.ToInt32(result);
        }

        public bool InsertFileAttachment(hlab_email_file_attachments file)
        {
            var result = _hlabEmail.AssignFileAttachments(file, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool UpdateEmailTemplate(hlab_email_templates template)
        {
            var result = _hlabEmail.UpdateTemplate(template, _webApibaseUrl, _hlabApiKey, _ApiHeader);
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "success")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteAllFileAttachments(hlab_email_file_attachments efa)
        {
            try
            {
                _hlabEmail.DeleteAllAttachment(efa, _webApibaseUrl, _hlabApiKey, _ApiHeader);
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public List<emaillogview> GetAllEmailLogs()
        {
            var jsonList = _hlabEmail.GetAllEmailLogs(_webApibaseUrl, _hlabApiKey, _ApiHeader);
            var logs = JsonConvert.DeserializeObject<List<emaillogview>>(jsonList);
            return logs;
        }

        public void LogEmail(hlab_email_log emaillog)
        {
            _hlabEmail.LogEmail(emaillog, _webApibaseUrl, _hlabApiKey, _ApiHeader);
        }
    }
}

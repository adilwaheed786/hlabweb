using HorizonLabLibrary.Entities;
using HorizonLabLibrary.Interfaces;
using HorizonLabLibrary.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HorizonLabWebApi.Models
{
    public class HlabEmailSender : Interface_EmailSender
    {
        private IConfiguration _appConfig { get; }
        private readonly ILogger<HlabEmailSender> _logger;
        private readonly HorizonLabDbContext _hlab_Db_Context;
        private string _smtpServer;
        private string _email;
        private string _password;
        private string _testemail;
        private string _testemail2;
        private int _port;

        public HlabEmailSender(IConfiguration appConfig, ILogger<HlabEmailSender> logger, HorizonLabDbContext hlab_Db_Context)
        {
            _appConfig = appConfig;
            _smtpServer = _appConfig["AppSettings:SMTPServer"];
            _email = _appConfig["AppSettings:email"];
            _password = _appConfig["AppSettings:password"];
            _testemail = _appConfig["AppSettings:testemail"];
            _testemail2 = _appConfig["AppSettings:testemail2"];
            _port = Convert.ToInt32(_appConfig["AppSettings:port"]);
            _logger = logger;
            _hlab_Db_Context = hlab_Db_Context;
        }

        public void LogEmail(hlab_email_log emaillog)
        {
            try
            {
                _hlab_Db_Context.hlab_email_log.Add(emaillog);
                _hlab_Db_Context.SaveChanges();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
            }

        }

        public bool SendEMailByTestTransaction(emaildetails emaildetails)
        {
            throw new NotImplementedException();
            //List<hlab_email_log> emailloglist = new List<hlab_email_log>();
            //foreach (var transaction in emaildetails.transactionlist)
            //{
            //    try
            //    {
            //        if (!string.IsNullOrEmpty(transaction.email))
            //        {
            //            var credentials = new NetworkCredential(new MailAddress(_email).Address, _password);
            //            var mail = new MailMessage()
            //            {
            //                From = new MailAddress(_email),
            //                Subject = emaildetails.subject,
            //                Body = emaildetails.message
            //            };

            //            mail.IsBodyHtml = true;
            //            //mail.To.Add(new MailAddress(transaction.email));

            //            //for testing only
            //            mail.To.Add(new MailAddress(_testemail2));
            //            mail.To.Add(new MailAddress(_testemail));

            //            if(emaildetails.filelist.Count > 0)
            //            {
            //                foreach(var file in emaildetails.filelist)
            //                {
            //                    mail.Attachments.Add(file);
            //                }
            //            }

            //            var client = new SmtpClient()
            //            {
            //                Host = _smtpServer,
            //                Port = _port,
            //                EnableSsl = false,
            //                UseDefaultCredentials = false,
            //                DeliveryMethod = SmtpDeliveryMethod.Network,
            //                Credentials = credentials
            //            };

            //            hlab_email_log emaillog = new hlab_email_log();
            //            try
            //            {
            //                client.Send(mail);
            //                emaillog.remarks = $"Transaction ID: {transaction.trans_id} | Email Successfully Sent!";
            //                emaillog.status = true;
            //            }
            //            catch (Exception exc)
            //            {
            //                emaillog.remarks = $"Transaction ID: {transaction.trans_id} | Sending Email Failed: " + exc.Message;
            //                emaillog.status = false;
            //            }
                        
            //            emaillog.date_sent = DateTime.Now;
            //            emaillog.email_recepient = transaction.email;
            //            emailloglist.Add(emaillog);
            //        }                    
            //    }
            //    catch (Exception exc)
            //    {
            //        _logger.LogError("HlabEmailSender > SendEMailByTestTransaction Error: " + exc.InnerException);
            //        return false;
            //    }
            //}
            //_hlab_Db_Context.hlab_email_log.AddRange(emailloglist);
            //_hlab_Db_Context.SaveChanges();

            //return true;
        }

        public bool SendEMail(emaildetails emaildetails)
        {
            try
            {
                var credentials = new NetworkCredential(new MailAddress(_email).Address, _password);
                var mail = new MailMessage()
                {
                    From = new MailAddress(_email),
                    Subject = emaildetails.subject,
                    Body = emaildetails.message
                };

                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(emaildetails.email));

                var client = new SmtpClient()
                {
                    Host = _smtpServer,
                    Port = _port,
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = credentials
                };

                client.Send(mail);
                _logger.LogInformation("Message was sent successfully to: " + emaildetails.email + " at " + DateTime.Now);
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError("MODEL sendEMail failed to send to  - " + emaildetails.email + ", Error: " + exc.InnerException);
                return false;
            }
        }

        public IEnumerable<hlab_email_templates> GetAllEmailTemplates()
        {
            try
            {
                return _hlab_Db_Context.hlab_email_templates.ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public IEnumerable<hlab_email_file_attachments> GetEmailFileAttachments(hlab_email_file_attachments efa)
        {
            try
            {
                return _hlab_Db_Context.hlab_email_file_attachments.Where(x=>x.email_template_id==efa.email_template_id).ToList();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }

        public int InsertNewEmailTemplate(hlab_email_templates template)
        {
            try
            {
                _hlab_Db_Context.hlab_email_templates.Add(template);
                _hlab_Db_Context.SaveChanges();
                return template.id;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return 0;
            }
        }

        public bool InsertFileAttachment(hlab_email_file_attachments file)
        {
            try
            {
                //insert new set of attachments
                _hlab_Db_Context.hlab_email_file_attachments.Add(file);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool UpdateEmailTemplate(hlab_email_templates template)
        {
            try
            {
                _hlab_Db_Context.hlab_email_templates.Update(template);
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public bool DeleteAllFileAttachments(hlab_email_file_attachments efa)
        {
            try
            {
                // remove all attachment
                _hlab_Db_Context.hlab_email_file_attachments.RemoveRange(_hlab_Db_Context.hlab_email_file_attachments.Where(x => x.email_template_id == efa.email_template_id));
                _hlab_Db_Context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }
        }

        public List<emaillogview> GetAllEmailLogs()
        {
            try
            {
                return _hlab_Db_Context.emaillogview.ToList();
            }
            catch(Exception exc)
            {
                _logger.LogError(exc.Message);
                return null;
            }
        }
    }
}

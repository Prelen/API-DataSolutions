﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Model.Utilities;
using DataSolution.Domain.Interfaces.Utilities;
using DataSolution.Domain.Interfaces.Utilities.Mail;
using System.Net.Mail;
using System.Configuration;
using DataSolution.Utilities.Logging;

namespace DataSolution.Utilities.Mail
{
    public  class Email : IMail
    {
        private  readonly string host = ConfigurationManager.AppSettings["SMTP"].ToString();
        private readonly Logger log = new Logger();
        private  SmtpClient smtp;
        public bool SendEmail(Domain.Model.Utilities.Email Email)
        {
            bool result = false;

            try
            {
                smtp = new SmtpClient(host);
                MailMessage mail = new MailMessage();

                foreach (var address in Email.ToEmail)
                {
                    mail.To.Add(address);
                }
                mail.From = new MailAddress(Email.FromAddress);
                mail.IsBodyHtml = true;
                mail.Subject = Email.Subject;
                mail.Body = Email.EmailMessage;
                smtp.Send(mail);
                result = true;
            }
            catch (Exception ex)
            {

                log.LogError(Email.FromAddress, "DataSolutions.Utlities", "SendEmail", ex.Message);
            }
    
            return result;

        }

        public bool SendEmailWithAttachment(Domain.Model.Utilities.Email Email)
        {
            bool result = false;

            try
            {
                smtp = new SmtpClient(host);
                MailMessage mail = new MailMessage();

                foreach (var address in Email.ToEmail)
                {
                    mail.To.Add(address);
                }
                mail.From = new MailAddress(Email.FromAddress);
                mail.IsBodyHtml = true;
                mail.Subject = Email.Subject;
                mail.Body = Email.EmailMessage;

                
                foreach (var attachment in Email.Attachment)
                {
                    mail.Attachments.Add(new Attachment(attachment));
                }
                
                smtp.Send(mail);
                result = true;
            }
            catch (Exception ex)
            {

                log.LogError(Email.FromAddress, "DataSolutions.Utlities", "SendEmailWithAttachment", ex.Message);
            }

            return result;
        }
    }
}

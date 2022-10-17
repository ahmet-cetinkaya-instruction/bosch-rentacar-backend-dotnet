using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Core.Business.Mailing.MailKit
{
    public class MailKitMailService : IMailService
    {
        private MailSettings _mailSettings;

        public MailKitMailService(IConfiguration configuration)
        {
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public void SendMail(Mail mail)
        {
            MimeMessage email = new();
            email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderMail));
            email.To.Add(new MailboxAddress(mail.ToFullName,mail.ToMail));
            email.Subject = mail.Subject;
            BodyBuilder bodyBuilder = new()
            {
                TextBody = mail.TextBody,
                HtmlBody = mail.HtmlBody,
            };
            //if (mail.Attachments is not null)
            //{
            //    foreach (var attachment in mail.Attachments)
            //    {
            //        bodyBuilder.Attachments.Add(attachment);
            //    }
            //}
            email.Body = bodyBuilder.ToMessageBody();

            using SmtpClient smtp = new();
            smtp.Connect(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

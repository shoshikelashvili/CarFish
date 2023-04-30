using CarFish.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Model
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = "CarFish Message Alert";
            var builder = new BodyBuilder();

            builder.HtmlBody = "გამომგზავნის სახელი: " + mailRequest.Name;
            builder.HtmlBody += "<br/>გამომგზავნის ნომერი: " + mailRequest.Phone;
            builder.HtmlBody += "<br/>გამომგზავნის ელ-ფოსტა: " + mailRequest.Email;
            builder.HtmlBody += "<br/>გამომგზავნის მესიჯი:" + mailRequest.Message;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}

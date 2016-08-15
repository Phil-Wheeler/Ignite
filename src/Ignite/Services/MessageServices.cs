using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;


namespace Ignite.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress("Dunedin Ignite!", "phil.wheeler@outlook.co.nz"));
            mail.To.Add(new MailboxAddress(email, email));
            mail.Subject = subject;

            mail.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.sendgrid.net", 465, SecureSocketOptions.SslOnConnect);

                client.Authenticate("dunedinignite", "-Zv/YO=Y&l!(3cH}gMD~");

                client.Send(mail);

                client.Disconnect(true);
            }

            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}

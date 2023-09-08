using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
namespace VBookHaven.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BookStore Management System", "EmailAddress"));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = $"{subject}";
            message.Body = new TextPart("html")
            {
                Text = $"{htmlMessage}",
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("", "");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            //return Task.CompletedTask;
            //c2
        }
    }
}

//c2
//MailMessage mail = new MailMessage();
//mail.To.Add(email.ToString().Trim());
//mail.From = new MailAddress("acchunglmhe151273@gmail.com");
//mail.Subject = "Password recovery";
//mail.Body = $"<p>Hi,{htmlMessage}</p><br/>Have a good day!<br/>";
//mail.IsBodyHtml = true;
//SmtpClient smtp = new SmtpClient();
//smtp.Port = 587;
//smtp.EnableSsl = true;
//smtp.UseDefaultCredentials = false;
//smtp.Host = "smtp.gmail.com";
//smtp.Credentials = new System.Net.NetworkCredential("acchunglmhe151273@gmail.com", "");
//smtp.Send(mail);
//return Task.CompletedTask;
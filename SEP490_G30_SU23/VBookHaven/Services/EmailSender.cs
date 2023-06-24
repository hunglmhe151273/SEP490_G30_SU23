using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace VBookHaven.Services
{
    public interface IEmailSender
    {
        void SendForgotPasswordEmailAsync(string name, string email, string newPassword);
    }
    public class EmailSender : IEmailSender
    {
        public void SendForgotPasswordEmailAsync(string name, string email, string newPass)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email.ToString().Trim());
            mail.From = new MailAddress("acchunglmhe151273@gmail.com");
            mail.Subject = "Password recovery";
            mail.Body = $"<p>Hi,<br/> This is VBookHaven. Your new password is: {newPass}<br/> To change password, GO TO this link: https://localhost:7139/account/changepassword</p><br/>Have a good day!<br/>";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("acchunglmhe151273@gmail.com", "");
            smtp.Send(mail);
        }
    }
}

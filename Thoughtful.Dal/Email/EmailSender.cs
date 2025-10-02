

using System.Net.Mail;
using System.Net;

namespace Thoughtful.Dal.Email
{
    public class EmailSender
    {
        public static string _fromMail { get; set; }
        public static string _fromPassword { get; set; }
        public EmailSender(string fromEmail, string  password)
        {
            _fromMail = fromEmail;
            _fromPassword = password;
        }
        public static async Task SendEmailAsync(EmailModel data)
        {


            MailMessage message = new MailMessage();
            message.From = new MailAddress(_fromMail);
            message.Subject = data.Subject;
            foreach (var to in data.To)
            {
                message.To.Add(new MailAddress(to));
            }
            message.Body = "<html><body> " + data.Body + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_fromMail, _fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}

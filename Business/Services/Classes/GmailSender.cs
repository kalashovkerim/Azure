using System.Net.Mail;
using System.Net;

namespace Business.Services.Classes
{
    public class GmailSender
    {
        public string TxtTo { get; set; }
        public string TxtFrom { get; set; }
        public string TxtPassword { get; set; }
        public string TxtSubject { get; set; }
        public string TxtBody { get; set; }

        public string TxtConfirmationUrl { get; set; }

        public GmailSender(string _txtTo, string _txtFrom, string _password, string _txtSubject, string _body)
        {
            TxtTo = _txtTo;
            TxtFrom = _txtFrom;
            TxtPassword = _password;
            TxtSubject = _txtSubject;
            TxtBody = _body;
        }
        public void SendAsyncEmail()
        {
            string to = TxtTo;
            string from = TxtFrom;
            string password = TxtPassword;
            string subject = TxtSubject;
            string body = TxtBody;

            using (MailMessage message = new MailMessage(from, to))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(from, password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(message);
            }
            // Send the email confirmation message
            string confirmationUrl = TxtConfirmationUrl;
            string confirmationLink = "<a href='" + confirmationUrl + "'>Confirm your email address</a>";

            using (MailMessage message = new MailMessage(from, to))
            {
                message.Subject = "Please confirm your email address";
                message.Body = "Thank you for registering. To complete your registration, please click the following link: " + confirmationLink;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(from, password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(message);
            }
        }
    }
}

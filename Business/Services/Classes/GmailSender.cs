using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Classes
{
    public class GmailSender
    {
        public string TxtTo { get; set; }
        public string TxtFrom { get; set; }
        public string TxtPassword { get; set; }
        public string TxtSubject { get; set; }
        public string TxtBody { get; set; }

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

            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(from, password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}

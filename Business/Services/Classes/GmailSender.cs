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
        public string txtTo { get; set; }
        public string txtFrom { get; set; }
        public string txtPassword { get; set; }
        public string txtSubject { get; set; }
        public string txtBody { get; set; }

        public GmailSender(string _txtTo, string _txtFrom, string _password, string _txtSubject, string _body)
        {
            txtTo = _txtTo;
            txtFrom = _txtFrom;
            txtSubject = _txtSubject;
            txtPassword = _password;
            txtBody = _body;
        }
        public void SendAsyncEmail()
        {
            string to = txtTo;
            string from = txtFrom;
            string password = txtPassword;
            string subject = txtSubject;
            string body = txtBody;
            //HttpPostedFile postedFile = fuAttachment.PostedFile;

            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;
                /*if (postedFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    mm.Attachments.Add(new Attachment(postedFile.InputStream, fileName));
                }*/
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
            /*ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);*/
        }
    }
}

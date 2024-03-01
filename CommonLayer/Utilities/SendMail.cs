using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using CommonLayer.RequestModel;

namespace CommonLayer.Utilities
{
    public class SendMail
    {
        public string SendEmail(string ToEmail, ForgetPasswordModel Token)
        {
            string FromEmail = "pg6311@srmist.edu.in";
            MailMessage Message = new MailMessage(FromEmail, ToEmail);
            string MailBody = "Token Generated : " + Token;
            Message.Subject = "Token Generated For Resetting Password";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential
                = new NetworkCredential(FromEmail, "fuks qwjz smlq pezr");

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;

            smtp.Send(Message);
            return ToEmail;
        }
    }
}

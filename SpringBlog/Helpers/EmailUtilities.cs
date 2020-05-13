using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SpringBlog.Helpers
{   
    public static class EmailUtilities
    {
        public static async Task SendEmailAsync(string destination,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(destination);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;


            using (var smtpClient = new SmtpClient("iremsoydan.com"))
            {
                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}
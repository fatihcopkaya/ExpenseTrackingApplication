using MasrafTakipYonetim.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MasrafTakipYonetim.Infrastructure.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMail(string toMail, string subject, string mailBody/*, string fromMail, string password*/)
        {
            
            try
            {

                string fromMail = _configuration["MailSettings:FromMail"];
                string password = _configuration["MailSettings:Password"];
                int port = _configuration.GetValue<int>("MailSettings:Port");

                MailMessage msg = new MailMessage();
                msg.Subject = subject;
                msg.From = new MailAddress(fromMail);
                msg.To.Add(new MailAddress(toMail));
                msg.Body = $"{mailBody} <br>";
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //NetworkCredential AccountInfo = new NetworkCredential("masraftakipyonetim23@gmail.com", "zzwg ihkp nifb xcuw");
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = AccountInfo;
                //smtp.EnableSsl = true;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //await smtp.Send(msg);


                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("masraftakipyonetim23@gmail.com", "zzwg ihkp nifb xcuw");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void NotifySystemAdmin()
        {
            Console.WriteLine("Devamı yakında başlayacak...");
        }

        public  async Task SendPasswordResetMailAsync(string toMail,Guid userId,string resetToken)
        {

            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\""); 
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/updatePassword/");
            mail.AppendLine(userId.ToString());
            mail.Append("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\" font-size:12px;\">Not:Eğer ki bu talep tarafınızca gerçekleşmemişse lütfen bu maili ciddiye almayınız.</span><br>");
            //await SendMail(toMail, "Şifre Yenileme Talebi", mail.ToString(), "masraftakipyonetim23@gmail.com", "zzwg ihkp nifb xcuw");
            await SendMail(toMail, "Şifre Yenileme Talebi", mail.ToString());
        }


    
    }
}

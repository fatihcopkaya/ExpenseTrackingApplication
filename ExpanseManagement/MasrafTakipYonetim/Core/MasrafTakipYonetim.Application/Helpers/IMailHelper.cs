using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Helpers
{
    public interface IMailHelper
    {
       // public Task SendMail(string toMail, string subject, string mailBody, string fromMail, string password);
        public Task SendMail(string toMail, string subject, string mailBody);

        public Task SendPasswordResetMailAsync(string toMail, Guid userId, string resetToken);
    }
}

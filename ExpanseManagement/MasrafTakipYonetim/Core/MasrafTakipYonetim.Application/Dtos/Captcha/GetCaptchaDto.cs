using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Captcha
{
    public class GetCaptchaDto
    {
        public string CaptchaImage { get; set; }
        public string CaptchaValue { get; set; }
    }
}

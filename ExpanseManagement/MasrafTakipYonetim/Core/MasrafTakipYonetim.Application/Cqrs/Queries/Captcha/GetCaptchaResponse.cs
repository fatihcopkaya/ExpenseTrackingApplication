using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.Captcha
{
    public class GetCaptchaResponse:IRequest<GetCaptchaRequest>
    {
        public string CaptchaImage { get; set; }
        public string CaptchaValue { get; set; }
    }
}

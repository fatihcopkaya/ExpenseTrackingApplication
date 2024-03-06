using MasrafTakipYonetim.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AuthenticaitonRequestAndResponses
{
    public class PasswordResetCommandRequest:IRequest<Results>
    {
        public string Email { get; set; }
    }
}

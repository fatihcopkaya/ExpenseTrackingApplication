using MasrafTakipYonetim.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AuthenticaitonRequestAndResponses
{
    public class UpdatePasswordCommandRequest:IRequest<Results>
    {
        public Guid UserId { get; set; }
        public string ResetToken {  get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}

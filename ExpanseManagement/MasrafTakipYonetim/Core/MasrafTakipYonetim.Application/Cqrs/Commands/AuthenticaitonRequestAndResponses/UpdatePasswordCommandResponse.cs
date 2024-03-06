using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AuthenticaitonRequestAndResponses
{
    public class UpdatePasswordCommandResponse:IRequest<UpdatePasswordCommandRequest>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

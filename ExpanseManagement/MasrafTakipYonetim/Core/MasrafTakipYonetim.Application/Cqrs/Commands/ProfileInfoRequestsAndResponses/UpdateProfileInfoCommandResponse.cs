using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ProfileInfoRequestsAndResponses
{
    public class UpdateProfileInfoCommandResponse:IRequest<UpdateProfileInfoCommandRequest>
    {
        public string ?Message { get; set; }
    }
}

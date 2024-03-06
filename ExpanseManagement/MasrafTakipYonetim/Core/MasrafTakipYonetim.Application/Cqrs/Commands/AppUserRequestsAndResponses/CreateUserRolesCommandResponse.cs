using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class CreateUserRolesCommandResponse:IRequest<CreateUserRolesCommandRequest>
    {
        public string Message { get; set; }
    }
}

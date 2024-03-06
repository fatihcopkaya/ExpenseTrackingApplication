using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.UserRoleRequestsAndResponses
{
    public class UpdateUserRoleCommandResponse:IRequest<UpdateUserRoleCommandRequest>
    {
        public string Message { get; set; }
    }
}

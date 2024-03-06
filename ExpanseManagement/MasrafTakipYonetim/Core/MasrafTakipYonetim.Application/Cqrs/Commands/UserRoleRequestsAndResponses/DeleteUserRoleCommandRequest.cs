using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.RoleUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.UserRoleRequestsAndResponses
{
    public class DeleteUserRoleCommandRequest:IRequest<Results>
    {
        public Guid Id { get; set; }
       
    }
}

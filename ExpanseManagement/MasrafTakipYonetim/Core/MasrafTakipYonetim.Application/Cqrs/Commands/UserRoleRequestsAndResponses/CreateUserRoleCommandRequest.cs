using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.RoleUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleUserRequestsAndResponses
{
    public class CreateUserRoleCommandRequest:IRequest<Results>
    {
        public Guid RoleId { get; set; }

        public Guid AppUserId { get; set; }
       
    }
}

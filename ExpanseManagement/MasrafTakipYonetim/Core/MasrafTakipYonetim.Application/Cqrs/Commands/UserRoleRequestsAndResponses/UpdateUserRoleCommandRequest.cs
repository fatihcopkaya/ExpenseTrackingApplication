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
    public class UpdateUserRoleCommandRequest : IRequest<Results>
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }

        public Guid AppUserId { get; set; }
       
    }
}

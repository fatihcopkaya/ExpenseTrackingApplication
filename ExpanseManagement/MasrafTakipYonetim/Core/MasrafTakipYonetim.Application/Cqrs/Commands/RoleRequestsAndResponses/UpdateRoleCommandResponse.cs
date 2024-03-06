using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses
{
    public class UpdateRoleCommandResponse : IRequest<UpdateRoleCommandResponse>
    {
        public Roles? roles { get; set; }
        public string Messages { get; set; }

    }
}

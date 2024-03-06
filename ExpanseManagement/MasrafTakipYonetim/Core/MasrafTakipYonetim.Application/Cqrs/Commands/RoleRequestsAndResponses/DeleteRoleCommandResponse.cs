using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses
{
    public class DeleteRoleCommandResponse : IRequest<DeleteRoleCommandRequest>
    {

        public string Messages { get; set; }
    }
}

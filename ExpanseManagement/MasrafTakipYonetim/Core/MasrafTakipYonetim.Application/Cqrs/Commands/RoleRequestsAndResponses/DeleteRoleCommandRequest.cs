using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses
{
    public class DeleteRoleCommandRequest : IRequest<Results>
    {
        //public DeleteRoleDto DeleteRoleDto { get; set; }
        public Guid Id { get; set; }
    }
}

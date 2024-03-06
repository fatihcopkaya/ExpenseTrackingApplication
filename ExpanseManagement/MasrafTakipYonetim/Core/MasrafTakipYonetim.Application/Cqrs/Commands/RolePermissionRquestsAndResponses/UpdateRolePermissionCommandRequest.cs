using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses
{
    public class UpdateRolePermissionCommandRequest:IRequest<Results>
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }
        public Guid PermissionIds { get; set; }

         

    }
}

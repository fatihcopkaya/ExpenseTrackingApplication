using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses
{
    public class DeleteRolePermissionCommandRequest:IRequest<Results>
    {
        public Guid RoleId { get; set; }
        public string PermissionIds { get; set; }

    }
}

using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses
{
    public class CreateRolePermissionCommandRequest : IRequest<Results>
    {
        public Guid RoleId { get; set; }
        //public List<MasrafTakipYonetim.Domain.Entities.Permission> Permission { get; set; }
        public List<Guid> PermissionIds { get; set; }


    }
       
}



using MasrafTakipYonetim.Domain.Entities;

namespace MasrafTakipYonetim.Application.Dtos.RolePermission
{
    public class CreateRolePermissionRequesDto
    {
        public Guid RoleId { get; set; }

        public Guid PermissionIds { get; set; }
         
       // public ICollection<MasrafTakipYonetim.Domain.Entities.Permission> Permission { get; set; }
    }
}
 
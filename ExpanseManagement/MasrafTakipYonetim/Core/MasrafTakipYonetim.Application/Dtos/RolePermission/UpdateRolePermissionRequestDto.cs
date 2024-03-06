

namespace MasrafTakipYonetim.Application.Dtos.RolePermission
{
    public class UpdateRolePermissionRequestDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}

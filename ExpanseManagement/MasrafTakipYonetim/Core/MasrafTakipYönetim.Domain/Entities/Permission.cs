using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipYonetim.Domain.Entities
{
    public class Permission : BaseEntity
    {
        [Required]
        [StringLength(25)]
        public string PermissionName { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }


}

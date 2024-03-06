using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipYonetim.Domain.Entities
{
    public class Roles : BaseEntity
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

    }


}

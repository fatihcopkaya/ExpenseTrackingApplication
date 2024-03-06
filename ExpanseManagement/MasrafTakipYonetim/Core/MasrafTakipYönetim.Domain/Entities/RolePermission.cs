using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasrafTakipYonetim.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        
        [ForeignKey("Roles")]
        public Guid RoleId { get; set; }
        public Roles Role { get; set; }
        [ForeignKey("Permissions")]
        public Guid PermissionId{ get; set; }
        public Permission Permission { get; set; }
        //public ICollection<Permission> Permission { get; set; }
    }


}

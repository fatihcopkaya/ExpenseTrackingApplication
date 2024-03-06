using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasrafTakipYonetim.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        [ForeignKey("Roles")]
        public Guid RoleId { get; set; }
        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        public AppUser User { get; set; }
        public Roles Role { get; set; }
    }


}

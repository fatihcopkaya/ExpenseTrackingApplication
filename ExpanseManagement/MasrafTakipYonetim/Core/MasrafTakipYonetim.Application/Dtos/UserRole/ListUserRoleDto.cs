using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.UserRole
{
    public class ListUserRoleDto
    {
        public Guid Id {  get; set; }
        public Guid AppUserId { get; set; }
        public string? AppUserFirstName { get; set; }
        public string? AppUserLastName { get; set;}
        public Guid RoleId { get; set; }
        public string? RoleName { get; set;}
    }
}

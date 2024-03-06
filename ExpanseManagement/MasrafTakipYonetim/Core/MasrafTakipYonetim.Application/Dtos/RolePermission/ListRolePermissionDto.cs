using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.RolePermission
{
    public class ListRolePermissionDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string ?RoleName { get; set; }
        public Guid PermissionId { get; set; }
        public string ?PermissionName { get; set; }
    }
}

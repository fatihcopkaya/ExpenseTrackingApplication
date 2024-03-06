using MasrafTakipYonetim.Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.RoleQuery
{
    public class GetRoleListForPaginationQueryRequest:BasePagination<Results>
    {

        public string? Name { get; set; }
        public string? PermissionName { get; set; }
    }
}

using MasrafTakipYonetim.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PermissionQuery
{
    public class ListPermissionByRoleIdRequest:IRequest<Results>
    {
        public Guid RoleId { get; set; }
        public string? PermissionName { get; set; }
    }
}

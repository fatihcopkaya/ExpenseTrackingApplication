using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PermissionQuery
{
    public class ListPermissionByRoleIdResponse : IRequest<ListPermissionByRoleIdRequest>
    {
        public List<Permission>? permissions { get; set; }
        public bool Success { get; set; }
    }
}

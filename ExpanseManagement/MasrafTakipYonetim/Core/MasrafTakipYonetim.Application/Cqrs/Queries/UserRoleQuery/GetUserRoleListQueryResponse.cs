using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery
{
    public class GetUserRoleListQueryResponse:IRequest<GetUserRoleListQueryRequest>
    {
        //public List<UserRole> UserRoles { get; set; }
        public string Messages { get; set; }
    }
}

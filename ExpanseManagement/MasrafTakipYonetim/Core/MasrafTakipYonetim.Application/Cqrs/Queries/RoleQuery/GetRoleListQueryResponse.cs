using MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.RoleQuery
{
    public class GetRoleListQueryResponse:IRequest<GetUserRoleListQueryRequest>
    {
        public List<Roles>? Roles {  get; set; }
        public string ?Messages {  get; set; }
    }
}

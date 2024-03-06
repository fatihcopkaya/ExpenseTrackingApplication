using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery
{
    public class GetUserRoleByIdQueryResponse:IRequest<GetUserRoleByIdQueryRequest>
    {
        public UserRole? UserRole { get; set; }
        public string Message { get; set; }
    }
}

using MasrafTakipYonetim.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery
{
    public class GetUserRoleListQueryRequest:BasePagination<Results>
    {
        public string? AppUserFirstName {  get; set; }
        public string? AppUserLastName { get; set; }
        public string? RoleName { get; set; }
    }
}

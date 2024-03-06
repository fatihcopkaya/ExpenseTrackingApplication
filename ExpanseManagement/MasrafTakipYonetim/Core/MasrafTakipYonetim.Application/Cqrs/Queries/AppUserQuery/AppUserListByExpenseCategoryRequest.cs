using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class AppUserListByExpenseCategoryRequest: BasePagination<Results>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleName { get; set; }
        public ExpenseCategoryType ExpenseCategoryType { get; set; }
    }
}

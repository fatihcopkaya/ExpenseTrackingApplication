using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery
{
    public class AppUserListByExpenseCategoryResponse:IRequest<AppUserListByExpenseCategoryRequest>
    {
        public string Messsages { get; set; }
    }
}

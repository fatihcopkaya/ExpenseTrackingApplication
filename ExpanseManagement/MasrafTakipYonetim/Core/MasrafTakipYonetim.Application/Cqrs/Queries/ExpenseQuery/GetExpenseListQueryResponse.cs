using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery
{
    public class GetExpenseListQueryResponse : IRequest<GetExpenseListQueryRequest>
    {
        public List<Expense> Expenses { get; set; }
    }
}

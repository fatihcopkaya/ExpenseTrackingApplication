using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery
{
    public class GetExpenseByIdQueryResponse : IRequest<GetExpenseByIdQueryRequest>
    {
        public Expense Expense { get; set; }
        public string Message { get; set; }
    }
}

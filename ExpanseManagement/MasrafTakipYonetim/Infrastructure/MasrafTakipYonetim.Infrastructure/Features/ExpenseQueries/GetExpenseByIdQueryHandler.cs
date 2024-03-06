using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseQueries
{
    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQueryRequest, GetExpenseByIdQueryResponse>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetExpenseByIdQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<GetExpenseByIdQueryResponse> Handle(GetExpenseByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var row = await _expenseRepository.GetExpenseByIdAsync(request.ExpenseId);
            if (row.Data == null) 
            {
                throw new MasrafTakipCustomException($"{request.ExpenseId} Has ID Number {nameof(Expense)} Could Not Be Found", 404);
            }
            return new GetExpenseByIdQueryResponse { Expense = row.Data };
        }
    }
}

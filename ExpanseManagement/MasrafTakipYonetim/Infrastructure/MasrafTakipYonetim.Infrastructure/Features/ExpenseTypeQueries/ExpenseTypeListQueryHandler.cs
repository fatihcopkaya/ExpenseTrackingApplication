
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeQueries
{
    public class ExpenseTypeListQueryHandler : IRequestHandler<ExpenseTypeListQueryRequest, ExpenseTypeListQueryResponse>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public ExpenseTypeListQueryHandler(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
        }

        public async Task<ExpenseTypeListQueryResponse> Handle(ExpenseTypeListQueryRequest request, CancellationToken cancellationToken)
        {
            var expensetypelist = await _expenseTypeRepository.GetListAsync();
            if (expensetypelist.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(ExpenseType)} List Not Found!", 404);
            };
            return new ExpenseTypeListQueryResponse { ExpenseTypes = expensetypelist.ToList() };
        }
    }
}

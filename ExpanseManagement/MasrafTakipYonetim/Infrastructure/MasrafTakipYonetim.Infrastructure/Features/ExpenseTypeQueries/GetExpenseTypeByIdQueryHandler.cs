
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;

using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeQueries
{
    public class GetExpenseTypeByIdQueryHandler : IRequestHandler<GetExpenseTypeByIdQueryRequest, GetExpenseTypeByIdQueryResponse>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public GetExpenseTypeByIdQueryHandler(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;

        }

        public async Task<GetExpenseTypeByIdQueryResponse> Handle(GetExpenseTypeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var expenseTypeById = await _expenseTypeRepository.GetExpenseTypeByIdAsync(request.Id);
            if (expenseTypeById.Data == null)
            {
                throw new MasrafTakipCustomException($"{request.Id} Has ID Number{nameof(ExpenseType)} Could Not Be Found!", 404);
            }
            return new GetExpenseTypeByIdQueryResponse { ExpenseType = expenseTypeById.Data };

        }
    }
}

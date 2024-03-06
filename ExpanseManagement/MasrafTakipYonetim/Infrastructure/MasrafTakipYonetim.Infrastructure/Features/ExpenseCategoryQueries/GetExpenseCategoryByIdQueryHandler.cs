using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;

using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryQueries
{
    public class GetExpenseCategoryByIdQueryHandler : IRequestHandler<GetExpenseCategoryByIdQueryRequest, GetExpenseCategoryByIdQueryResponse>
    {

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
public GetExpenseCategoryByIdQueryHandler(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task<GetExpenseCategoryByIdQueryResponse> Handle(GetExpenseCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var expenseCategoryById = await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(request.Id);
            if (expenseCategoryById == null) {

                throw new MasrafTakipCustomException($"{request.Id} Has ID Number {nameof(ExpenseCategory)} Could Not be Found", 404);
            }
            return new GetExpenseCategoryByIdQueryResponse { ExpenseCategory = expenseCategoryById.Data };

        }
    }
}

using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.WalletByCategoryQueries
{
    public class GetWalletByCategoryByExpenseCategoryIdHandler : IRequestHandler<GetWalletByCategoryByExpenseCategoryIdRequest, GetWalletByCategoryByExpenseCategoryIdResponse>
    {
        private readonly IWalletByExpenseCategoryRepository _walletByExpenserepository;

        public GetWalletByCategoryByExpenseCategoryIdHandler(IWalletByExpenseCategoryRepository repository)
        {
            _walletByExpenserepository = repository;
        }

        public async Task<GetWalletByCategoryByExpenseCategoryIdResponse> Handle(GetWalletByCategoryByExpenseCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var _walletByExpenseCategory = await _walletByExpenserepository.GetFirstOrDefaultAsync(x => x.ExpenseCategoryId == request.ExpenseCategoryId);
            return _walletByExpenseCategory == null ? throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Found", 404)
           : new GetWalletByCategoryByExpenseCategoryIdResponse { WalletByExpenseCategory = _walletByExpenseCategory };


        }
    }
}

using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.WalletByCategoryQueries
{
    public class GetAllWalletByCategoryByExpenseCategoryIdHandler : IRequestHandler<GetAllWalletByCategoryByExpenseCategoryIdRequest, GetAllWalletByCategoryByExpenseCategoryIdResponse>
    {
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;

        public GetAllWalletByCategoryByExpenseCategoryIdHandler(IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository)
        {
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
        }

        public async Task<GetAllWalletByCategoryByExpenseCategoryIdResponse> Handle(GetAllWalletByCategoryByExpenseCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var allwalletbyexpensecategory = await _walletByExpenseCategoryRepository.GetListAsync(x=>x.IsDeleted==false, includes:x=>x.ExpenseCategory);

            var getwallets = new GetAllWalletByCategoryByExpenseCategoryIdResponse()
            {
                ExpenseCategoryName = allwalletbyexpensecategory.Select(p => p.ExpenseCategory.Name),
                TotalExpense = allwalletbyexpensecategory.Select(p => p.TotalExpenseByExpenseCategory),
                TotalPaments = allwalletbyexpensecategory.Select(p => p.TotalPaymentByExpenseCategory),
            };

            return getwallets;

        }
    }
}

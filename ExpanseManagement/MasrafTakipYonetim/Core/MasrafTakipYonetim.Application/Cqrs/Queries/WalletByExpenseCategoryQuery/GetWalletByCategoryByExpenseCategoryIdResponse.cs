using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery
{
    public class GetWalletByCategoryByExpenseCategoryIdResponse : IRequest<GetWalletByCategoryByExpenseCategoryIdRequest>
    {
        public WalletByExpenseCategory WalletByExpenseCategory { get; set; }
    }
}

using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery
{
    public class GetWalletByCategoryByExpenseCategoryIdRequest : IRequest<GetWalletByCategoryByExpenseCategoryIdResponse>
    {
        public Guid ExpenseCategoryId { get; set; }
    }
}

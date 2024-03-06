using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery
{
    public class GetExpenseCategoryListQueryResponse : IRequest<GetExpenseCategoryListQueryRequest>
    {
        public List<ExpenseCategory>? ExpenseCategories { get; set; }
        public string Messages { get; set; }
    }
}

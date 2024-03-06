using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery
{
    public class GetExpenseCategoryByIdQueryResponse : IRequest<GetExpenseCategoryByIdQueryRequest>
    {
        public ExpenseCategory ExpenseCategory { get; set; }
        public string Messages { get; set; }
    }
}

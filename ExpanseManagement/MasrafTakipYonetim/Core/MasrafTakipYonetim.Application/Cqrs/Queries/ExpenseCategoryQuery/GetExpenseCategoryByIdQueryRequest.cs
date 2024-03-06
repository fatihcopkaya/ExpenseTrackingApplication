using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery
{
    public class GetExpenseCategoryByIdQueryRequest : IRequest<GetExpenseCategoryByIdQueryResponse>
    {

        public Guid Id { get; set; }
    }
}

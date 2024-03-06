using MediatR;

namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery
{
    public class GetExpenseTypeByIdQueryRequest:IRequest<GetExpenseTypeByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

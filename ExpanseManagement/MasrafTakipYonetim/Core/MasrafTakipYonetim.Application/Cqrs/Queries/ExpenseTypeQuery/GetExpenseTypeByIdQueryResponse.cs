using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery
{
    public class GetExpenseTypeByIdQueryResponse:IRequest<GetExpenseTypeByIdQueryRequest>
    {
        public ExpenseType ExpenseType { get; set; }
        public string Message { get; set; }
    }
}

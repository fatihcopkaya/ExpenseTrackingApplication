using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery
{
    public class ExpenseTypeListQueryResponse:IRequest<ExpenseTypeListQueryRequest>
    {
        public List<ExpenseType> ExpenseTypes { get; set; }
        public string Messages { get; set; }
    }
}

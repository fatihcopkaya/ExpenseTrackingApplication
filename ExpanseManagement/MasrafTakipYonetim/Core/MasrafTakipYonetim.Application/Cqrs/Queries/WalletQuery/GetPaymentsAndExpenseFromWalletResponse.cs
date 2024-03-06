using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery
{
    public class GetPaymentsAndExpenseFromWalletResponse:IRequest<GetPaymentsAndExpenseFromWalletRequest>
    {
        public IEnumerable<float> ?TotalPayments { get; set; }
        public IEnumerable<float> ?TotalExpenses { get; set; }
    }
}

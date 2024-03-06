using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery
{
    public class GetPaymentListForReportResponse:IRequest<GetPaymentAmountForHomeQueryRequest>
    {

        public string Messages { get; set; }
    }
}

using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery
{
    public class GetPaymentListsIdQueryResponse:IRequest<GetPaymentListsIdQueryRequest>
    {
        public List<Payment>? payments { get; set; }

        public string Messages { get; set; }
    }
}

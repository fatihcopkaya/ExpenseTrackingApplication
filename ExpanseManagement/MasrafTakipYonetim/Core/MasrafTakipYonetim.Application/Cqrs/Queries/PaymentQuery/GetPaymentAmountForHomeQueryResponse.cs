using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery
{
    public class GetPaymentAmountForHomeQueryResponse
    {
        public double? TotalAmount { get; set; }
    }
}

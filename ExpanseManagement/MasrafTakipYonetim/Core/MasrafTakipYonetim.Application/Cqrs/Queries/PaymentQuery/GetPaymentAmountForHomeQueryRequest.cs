using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery
{
    public class GetPaymentAmountForHomeQueryRequest:IRequest<Results>
    {
        public ExpenseCategoryType ExpenseCategoryType { get; set; }
    }
}

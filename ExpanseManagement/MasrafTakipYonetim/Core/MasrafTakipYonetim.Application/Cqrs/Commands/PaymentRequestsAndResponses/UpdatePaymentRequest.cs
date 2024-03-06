using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Utilities.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses
{
    public class UpdatePaymentRequest : IRequest<Results>
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PeriodDate { get; set; }
        public float PaymentAmount { get; set; }
        public bool IsPaid { get; set; }
   
    }
}

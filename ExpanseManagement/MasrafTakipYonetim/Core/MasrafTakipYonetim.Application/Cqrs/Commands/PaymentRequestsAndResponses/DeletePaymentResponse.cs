using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses
{
    public class DeletePaymentResponse:IRequest<DeletePaymentRequest>
    {
        public string Message { get; set; }
        public Guid Id { get; set; }
    }
}

using MasrafTakipYonetim.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses
{
    public class DeletePaymentRequest: IRequest<DeletePaymentResponse>
    {
        public Guid Id { get; set; }
    }
}

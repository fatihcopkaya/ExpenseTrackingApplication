using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.PaymentCommands
{
    public class DeletePaymentHandler : IRequestHandler<DeletePaymentRequest, DeletePaymentResponse>

    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeletePaymentHandler(IPaymentRepository paymentRepository, IMapper mapper, IMediator mediator)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


          async Task <DeletePaymentResponse>  IRequestHandler<DeletePaymentRequest, DeletePaymentResponse>.Handle(DeletePaymentRequest request, CancellationToken cancellationToken)
        {
            var paymentId = request.Id;


            // İlgili Payment'ı  buluyorum
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);

            if (payment == null)
            {
                // Ödeme bulunamadıysa başarısız bir sonuç döndürüyor
                throw new MasrafTakipCustomException($"{request.Id} numaralı Id'ye sahip{nameof(Payment)} bulunamadı", 404);
            }

            payment.IsDeleted = true;
            // Payment'i sil
            await _paymentRepository.DeletePaymentAsync(payment);

            // İşlem başarılı olduysaa  başarılı bir sonuç döndürüyor
            return new DeletePaymentResponse() { Id = payment.Id, Message = "" };
        }
    }
}

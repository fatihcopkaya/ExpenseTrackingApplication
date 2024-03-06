using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
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
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommandRequest, Results>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;
        private readonly IAppUserRepository _appUserRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IMediator mediator, IWalletRepository walletRepository, IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository, IAppUserRepository appUserRepository)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _mediator = mediator;
            _walletRepository = walletRepository;
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task<Results> Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken)
        {

            var newPayment = new Payment
            {
                AppUserId = request.AppUserId,
                ExpenseCategoryId = request.ExpenseCategoryId,
                PaymentAmount = request.PaymentAmount,
                PaymentDate = request.PaymentDate,
                IsPaid = request.IsPaid,
            };

            var createdPayment = await _paymentRepository.CreatePaymentAsync(newPayment);

            var walletByExpenseByExpenseCategoryId = new GetWalletByCategoryByExpenseCategoryIdRequest()
            { ExpenseCategoryId = request.ExpenseCategoryId };
            var walletByExpenseCategory = await _mediator.Send(walletByExpenseByExpenseCategoryId);
            walletByExpenseCategory.WalletByExpenseCategory.TotalPaymentByExpenseCategory += request.PaymentAmount;
            var updatedWalletByCategory = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(walletByExpenseCategory.WalletByExpenseCategory);

            if (!updatedWalletByCategory.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Updated", 500);
            }

            var walletRequest = new GetWalletRequest() { };
            var wallet = await _mediator.Send(walletRequest);
            wallet.Wallet.TotalPayments += request.PaymentAmount;
            var updatedWallet = await _walletRepository.UpdateWalletAsync(wallet.Wallet);

            return Results.Success();


            
        }
    }
}

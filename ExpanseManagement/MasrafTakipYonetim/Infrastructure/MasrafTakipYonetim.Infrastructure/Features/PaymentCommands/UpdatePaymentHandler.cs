using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses;
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
    public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentRequest, Results>
    {    

        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;

        public UpdatePaymentHandler(IPaymentRepository paymentRepository, IMapper mapper, IMediator mediator, IWalletRepository walletRepository, IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _mediator = mediator;
            _walletRepository = walletRepository;
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
        }

        public async Task<Results> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            var paymentuserId = request.Id; // Güncellenecek kullanıcının ID'si
            var updatepaymentuserId = await _paymentRepository.GetPaymentByIdAsync(paymentuserId);
            if (updatepaymentuserId == null)
            {
                throw new MasrafTakipCustomException($"{paymentuserId} 'ye sahip{nameof(Payment)} bulunamadı", 404);
            }

            updatepaymentuserId.Id = request.Id; 
            updatepaymentuserId.AppUserId = request.AppUserId;
            updatepaymentuserId.ExpenseCategoryId = request.ExpenseCategoryId;
           // updatepaymentuserId.AppUser = request.AppUser;
            updatepaymentuserId.PaymentAmount = request.PaymentAmount;
            updatepaymentuserId.PeriodDate = request.PeriodDate;
            updatepaymentuserId.IsPaid = request.IsPaid;
            // updatepaymentuserId.ExpenseCategory.Name = request.ExpenseCategoryName;

            var result = await _paymentRepository.UpdatePaymentAsync(updatepaymentuserId);


            var walletByExpenseByExpenseCategoryId = new GetWalletByCategoryByExpenseCategoryIdRequest()
            { ExpenseCategoryId = updatepaymentuserId.ExpenseCategoryId };
            var walletByExpenseCategory = await _mediator.Send(walletByExpenseByExpenseCategoryId);
            walletByExpenseCategory.WalletByExpenseCategory.TotalPaymentByExpenseCategory += updatepaymentuserId.PaymentAmount;
            var updatedWalletByCategory = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(walletByExpenseCategory.WalletByExpenseCategory);

            if (!updatedWalletByCategory.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Updated", 500);
            }

            //categorye göre olan wallet değeri eklendikten sonra genel wallete de bu değer eklenir
            var walletRequest = new GetWalletRequest() { };
            var wallet = await _mediator.Send(walletRequest);
            wallet.Wallet.TotalPayments += updatepaymentuserId.PaymentAmount;
            var updatedWallet = await _walletRepository.UpdateWalletAsync(wallet.Wallet);

          

            return Results.Success();


        }
    }
}

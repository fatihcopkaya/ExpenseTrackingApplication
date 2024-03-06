using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.PaymentQueries
{
    public class GetPaymentAmountForHomeQueryHandler : IRequestHandler<GetPaymentAmountForHomeQueryRequest, Results>
    {
        private readonly IPaymentRepository _paymentRepository;

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        private readonly IAppUserRepository _appuserRepository;
        public GetPaymentAmountForHomeQueryHandler(IPaymentRepository paymentRepository, IAppUserRepository appUserRepository, IExpenseCategoryRepository expenseCategoryRepository)
        {
            _paymentRepository = paymentRepository;
            _appuserRepository = appUserRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
        }
        public async Task<Results> Handle(GetPaymentAmountForHomeQueryRequest request, CancellationToken cancellationToken)
        {
            double? totalAmount = await _paymentRepository.GetTotalAmountByExpenseCategoryType(request.ExpenseCategoryType);
            var response = new GetPaymentAmountForHomeQueryResponse
            {
                TotalAmount = totalAmount
            };

            return Results<GetPaymentAmountForHomeQueryResponse>.Success(response);
        }
    }
}

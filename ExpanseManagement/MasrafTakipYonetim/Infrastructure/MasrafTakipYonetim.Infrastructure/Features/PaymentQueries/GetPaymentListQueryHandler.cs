 using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.Payment;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.PaymentQueries
{
    public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQueryRequest, Results>
    {

        private readonly IPaymentRepository _paymentRepository;

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        private readonly IAppUserRepository _appuserRepository;

        public GetPaymentListQueryHandler(IPaymentRepository paymentRepository, IAppUserRepository appUserRepository, IExpenseCategoryRepository expenseCategoryRepository)
        {
            _paymentRepository = paymentRepository;
            _appuserRepository = appUserRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
        }
        public async Task<Results> Handle(GetPaymentListQueryRequest request, CancellationToken cancellationToken)
        {


            // Veritabanından ödeme listesini almak için _paymentRepository'den sorguyu çağırıyoruz.
            var paymentlist = await _paymentRepository.GetListAsync(x => x.IsDeleted == false, includes: new Expression<Func<Payment, object>>[] { x => x.AppUser, x => x.ExpenseCategory });

            // Eğer ödeme listesi sorgusu başarılı olduysa ve veri varsa devam ediyor.


            // Ödeme listesini, kullanıcı dostu bir 'ListPaymentDto' türüne dönüştürdüm.
            var paymentDto = paymentlist.Select(P => new ListPaymentDto()
            {
                Id = P.Id,
                AppUserId = P.AppUserId,
                PaymentDate = P.PaymentDate,
                PaymentAmount = P.PaymentAmount,
                IsPaid = P.IsPaid,
                AppUserName = P.AppUser?.FirstName,
                ExpenseCategoryId = P.ExpenseCategoryId,
                ExpenseCategoryName=P.ExpenseCategory?.Name
            }).AsQueryable();


            if (paymentlist.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(Payment)} Listesi bulunamadı", 404);
            };

            Results result;

            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                paymentDto = paymentDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(paymentDto.OrderBy(x => x.AppUserId)
                    .QueryResourceNotMapped<ListPaymentDto>(request.PageNumber, request.PageSize));
            }



            result = await Task.FromResult(paymentDto.QueryResourceNotMapped<ListPaymentDto>(request.PageNumber, request.PageSize));
            return result;




        }
    }
}

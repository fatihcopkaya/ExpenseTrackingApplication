using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Payment;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.PaymentQueries
{
    public class GetPaymentListIdQueryHandler : IRequestHandler<GetPaymentListsIdQueryRequest, Results>
    {
        private readonly IPaymentRepository _paymentRepository;

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        private readonly IAppUserRepository _appuserRepository;

        public GetPaymentListIdQueryHandler(IPaymentRepository paymentRepository, IAppUserRepository appUserRepository, IExpenseCategoryRepository expenseCategoryRepository)
        {
            _paymentRepository = paymentRepository;
            _appuserRepository = appUserRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
        }
        public async Task<Results> Handle(GetPaymentListsIdQueryRequest request, CancellationToken cancellationToken)
        {
            var paymentlist = await _paymentRepository.GetPaymentsByExpenseCategoryTypeAsync(request.ExpenseCategoryType);


            var paymentDto = paymentlist.Data.Select(P => new ListPaymentDto()
            {
                Id = P.Id,
                AppUserId = P.AppUserId,
                PeriodDate= P.PeriodDate,
                //PaymentDate = P.PaymentDate,
                PaymentAmount = P.PaymentAmount,
                IsPaid = P.IsPaid,
                AppUserName = P.AppUser?.FirstName,
                ExpenseCategoryId = P.ExpenseCategoryId,
                ExpenseCategoryName = P.ExpenseCategory?.Name,
                ExpenseCategoryType=P.ExpenseCategory.ExpenseCategoryType
            }).AsQueryable();

            if (paymentlist.Data.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(Payment)} Listesi bulunamadı", 404);
            };

            Results result;

            if (!string.IsNullOrEmpty(request.AppUserName) || !String.IsNullOrWhiteSpace(request.PeriodDate) || request.IsPaid.HasValue)
            {
                paymentDto = paymentDto.Where(x =>
                    (string.IsNullOrEmpty(request.AppUserName) || x.AppUserName.ToLower().Contains(request.AppUserName.Trim().ToLower()) || x.AppUserName.ToUpper().Contains(request.AppUserName.Trim().ToUpper())) &&
                    (String.IsNullOrWhiteSpace(request.PeriodDate) || (x.PeriodDate.Year == DateTime.Parse(request.PeriodDate).Year &&
                                           x.PeriodDate.Month == DateTime.Parse(request.PeriodDate).Month )) &&
                                           (!request.IsPaid.HasValue || x.IsPaid == request.IsPaid)
                );
            }



            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                paymentDto = paymentDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(paymentDto.OrderBy(x => x.AppUserName)
                    .QueryResourceNotMapped<ListPaymentDto>(request.PageNumber, request.PageSize));
            }




            result = await Task.FromResult(paymentDto.QueryResourceNotMapped<ListPaymentDto>(request.PageNumber, request.PageSize));
            return result;


        }
    }
}

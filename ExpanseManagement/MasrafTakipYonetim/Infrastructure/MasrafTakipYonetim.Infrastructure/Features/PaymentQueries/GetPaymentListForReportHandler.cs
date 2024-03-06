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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasrafTakipYonetim.Infrastructure.Features.PaymentQueries
{
    public class GetPaymentListForReportHandler : IRequestHandler<GetPaymentListForReportRequest, Results>
    {

        private readonly IPaymentRepository _paymentRepository;

        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        private readonly IAppUserRepository _appuserRepository;

        public GetPaymentListForReportHandler(IPaymentRepository paymentRepository, IAppUserRepository appUserRepository, IExpenseCategoryRepository expenseCategoryRepository)
        {
            _paymentRepository = paymentRepository;
            _appuserRepository = appUserRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
        }
        public async Task<Results> Handle(GetPaymentListForReportRequest request, CancellationToken cancellationToken)
        {
            var paymentlist = await _paymentRepository.GetPaymentListAsync();
            DateTime? startDate = null;
            DateTime? endDate = null;



            if (!string.IsNullOrWhiteSpace(request.StartDate))
            {
                if (DateTime.TryParse(request.StartDate, out DateTime parsedStartDate))
                {
                    startDate = parsedStartDate;
                }
                else
                {
                    // Hatalı tarih formatı ile ilgili bir işlem yapabilirsiniz.
                    // Örneğin:
                     throw new ArgumentException("Geçersiz tarih formatı. YYYY-MM-DD şeklinde olmalıdır.");
                }
            }

            if (!string.IsNullOrWhiteSpace(request.EndDate))
            {
                if (DateTime.TryParse(request.EndDate, out DateTime parsedEndDate))
                {
                    endDate = parsedEndDate;
                }
                else
                {
                    // Hatalı tarih formatı ile ilgili bir işlem yapabilirsiniz.
                    // Örneğin:
                     throw new ArgumentException("Geçersiz tarih formatı. YYYY-MM-DD şeklinde olmalıdır.");
                }
            }





            var paymentforReportDto = paymentlist.Data
            .Where(P => (!startDate.HasValue || (P.PaymentDate.Year >= startDate.Value.Year && P.PaymentDate.Month >= startDate.Value.Month)) &&
                (!endDate.HasValue || (P.PaymentDate.Year <= endDate.Value.Year && P.PaymentDate.Month <= endDate.Value.Month)))
                .Select(P => new ListPaymentForReportDto()

            {
                Id =P.Id,
                AppUserId = P.AppUserId,
                AppUserName = P.AppUser?.FirstName,
                PaymentDate = P.PaymentDate,
                IsPaid = P.IsPaid,
                ExpenseCategoryName = P.ExpenseCategory?.Name,

            }).AsQueryable();

            if (paymentlist.Data.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(Payment)} Listesi bulunamadı", 404);
            };

            Results result;

   

            if (!string.IsNullOrEmpty(request.AppUserName) || request.IsPaid.HasValue)
            {
                paymentforReportDto = paymentforReportDto.Where(x =>
                    (string.IsNullOrEmpty(request.AppUserName) || x.AppUserName.ToLower().Contains(request.AppUserName.Trim().ToLower()) ||
                    x.AppUserName.ToUpper().Contains(request.AppUserName.Trim().ToUpper())) &&
                    (!request.IsPaid.HasValue || x.IsPaid == request.IsPaid)
                );
            }




            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                paymentforReportDto = paymentforReportDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(paymentforReportDto.OrderBy(x => x.AppUserName)
                    .QueryResourceNotMapped<ListPaymentForReportDto>(request.PageNumber, request.PageSize));
            }




            result = await Task.FromResult(paymentforReportDto.QueryResourceNotMapped<ListPaymentForReportDto>(request.PageNumber, request.PageSize));
            return result;


        }
    }
}

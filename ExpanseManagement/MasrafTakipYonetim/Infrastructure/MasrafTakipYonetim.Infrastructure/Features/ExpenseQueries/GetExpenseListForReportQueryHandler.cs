using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Expense;
using MasrafTakipYonetim.Application.Dtos.Payment;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseQueries
{
    public class GetExpenseListForReportQueryHandler : IRequestHandler<GetExpenseListForReportRequest, Results>
    {

        private readonly IExpenseRepository _expenseRespository;
        private readonly IExpenseCategoryRepository _expenseCategoryRespository;
        private readonly IExpenseTypeRepository _expenseTypeRespository;

        public GetExpenseListForReportQueryHandler(IExpenseRepository expenseRespository, IExpenseCategoryRepository expenseCategoryRespository, IExpenseTypeRepository expenseTypeRespository)
        {
            _expenseRespository = expenseRespository;
            _expenseCategoryRespository = expenseCategoryRespository;
            _expenseTypeRespository = expenseTypeRespository;
        }
        public async Task<Results> Handle(GetExpenseListForReportRequest request, CancellationToken cancellationToken)
        {
            var expenseForReportList = await _expenseRespository.GetListAsync(x => x.IsDeleted == false, includes: new Expression<Func<Expense, object>>[] { x => x.ExpenseType, x => x.ExpenseCategory });

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





            var expenseforReportDto = expenseForReportList
           .Where(P => (!startDate.HasValue || (P.PaymentDate.Year >= startDate.Value.Year && P.PaymentDate.Month >= startDate.Value.Month)) &&
               (!endDate.HasValue || (P.PaymentDate.Year <= endDate.Value.Year && P.PaymentDate.Month <= endDate.Value.Month)))
               .Select(P => new ListExpenseForReportDto()
               {
                Id=P.Id,
                Description=P.Description,
                ExpenseCategoryId=P.ExpenseCategoryId,
                ExpenseTypeId=P.ExpenseTypeId,
                ExpenseTypeName=P.ExpenseType.Name,
                Amount=P.Amount,
                ExpenseCategoryName=P.ExpenseCategory.Name,
                PaymentDate=P.PaymentDate,
                

            }).AsQueryable();


            if (expenseForReportList.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(Expense)} Listesi bulunamadı", 404);
            };

            Results result;
            expenseforReportDto = expenseforReportDto.Where(x =>
             (string.IsNullOrEmpty(request.ExpenseTypeName) ||
              x.ExpenseTypeName.ToLower().Contains(request.ExpenseTypeName.Trim().ToLower()) ||
              x.ExpenseTypeName.ToUpper().Contains(request.ExpenseTypeName.Trim().ToUpper())) &&
             (!request.PaymentDate.HasValue ||
              (x.PaymentDate.Year == request.PaymentDate.Value.Year &&
               x.PaymentDate.Month == request.PaymentDate.Value.Month + 1))
            );






            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                expenseforReportDto = expenseforReportDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(expenseforReportDto.OrderBy(x => x.ExpenseTypeId)
                    .QueryResourceNotMapped<ListExpenseForReportDto>(request.PageNumber, request.PageSize));
            }




            result = await Task.FromResult(expenseforReportDto.QueryResourceNotMapped<ListExpenseForReportDto>(request.PageNumber, request.PageSize));
            return result;

        }
    }
}

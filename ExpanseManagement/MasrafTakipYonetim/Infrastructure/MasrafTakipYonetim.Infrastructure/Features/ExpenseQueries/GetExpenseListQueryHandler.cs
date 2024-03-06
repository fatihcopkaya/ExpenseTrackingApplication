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
    public class GetExpenseListQueryHandler : IRequestHandler<GetExpenseListQueryRequest, Results>
    {
        private readonly IExpenseRepository _expenseRespository;
        private readonly IExpenseCategoryRepository _expenseCategoryRespository;
        private readonly IExpenseTypeRepository _expenseTypeRespository;

        public GetExpenseListQueryHandler(IExpenseRepository expenseRespository, IExpenseCategoryRepository expenseCategoryRespository, IExpenseTypeRepository expenseTypeRespository)
        {
            _expenseRespository = expenseRespository;
            _expenseCategoryRespository = expenseCategoryRespository;
            _expenseTypeRespository = expenseTypeRespository;
        }

        public async Task<Results> Handle(GetExpenseListQueryRequest request, CancellationToken cancellationToken)
        {
            var expenseList = await _expenseRespository.GetListAsync(x => x.IsDeleted == false, includes: new Expression<Func<Expense, object>>[] { x => x.ExpenseType, x => x.ExpenseCategory });

            var expenseDto = expenseList.Select(P => new ListExpenseDto()
            {
                Id = P.Id,
                Description = P.Description,
                PaymentDate = P.PaymentDate,
                Amount = P.Amount,
                ExpenseCategoryId = P.ExpenseCategoryId,
                ExpenseTypeId= P.ExpenseTypeId,
                ExpenseCategoryName = P.ExpenseCategory?.Name,
                ExpenseCategoryType = P.ExpenseCategory.ExpenseCategoryType,
                ExpenseTypeName = P.ExpenseType?.Name,
            }).AsQueryable();

            if (expenseList.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(Expense)} Listesi bulunamadı", 404);
            };

            Results result;
            expenseDto = expenseDto.Where(x =>
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
                expenseDto = expenseDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(expenseDto.OrderBy(x => x.ExpenseCategoryType)
                    .QueryResourceNotMapped<ListExpenseDto>(request.PageNumber, request.PageSize));
            }




            result = await Task.FromResult(expenseDto.QueryResourceNotMapped<ListExpenseDto>(request.PageNumber, request.PageSize));
            return result;
           
        }
    }
}

using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery
{
    public class GetExpenseListQueryRequest : BasePagination<Results>
    {
        //public string ?ExpenseCategoryName { get; set; }

        public string? ExpenseTypeName { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}

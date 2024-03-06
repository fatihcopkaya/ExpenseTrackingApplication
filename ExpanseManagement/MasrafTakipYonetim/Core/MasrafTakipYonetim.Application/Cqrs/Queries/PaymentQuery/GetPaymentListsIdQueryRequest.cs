using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasrafTakipYonetim.Domain.Enums;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery
{
    public class GetPaymentListsIdQueryRequest : BasePagination<Results>
    {
        public ExpenseCategoryType ExpenseCategoryType {get;set;}
        public string? AppUserName { get; set; }
        //public string? PaymentDate { get; set; }
        public string? PeriodDate { get; set; }
        public bool? IsPaid { get; set; }
        //public Guid ExpenseCategoryId { get; set; }

    }
}

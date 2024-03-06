using MasrafTakipYonetim.Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.PaymentQuery
{
    public class GetPaymentListForReportRequest: BasePagination<Results>
    {
        public string? AppUserName { get; set; }
        public string? PaymentDate { get; set; }
        public string? StartDate { get; set; }  // Eklenen satır
        public string? EndDate { get; set; }    // Eklenen satır
        public bool? IsPaid { get; set; }
    }
}

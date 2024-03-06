using MasrafTakipYonetim.Application.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery
{
    public class GetExpenseListForReportRequest:BasePagination<Results>
    { 
        public string? ExpenseTypeName { get; set; }
        public string? StartDate { get; set; }  // Eklenen satır
        public string? EndDate { get; set; }    // Eklenen satır
        public DateTime? PaymentDate { get; set; }
    }
}

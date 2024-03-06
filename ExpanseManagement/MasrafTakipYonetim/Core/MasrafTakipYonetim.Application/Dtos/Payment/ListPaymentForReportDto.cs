using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Payment
{
    public class ListPaymentForReportDto
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public string AppUserName { get; set; } // AppUser ile ilişkilendirilmiş kullanıcı adı
        public string ExpenseCategoryName { get; set; }

        public string StartDate { get; set; }  // Eklenen satır
        public string EndDate { get; set; }    // Eklenen satır
    }
}

using MasrafTakipYonetim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Payment
{
    public class ListPaymentDto
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PeriodDate { get; set; }
        public float PaymentAmount { get; set; }
        public bool IsPaid { get; set; }
        public string AppUserName { get; set; } // AppUser ile ilişkilendirilmiş kullanıcı adı
        public string ExpenseCategoryName { get; set; }
        public ExpenseCategoryType ExpenseCategoryType { get; set; }


        // Diğer gerekli özellikler
    }
}

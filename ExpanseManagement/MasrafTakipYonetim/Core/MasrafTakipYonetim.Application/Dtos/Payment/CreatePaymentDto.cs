using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Payment
{
    public class CreatePaymentDto
    {
        public Guid AppUserId { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public DateTime PaymentDate { get; set; }
        public float PaymentAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}

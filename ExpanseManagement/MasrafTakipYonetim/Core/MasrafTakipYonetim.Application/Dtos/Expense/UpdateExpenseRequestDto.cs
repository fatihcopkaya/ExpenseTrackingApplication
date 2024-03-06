using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Expense
{
    public class UpdateExpenseRequestDto
    {
        public Guid Id { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

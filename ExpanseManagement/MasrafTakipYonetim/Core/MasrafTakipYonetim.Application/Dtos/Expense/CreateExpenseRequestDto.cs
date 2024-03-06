using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Expense
{
    public class CreateExpenseRequestDto
    {
        public Guid ExpenseCategoryId { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

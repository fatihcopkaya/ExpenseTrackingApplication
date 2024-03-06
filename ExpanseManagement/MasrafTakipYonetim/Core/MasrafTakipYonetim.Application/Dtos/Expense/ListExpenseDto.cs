using MasrafTakipYonetim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.Expense
{
    public class ListExpenseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public float Amount { get; set; }
        public string ExpenseCategoryName { get; set; }
        
        public ExpenseCategoryType ExpenseCategoryType { get; set; }
        public string ExpenseTypeName { get; set; }
    }
        
}


using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasrafTakipYonetim.Domain.Entities
{
    public class WalletByExpenseCategory : BaseEntity
    {
        [ForeignKey("ExpenseCategory")]
        public Guid ExpenseCategoryId { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public float TotalPaymentByExpenseCategory { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public float TotalExpenseByExpenseCategory { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }


}

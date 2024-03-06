using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasrafTakipYonetim.Domain.Entities
{
    public class Expense : BaseEntity
    {
        [ForeignKey("ExpenseCategory")]
        public Guid ExpenseCategoryId { get; set; }
        [ForeignKey("ExpenseType")]
        public Guid ExpenseTypeId { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public float Amount { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public ExpenseType ExpenseType { get; set; }
    }


}

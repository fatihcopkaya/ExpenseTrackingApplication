using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasrafTakipYonetim.Domain.Entities
{
    public class Payment : BaseEntity
    {
        [ForeignKey("ExpenseCategory")]
        public Guid ExpenseCategoryId { get; set; }
        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }
        public DateTime PeriodDate { get; set; }
        public DateTime PaymentDate { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public float PaymentAmount { get; set; }
        public bool IsPaid { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public AppUser AppUser { get; set; }
    }


}

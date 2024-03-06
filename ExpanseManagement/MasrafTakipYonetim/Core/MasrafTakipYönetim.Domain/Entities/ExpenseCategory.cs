using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;



namespace MasrafTakipYonetim.Domain.Entities
{
    public class ExpenseCategory : BaseEntity
    {

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        // [Required]
        // public Guid Id { get; set; }
        public List<UserExpenseCategory> UserExpenseCategory { get; set; }
        public Domain.Enums.ExpenseCategoryType ExpenseCategoryType { get; set; }
    }




}

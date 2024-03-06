using MasrafTakipYönetim.Domain.Entities.Common;
using MasrafTakipYonetim.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipYonetim.Domain.Entities
{
    public class ExpenseType : BaseEntity
    {

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        
    }

}

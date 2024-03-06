using MasrafTakipYönetim.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Domain.Entities
{
    public class UserExpenseCategory : BaseEntity
    {
        [ForeignKey("AppUser")]
        public Guid AppUserId { get; set; }

        //[InverseProperty("UserExpenseCategory")]// ikili ilişkilerdeki karışıklığı çözmek için kullanılıyormuş
        public AppUser AppUser { get; set; }

        public  Guid ExpenseCategoryId { get; set; }
        public ExpenseCategory? ExpenseCategory { get; set; }
    }
}

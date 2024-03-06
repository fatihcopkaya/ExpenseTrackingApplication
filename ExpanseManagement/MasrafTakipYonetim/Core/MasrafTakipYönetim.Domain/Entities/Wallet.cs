using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipYonetim.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public float TotalPayments { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public float TotalExpenses { get; set; }

    }


}



namespace MasrafTakipYonetim.Application.Dtos.ExpenseCategory
{
    public class UpdateExpenseCategoryRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Domain.Enums.ExpenseCategoryType ExpenseCategoryType { get; set; }
    }
}




namespace MasrafTakipYonetim.Application.Dtos.ExpenseCategory
{
    public class CreateExpenseCategoryRequestDto
    {
        public string Name { get; set; }
        public Domain.Enums.ExpenseCategoryType ExpenseCategoryType { get; set; }
    }
}

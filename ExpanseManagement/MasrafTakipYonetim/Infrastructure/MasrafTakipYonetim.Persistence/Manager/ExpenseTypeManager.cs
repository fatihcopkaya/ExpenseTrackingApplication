using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;



namespace MasrafTakipYonetim.Persistence.Manager
{
    public class ExpenseTypeManager : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        public ExpenseTypeManager(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
        }

       
    }
}

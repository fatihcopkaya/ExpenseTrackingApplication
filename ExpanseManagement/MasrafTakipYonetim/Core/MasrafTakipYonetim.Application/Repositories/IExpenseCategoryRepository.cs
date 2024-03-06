using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IExpenseCategoryRepository : IBaseRepository<ExpenseCategory>
    {
        Task<IDataResult<List<ExpenseCategory>>> GetExpenseCategoryListAsync();
        Task<IDataResult<ExpenseCategory>> GetExpenseCategoryByIdAsync(Guid expenseCategoryId);
        Task<IDataResult<ExpenseCategory>>CreateExpenseCategory(ExpenseCategory expenseCategory);
        Task<IDataResult<ExpenseCategory>>DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory);
        Task<IDataResult<ExpenseCategory>>UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory);
        
       
    }
}

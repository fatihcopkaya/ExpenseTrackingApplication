using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IExpenseTypeRepository : IBaseRepository<ExpenseType>
    {
        Task<IDataResult<List<ExpenseType>>> GetExpenseTypeListAsync();
        Task<IDataResult<ExpenseType>> GetExpenseTypeByIdAsync(Guid expenseTypeId);
        Task<IDataResult<ExpenseType>>CreateExpenseType(ExpenseType expenseType);
        Task<IDataResult<ExpenseType>> UpdateExpenseType(ExpenseType expenseType);
        Task<IDataResult<ExpenseType>> DeleteExpenseType(ExpenseType expenseType);
        




    }
}

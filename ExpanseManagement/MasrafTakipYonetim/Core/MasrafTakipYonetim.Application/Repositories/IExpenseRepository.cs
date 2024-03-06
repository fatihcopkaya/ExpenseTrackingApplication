using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
        Task<IDataResult<List<Expense>>> GetExpenseListAsync();
        Task<IDataResult<Expense>> GetExpenseByIdAsync(Guid expenseId);
        Task<IDataResult<Expense>> CreateExpenseAsync(Expense expense);
        Task<Expense>DeleteExpenseAsync(Expense expense);
        Task<IDataResult<Expense>> UpdateExpenseAsync(Expense expense);

    }
}

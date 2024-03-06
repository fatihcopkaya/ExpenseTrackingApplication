using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IWalletByExpenseCategoryRepository : IBaseRepository<WalletByExpenseCategory>
    {
        Task<IDataResult<List<WalletByExpenseCategory>>> GetExpenseListAsync();
        Task<IDataResult<WalletByExpenseCategory>> GetExpenseByIdAsync(Guid walletByExpenseCategoryId);
        Task<IDataResult<WalletByExpenseCategory>> CreateExpenseAsync(WalletByExpenseCategory expense);
        Task<IDataResult<WalletByExpenseCategory>> UpdateExpenseAsync(WalletByExpenseCategory expense);
    }
}

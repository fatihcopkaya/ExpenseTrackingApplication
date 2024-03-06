using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;

namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<IDataResult<List<Wallet>>> GetWalletListAsync();
        Task<IDataResult<Wallet>> GetWalletByIdAsync(Guid walletId);
        Task<IDataResult<Wallet>> UpdateWalletAsync(Wallet wallet);
        Task<(float totalPayments, float totalExpenses)> GetTotalPaymentsAndExpensesAsync();
    }
}

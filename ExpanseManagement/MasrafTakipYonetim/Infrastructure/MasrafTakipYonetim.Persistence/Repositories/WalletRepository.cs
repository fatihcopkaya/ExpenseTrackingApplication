using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<Wallet>> GetWalletByIdAsync(Guid walletId)
        {
            var row= await GetFirstOrDefaultAsync(x=>x.Id==walletId);
            if (row !=null)
            {
                return new SuccessDataResult<Wallet>(row);
            }
            return new ErrorDataResult<Wallet>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<List<Wallet>>> GetWalletListAsync()
        {
            var list = await GetListAsync(x => x.IsDeleted == false);
            if (list!=null)
            {
                return new SuccessDataResult<List<Wallet>>(list.ToList());
            }
            return new ErrorDataResult<List<Wallet>>(Messages.noRecordMessage); 
        }

        public async Task<IDataResult<Wallet>> UpdateWalletAsync(Wallet wallet)
        {
           await UpdateAsync(wallet);
            return new SuccessDataResult<Wallet>(Messages.updateMessage);
        }

        public async Task<(float totalPayments, float totalExpenses)> GetTotalPaymentsAndExpensesAsync()
        {
            float totalPayments = (await GetListAsync()).Sum(w => w.TotalPayments);
            float totalExpenses = (await GetListAsync()).Sum(w => w.TotalExpenses);

            return (totalPayments, totalExpenses);
        }



    }
}

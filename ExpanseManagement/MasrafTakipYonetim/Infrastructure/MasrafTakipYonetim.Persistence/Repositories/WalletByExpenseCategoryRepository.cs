using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class WalletByExpenseCategoryRepository : BaseRepository<WalletByExpenseCategory>, IWalletByExpenseCategoryRepository
    {
        public WalletByExpenseCategoryRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<WalletByExpenseCategory>> CreateExpenseAsync(WalletByExpenseCategory expense)
        {
           await AddAsync(expense);
            return new SuccessDataResult<WalletByExpenseCategory>(Messages.addMessage);
        }

        public async Task<IDataResult<WalletByExpenseCategory>> GetExpenseByIdAsync(Guid walletByExpenseCategoryId)
        {
           var row = await GetFirstOrDefaultAsync(x=>x.Id == walletByExpenseCategoryId);
            if (row !=null)
            {
                return new SuccessDataResult<WalletByExpenseCategory>(row);
            }

            return new ErrorDataResult<WalletByExpenseCategory>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<List<WalletByExpenseCategory>>> GetExpenseListAsync()
        {
            var list = await GetListAsync(x => x.IsDeleted == false);
            if (list != null)
            {
                return new SuccessDataResult<List<WalletByExpenseCategory>>(list.ToList());
            }

            return new ErrorDataResult<List<WalletByExpenseCategory>>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<WalletByExpenseCategory>> UpdateExpenseAsync(WalletByExpenseCategory expense)
        {
            await UpdateAsync(expense);
            return new SuccessDataResult<WalletByExpenseCategory>(Messages.updateMessage);
        }
    }
}

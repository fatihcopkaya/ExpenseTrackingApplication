using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        

        public async Task<IDataResult<Expense>> CreateExpenseAsync(Expense expense)
        {
           await AddAsync(expense);
            return new SuccessDataResult<Expense>(expense, Messages.addMessage);

        }

        public async Task<Expense>DeleteExpenseAsync(Expense expense)
        {
            await UpdateAsync(expense);
            return expense;
           
        }

        public async  Task<IDataResult<Expense>> GetExpenseByIdAsync(Guid expenseId)
        {
           var row = await GetFirstOrDefaultAsync(x=>x.Id == expenseId); 
            if (row != null) {

                return new SuccessDataResult<Expense>(row);
            }

            return new ErrorDataResult<Expense>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<List<Expense>>> GetExpenseListAsync()
        {
            var list = await GetListAsync(x => x.IsDeleted == false);
            if (list != null)
            {
                return new SuccessDataResult<List<Expense>>(list.ToList()); 
            }

            return new ErrorDataResult<List<Expense>>(Messages.noRecordMessage);
            
        }

        public async Task<IDataResult<Expense>> UpdateExpenseAsync(Expense expense)
        {
            await UpdateAsync(expense);
            return new SuccessDataResult<Expense>(expense,Messages.updateMessage);
        }
    }
}

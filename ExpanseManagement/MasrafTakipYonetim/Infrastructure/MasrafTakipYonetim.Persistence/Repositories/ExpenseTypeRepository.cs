using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class ExpenseTypeRepository : BaseRepository<ExpenseType>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<ExpenseType>> CreateExpenseType(ExpenseType expenseType)
        {
            await AddAsync(expenseType);
            return new SuccessDataResult<ExpenseType>(expenseType, Messages.addMessage);// mesaj bastırılcak mı//
        }

        public async Task<IDataResult<ExpenseType>> DeleteExpenseType(ExpenseType expenseType)
        {
            await UpdateAsync(expenseType);
            return new SuccessDataResult<ExpenseType>(Messages.deleteMessage);
        }

        public async Task<IDataResult<ExpenseType>> GetExpenseTypeByIdAsync(Guid expenseTypeId)
        {
            var row = await GetFirstOrDefaultAsync(x => x.Id == expenseTypeId);
            return row != null ? new SuccessDataResult<ExpenseType>(row) : new ErrorDataResult<ExpenseType>(Messages.noRecordMessage);

        }
        




        public async Task<IDataResult<List<ExpenseType>>> GetExpenseTypeListAsync()
        {
            var list = await GetListAsync(x=>x.IsDeleted == false);
            return new SuccessDataResult<List<ExpenseType>>(list.ToList());
        }

        public async Task<IDataResult<ExpenseType>> UpdateExpenseType(ExpenseType expenseType)
        {
            await UpdateAsync(expenseType);
            return new SuccessDataResult<ExpenseType>(Messages.updateMessage);
        }

       



    }


}

using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;


namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class ExpenseCategoriesRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoriesRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<ExpenseCategory>> CreateExpenseCategory(ExpenseCategory expenseCategory)
        {
            
            
                await AddAsync(expenseCategory);
                return new SuccessDataResult<ExpenseCategory>(expenseCategory,Messages.addMessage);
           
        }

        public async Task<IDataResult<ExpenseCategory>> DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
           
         
            await UpdateAsync(expenseCategory);
            return new SuccessDataResult<ExpenseCategory>(Messages.deleteMessage);
        }

        public async Task<IDataResult<ExpenseCategory>> GetExpenseCategoryByIdAsync(Guid expenseCategoryId)
        {
                var row = await GetFirstOrDefaultAsync(x => x.Id == expenseCategoryId);
                if (row != null)
                {
                    return new SuccessDataResult<ExpenseCategory>(row);

                }
                return new ErrorDataResult<ExpenseCategory>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<List<ExpenseCategory>>> GetExpenseCategoryListAsync()
        {
            var list
            = await GetListAsync(X => X.IsDeleted == false);

            if (list != null)
            {
                return new SuccessDataResult<List<ExpenseCategory>>(list.ToList());

            }
            return new ErrorDataResult<List<ExpenseCategory>>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<ExpenseCategory>> UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {

           
            
                await UpdateAsync(expenseCategory);
                return new SuccessDataResult<ExpenseCategory>(Messages.updateMessage);
            
           
        }
    }
    }
       


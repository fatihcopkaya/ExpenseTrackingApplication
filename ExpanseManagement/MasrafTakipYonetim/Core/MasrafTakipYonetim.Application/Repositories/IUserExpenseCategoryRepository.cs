using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IUserExpenseCategoryRepository : IBaseRepository<UserExpenseCategory>
    {
        List<UserExpenseCategory> GetListUserExpenseCategoryById(Guid Id);
        Task<UserExpenseCategory> DeleteUserExpenseCategory(UserExpenseCategory userExpenseCategory);

        Task<UserExpenseCategory> GetUserExpenseCategoryById(Guid userexpensecategoryId);

        Task<bool> HasDeletedUserExpenseCategoriesForAppUser(Guid userId);

        Task<UserExpenseCategory> UpdateUserExpenseCategory(UserExpenseCategory userExpenseCategory);

        public  Task<IDataResult<List<UserExpenseCategory>>> GetAppUserlistByExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType);
        Task<IDataResult<UserExpenseCategory>> CreateUserExpenseCategoryUser(UserExpenseCategory userExpenseCategory);

    }
}

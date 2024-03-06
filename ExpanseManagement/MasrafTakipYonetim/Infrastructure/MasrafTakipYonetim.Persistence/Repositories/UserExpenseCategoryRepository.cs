using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class UserExpenseCategoryRepository : BaseRepository<UserExpenseCategory>, IUserExpenseCategoryRepository
    {
        private readonly MasrafTakipYonetimDbContext _dbContext;
        public UserExpenseCategoryRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public List<UserExpenseCategory> GetListUserExpenseCategoryById(Guid Id)
        {
           var List = GetList(x=> x.AppUserId == Id);
            return List.ToList();
        }


        public async Task<UserExpenseCategory> GetUserExpenseCategoryById(Guid userexpensecategoryId)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == userexpensecategoryId, includes: new Expression<Func<UserExpenseCategory, object>>[] { x => x.AppUser, x => x.ExpenseCategory });

        }

        public async Task<UserExpenseCategory> DeleteUserExpenseCategory(UserExpenseCategory userExpenseCategory )
        {
            await UpdateAsync(userExpenseCategory);
            return userExpenseCategory;

        }

        public async Task<bool> HasDeletedUserExpenseCategoriesForAppUser(Guid userId)
        {
            // userId'ye ait silinmiş UserExpenseCategory kayıtlarını kontrol et
            return !await _dbContext.Set<UserExpenseCategory>()
                .AnyAsync(uec => uec.AppUserId == userId && !uec.IsDeleted);
        }


        public async Task<UserExpenseCategory> UpdateUserExpenseCategory(UserExpenseCategory userExpenseCategory)
        {
            _dbContext.Update(userExpenseCategory);
            await _dbContext.SaveChangesAsync();

            return userExpenseCategory;
        }

        public async Task<IDataResult<List<UserExpenseCategory>>> GetAppUserlistByExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType)
        {
            var list = await GetListAsync(x => x.IsDeleted == false && x.ExpenseCategory.ExpenseCategoryType == expenseCategoryType, includes: new Expression<Func<UserExpenseCategory, object>>[] { x => x.AppUser,x => x.ExpenseCategory});
            return new SuccessDataResult<List<UserExpenseCategory>>(list.ToList());
        }

        public async  Task<IDataResult<UserExpenseCategory>> CreateUserExpenseCategoryUser(UserExpenseCategory userExpenseCategory)
        {
            await AddAsync(userExpenseCategory);
            return new SuccessDataResult<UserExpenseCategory>(userExpenseCategory, Messages.addMessage);
        }

        //public async Task UpdateUserExpenseCategories(Guid userId, List<Guid> expenseCategoryIds)
        //{
        //    // Kullanıcının mevcut harcama kategorilerini sil
        //    await DeleteUserExpenseCategoriesForAppUser(userId);

        //    // Yeni harcama kategorilerini oluştur ve ekle
        //    var newUserExpenseCategories = expenseCategoryIds.Select(categoryId => new UserExpenseCategory
        //    {
        //        AppUserId = userId,
        //        ExpenseCategoryId = categoryId
        //    }).ToList();

        //    await AddAsync(newUserExpenseCategories);
        //}

    }
}

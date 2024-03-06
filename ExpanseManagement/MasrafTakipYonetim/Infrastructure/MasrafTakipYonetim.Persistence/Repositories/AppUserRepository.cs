using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Entities.Common;
using MasrafTakipYonetim.Domain.Enums;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    
    {
        public AppUserRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<AppUser> DeleteAppUser(AppUser appUser)
        {
            await UpdateAsync(appUser) ;
            return appUser;
           
        }

        public async Task<bool> CheckAppUserByEmailAsync(string appUserEmail)
        {
           var row =  await GetFirstOrDefaultAsync(x=>x.Email == appUserEmail);
            if(row == null)
            {
                return true;
            }
            return false;
            
        }

        public async Task<AppUser> GetAppUserById(Guid appUserId)
        {
            return await GetFirstOrDefaultAsync(x=>x.Id == appUserId);
           
        }

        public async Task<IDataResult<List<AppUser>>> GetAppUserList()
        {
            var list = await GetListAsync(x=>x.IsDeleted == false);
            if (list != null)
            {
                return new SuccessDataResult<List<AppUser>>(list.ToList());

            }
            return new ErrorDataResult<List<AppUser>>(Messages.noRecordMessage);
        }

        public Task<IDataResult<AppUser>> SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> UpdateAppUser(AppUser appUser)
        {
            await UpdateAsync(appUser);
            return appUser;
        }

        public async Task<AppUser> GetAppUserByEmailAsync(string appUserEmail)
        {
            var row=await GetFirstOrDefaultAsync(x=>x.Email== appUserEmail);

            return row;
            
        }

        public List<AppUser> GetAllAppUsers()
        {
            var list = GetListAsync(x=>x.IsDeleted == false);
           if(list != null) 
           {
                return list.Result.ToList();
           }
            return null;
        }

        public async Task<int> GetUserCountAsync()
        {
            var users = await GetListAsync();
            int userCount = users.Count();
            return userCount;
        }



        public async Task<IDataResult<List<AppUser>>> GetAppUsersByExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType)
        {
            var list = await GetListAsync(x => x.IsDeleted == false && x.UserExpenseCategory.Any(x => x.ExpenseCategory.ExpenseCategoryType == expenseCategoryType));

            return new SuccessDataResult<List<AppUser>>(list.ToList());
        }

        public async Task<IDataResult<List<AppUser>>> GetAppUserListWithRoles()
        {
            var list = await GetListAsync(x => x.IsDeleted == false, includes: new Expression<Func<AppUser, object>>[] { x => x.Role,x=>x.UserExpenseCategory });

            return new SuccessDataResult<List<AppUser>>(list.ToList());
        }
    }
}

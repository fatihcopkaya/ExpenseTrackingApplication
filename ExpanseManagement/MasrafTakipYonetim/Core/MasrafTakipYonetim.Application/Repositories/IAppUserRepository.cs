using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        Task<bool> CheckAppUserByEmailAsync(string appUserEmail);
        Task<AppUser> GetAppUserByEmailAsync(string appUserEmail);

        Task<IDataResult<List<AppUser>>>GetAppUsersByExpenseCategoryTypeAsync(ExpenseCategoryType expenseCategoryType);
        Task<IDataResult<AppUser>> SignInAsync(string email, string password);       
        Task<IDataResult<List<AppUser>>> GetAppUserList();
        Task<AppUser> GetAppUserById(Guid appUserId);
        
        Task<AppUser> DeleteAppUser(AppUser appUser);
        List<AppUser> GetAllAppUsers();
        Task<int> GetUserCountAsync();

        Task<AppUser> UpdateAppUser(AppUser appUser);
        Task<IDataResult<List<AppUser>>> GetAppUserListWithRoles();


    }
}

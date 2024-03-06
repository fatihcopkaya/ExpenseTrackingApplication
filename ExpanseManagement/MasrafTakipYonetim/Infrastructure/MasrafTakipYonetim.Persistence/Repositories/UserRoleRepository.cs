using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using System.Linq.Expressions;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<UserRole>> CreateUserRole(UserRole userRole)
        {
            await AddAsync(userRole);
            return new SuccessDataResult<UserRole>(userRole, Messages.addMessage);
        }

        public  async Task<IDataResult<UserRole>> DeleteUserRole(UserRole userRole)
        {
            await UpdateAsync(userRole);
            return new SuccessDataResult<UserRole>(Messages.deleteMessage);
        }

        public async Task<IDataResult<UserRole>> GetUserRoleById(Guid Id)
        {
           var row= await GetFirstOrDefaultAsync(x=>x.Id==Id);
            if (row != null)
            {
                return new SuccessDataResult<UserRole>(row);
            }

            return new ErrorDataResult<UserRole>(Messages.noRecordMessage);
        }
       

        public async Task<IDataResult<List<UserRole>>> GetUserRoleList()
        {
            var list = await GetListAsync(x => x.IsDeleted == false  , includes: new Expression<Func<UserRole, object>>[] { x => x.User, x => x.Role });
            
            if (list != null)
            {
                return new SuccessDataResult<List<UserRole>>(list.ToList());    
            }

            return new ErrorDataResult<List<UserRole>>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<UserRole>> GetUserRoleUserId(Guid Id)
        {
            var role = await GetFirstOrDefaultAsync(x=>x.AppUserId==Id,includes:x=>x.Role);
            return new SuccessDataResult<UserRole>(role);
        }

        public async Task<IDataResult<UserRole>> UpdateUserRole(UserRole userRole)
        {
            await UpdateAsync(userRole);
            return new SuccessDataResult<UserRole>(Messages.updateMessage);
        }
    }
}

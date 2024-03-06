using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<IDataResult<List<UserRole>>> GetUserRoleList();
        Task<IDataResult<UserRole>> GetUserRoleById(Guid Id);
        Task<IDataResult<UserRole>> CreateUserRole(UserRole userRole);
        Task<IDataResult<UserRole>> UpdateUserRole(UserRole userRole);
        Task<IDataResult<UserRole>> DeleteUserRole(UserRole userRole);
        Task<IDataResult<UserRole>> GetUserRoleUserId(Guid Id);

    }
}

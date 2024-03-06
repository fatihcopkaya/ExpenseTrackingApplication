using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;

namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        Task<IDataResult<List<Permission>>> GetPermissionListAsync();
        Task<IDataResult<Permission>> GetPermissionByIdAsync(Guid Id);
        Task<IDataResult<Permission>> CreatePermissionAsync(Permission permission);
        Task<IDataResult<Permission>> UpdatePermissionAsync(Permission permission);
        Task<IDataResult<Permission>> DeletePermissionAsync(Permission permission);
        Task<IDataResult<Permission>> GetPermissionAsyncByPermissionId(Guid Id);
    }
}


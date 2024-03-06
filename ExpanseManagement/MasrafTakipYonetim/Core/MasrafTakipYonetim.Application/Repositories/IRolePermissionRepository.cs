using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IRolePermissionRepository : IBaseRepository<RolePermission>
    {
        Task<IDataResult<List<RolePermission>>> GetRolePermissionList();
        Task<IDataResult<RolePermission>> GetRolePermissionById(Guid Id);
        Task<IDataResult<RolePermission>>CreateRolePermission(RolePermission rolepermission);
        Task<IDataResult<RolePermission>>UpdateRolePermission(RolePermission rolepermission);
        Task<IDataResult<RolePermission>> DeleteRolePermission(RolePermission rolepermission);
        Task<IDataResult<List<RolePermission>>> GetRolePermissionByRoleId(Guid Id);
        Task<IDataResult<IEnumerable<RolePermission>>> GetRolePermissionsByRoleId(Guid roleId);
    }
}

using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Repositories
{
    public interface IRoleRepository : IBaseRepository<Roles>
    {
        Task<IDataResult<List<Roles>>> GetRoleList();
        Task<IDataResult<Roles>> GetRoleById(Guid Id);
        Task<IDataResult<Roles>> CreateRole(Roles roles);
        Task<IDataResult<Roles>> UpdateRoleAsync(Roles roles);
        Task<IDataResult<Roles>>DeleteRoleAsync(Roles roles);

    }
}

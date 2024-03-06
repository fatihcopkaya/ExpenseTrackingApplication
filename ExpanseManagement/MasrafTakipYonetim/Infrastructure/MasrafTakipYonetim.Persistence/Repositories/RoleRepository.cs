using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Roles>, IRoleRepository
    {
        public RoleRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<Roles>> CreateRole(Roles roles)
        {
            await AddAsync(roles);
            return new SuccessDataResult<Roles>(roles,Messages.addMessage);
        }

        public async Task<IDataResult<Roles>> DeleteRoleAsync(Roles roles)
        {
           await UpdateAsync(roles);
            return new SuccessDataResult<Roles>(roles, Messages.deleteMessage);
        }

        public async Task<IDataResult<Roles>> GetRoleById(Guid Id)
        {
            var row = await GetFirstOrDefaultAsync(x => x.Id == Id);
            return row != null ? new SuccessDataResult<Roles>(row) : new ErrorDataResult<Roles>(Messages.noRecordMessage); 
        }

        public async Task<IDataResult<List<Roles>>> GetRoleList()
        {
            var list = await GetListAsync(x => x.IsDeleted);
            return list != null ? new SuccessDataResult<List<Roles>>(list.ToList()) : new ErrorDataResult<List<Roles>>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<Roles>> UpdateRoleAsync(Roles roles)
        {
            await UpdateAsync(roles);
            return new SuccessDataResult<Roles>(roles,Messages.updateMessage);
        }
    }
}

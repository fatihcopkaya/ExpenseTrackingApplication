using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
        }

        public async Task<IDataResult<Permission>> CreatePermissionAsync(Permission permission)
        {
           await AddAsync(permission);
            return new SuccessDataResult<Permission>(permission,Messages.addMessage);
        }

        public async Task<IDataResult<Permission>> GetPermissionByIdAsync(Guid Id)
        {
            var row = await GetFirstOrDefaultAsync(x => x.Id == Id);
            return row != null ? new SuccessDataResult<Permission>(row) : new ErrorDataResult<Permission>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<List<Permission>>> GetPermissionListAsync()
        {
            var list = await GetListAsync(x => x.IsDeleted);
            return list != null ? new SuccessDataResult<List<Permission>>(list.ToList()) : new ErrorDataResult<List<Permission>>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<Permission>> UpdatePermissionAsync(Permission permission)
        {
           await UpdateAsync(permission);
            return new SuccessDataResult<Permission>(Messages.updateMessage);
        }
        public async Task<IDataResult<Permission>> DeletePermissionAsync(Permission permission)
        {
            await UpdateAsync(permission);
            return new SuccessDataResult<Permission>(Messages.deleteMessage);
        }

        public async  Task<IDataResult<Permission>> GetPermissionAsyncByPermissionId(Guid Id)
        {
            var list = await GetFirstOrDefaultAsync(x => x.IsDeleted==false && x.Id== Id);
            return list != null ? new SuccessDataResult<Permission>(list) : new ErrorDataResult<Permission>(Messages.noRecordMessage);
        }
    }
}

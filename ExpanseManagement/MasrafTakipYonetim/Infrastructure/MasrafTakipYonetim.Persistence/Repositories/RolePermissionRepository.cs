using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Context;
using MasrafTakipYonetim.Persistence.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace MasrafTakipYonetim.Persistence.Repositories
{
    public class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
    {
        private readonly MasrafTakipYonetimDbContext _dbContext;
        public RolePermissionRepository(MasrafTakipYonetimDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IDataResult<RolePermission>> CreateRolePermission(RolePermission rolepermission)
        {
            await AddAsync(rolepermission);
            return new SuccessDataResult<RolePermission>(rolepermission,Messages.addMessage);
        }

        public async Task<IDataResult<RolePermission>> DeleteRolePermission(RolePermission rolepermission)
        {
            await UpdateAsync(rolepermission);
            return new SuccessDataResult<RolePermission>(Messages.deleteMessage);
        }

        public async Task<IDataResult<RolePermission>> GetRolePermissionById(Guid Id)
        {
            var row = await GetFirstOrDefaultAsync(x => x.Id == Id);
            return row != null ? new SuccessDataResult<RolePermission>(row) : new ErrorDataResult<RolePermission>(Messages.noRecordMessage);
         
            
        }

        public async Task<IDataResult<List<RolePermission>>> GetRolePermissionByRoleId(Guid Id)
        {
            var row = await GetListAsync(x => x.RoleId == Id,includes:x=>x.Permission);
            return row != null ? new SuccessDataResult<List<RolePermission>>(row.ToList()) : new ErrorDataResult<List<RolePermission>>(Messages.noRecordMessage);
        }

        public async Task<IDataResult<List<RolePermission>>> GetRolePermissionList()
        {


            var list = await GetListAsync(x => x.IsDeleted == false , includes: new Expression<Func<RolePermission, object>>[] { x => x.Role, x => x.Permission });
            return list != null ? new SuccessDataResult<List<RolePermission>>(list.ToList()) : new ErrorDataResult<List<RolePermission>>(Messages.noRecordMessage);

        }



        public async Task<IDataResult<RolePermission>> UpdateRolePermission(RolePermission rolepermission)
        {

            await UpdateAsync(rolepermission);
            return new SuccessDataResult<RolePermission>(rolepermission, Messages.updateMessage);
        }


        public async Task<IDataResult<IEnumerable<RolePermission>>> GetRolePermissionsByRoleId(Guid roleId)
        {
            var rolePermissions = await _dbContext.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync();

            if (rolePermissions.Any())
            {
                return new SuccessDataResult<IEnumerable<RolePermission>>(rolePermissions);
            }
            else
            {
                return new ErrorDataResult<IEnumerable<RolePermission>>(Messages.noRecordMessage);
            }
        }

    }
}

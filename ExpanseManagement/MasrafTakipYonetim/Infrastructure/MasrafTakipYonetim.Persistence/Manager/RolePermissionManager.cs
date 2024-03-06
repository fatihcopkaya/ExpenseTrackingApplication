using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;

namespace MasrafTakipYonetim.Persistence.Manager
{
    public class RolePermissionManager : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionManager(IRolePermissionRepository roleRepository)
        {
            _rolePermissionRepository = roleRepository;
        }

        
    }
}

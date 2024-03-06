using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;


namespace MasrafTakipYonetim.Persistence.Manager
{
    public class PermissionManager : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionManager(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

       
    }
}

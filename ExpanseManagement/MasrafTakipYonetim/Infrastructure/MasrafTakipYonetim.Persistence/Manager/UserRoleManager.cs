using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;

namespace MasrafTakipYonetim.Persistence.Manager
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleManager(IUserRoleRepository roleRepository)
        {
            _userRoleRepository = roleRepository;
        }

       
    }
}

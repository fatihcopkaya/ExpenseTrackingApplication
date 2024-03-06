using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.JWT;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;
using Microsoft.Extensions.Configuration;


namespace MasrafTakipYonetim.Infrastructure.Features.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IPermissionRepository _permissionRepository;
        public AuthenticationHandler(IAuthenticationService authenticationService, IUserRoleRepository userRoleRepository, IRolePermissionRepository rolePermissionRepository, IPermissionRepository permissionRepository)
        {
            _authenticationService = authenticationService;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
           var infoControl = await _authenticationService.SignInAsyncWithToken(request.Email, request.Password);
            if(!infoControl.Success) 
            {
                throw new MasrafTakipCustomException("Giriş başarısız",401);

            }
            string firstAndLastName = infoControl.Data.FirstName + " " + infoControl.Data.LastName;
            string notEmptyFirstName = infoControl.Data.FirstName.Trim();
           
            var permission = await _rolePermissionRepository.GetRolePermissionByRoleId(infoControl.Data.RoleId);
            
            List<Permission> PermissionNameList= new List<Permission>();
            foreach (var item in permission.Data)
            {
               
                PermissionNameList.Add(item.Permission);
            }
            var tokenVM = JWToken.GetToken(infoControl.Data.Email,PermissionNameList,firstAndLastName);
            return new AuthenticationResponse { TokenVM = tokenVM,
                Message = Messages.succesfulLogin,
                FirstName = infoControl.Data.FirstName,
                LastName = infoControl.Data.LastName
            };
           
         }
    }
}

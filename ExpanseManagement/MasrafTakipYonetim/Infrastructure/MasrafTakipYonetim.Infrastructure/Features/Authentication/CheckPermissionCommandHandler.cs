using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AuthenticaitonRequestAndResponses;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results = MasrafTakipYonetim.Application.Commons.Results;

namespace MasrafTakipYonetim.Infrastructure.Features.Authentication
{
    public class CheckPermissionCommandHandler : IRequestHandler<CheckPermissionCommandRequest, Results>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRoleRepository _userRoleRepository;

        public CheckPermissionCommandHandler(IHttpContextAccessor httpContextAccessor, IUserRoleRepository userRoleRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepository = userRoleRepository;

        }

        public async Task<Results> Handle(CheckPermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                return Results.Failure(new List<string> { "Kullanıcı bilgileri bulunamadı." });
            }

            var userRoleResult = await _userRoleRepository.GetUserRoleUserId(new Guid(userId));
            if (userRoleResult.Success)
            {
                var userRole = userRoleResult.Data; // Verileri almak için Data özelliğini kullanın

                // Kullanıcının rolüne buradan erişebilirsiniz.
                if (userRole.Role.Name == request.Permission)
                {
                    return Results.Success();
                }
            }
            return Results.Failure(new List<string> { "Yetkisiz işlem" });

        }

    }
}

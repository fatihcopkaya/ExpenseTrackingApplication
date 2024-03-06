using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Services
{
    public interface IAuthenticationService
    {

        Task<IDataResult<ClaimsPrincipal>> GetByRefreshTokenAsync(string userRefreshToken);
        //Task PasswordResetAsync(string email);
        Task<IDataResult<AppUser>> SignInAsyncWithToken(string email, string password);
        //Task<IResult> PasswordResetAsync(string email);
        Task<IResult> PasswordResetAsync(string email);
        Task<bool>VerifyResetTokenAsync(string resetToken,Guid userId);
        Task UpdatePassword(Guid userId,string resetToken,string newPassword);

    }
}

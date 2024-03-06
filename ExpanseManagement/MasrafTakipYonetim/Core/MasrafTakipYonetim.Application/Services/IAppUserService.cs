using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;

namespace MasrafTakipYonetim.Application.Services
{
    public interface IAppUserService 
    {
        
        Task<IDataResult<AppUser>> GetAuthenticatedUser();
        Task<IDataResult<AppUser>> SignInAsyncWithToken(string email, string password);
        Task<IDataResult<AppUser>> CreateAppUser(AppUser user);
        Task<bool> VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt);
        (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password);


    }
}

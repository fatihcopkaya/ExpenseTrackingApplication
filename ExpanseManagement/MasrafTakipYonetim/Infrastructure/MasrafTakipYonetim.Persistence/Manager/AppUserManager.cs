using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Security.Hashing;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Security.Claims;


namespace MasrafTakipYonetim.Persistence.Manager
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public AppUserManager(IAppUserRepository appUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _appUserRepository = appUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<AppUser>> CreateAppUser(AppUser user)
        {
            var checkEmail = await _appUserRepository.CheckAppUserByEmailAsync(user.Email.Trim());
            if(checkEmail==false)
            {
                return new ErrorDataResult<AppUser>("Email already exist");
            }
            HashingHelper.CreatePasswordHash(user.Password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsDeleted = false;
            await _appUserRepository.AddAsync(user);
            return new SuccessDataResult<AppUser>(user, Messages.addMessage);
        }


        public (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password)
        {
            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            return (passwordHash, passwordSalt);
        }
        public async Task<bool>VerifyPasswordHash(string password,byte[] passwordhash, byte[] passwordsalt)
        {
           var verifypassword= HashingHelper.VerifyPasswordHash(password, passwordhash, passwordsalt);
           
            if (verifypassword== true)
            {
                return true;
            }
            return false;
        }

        public async Task<IDataResult<AppUser>> GetAuthenticatedUser()
        {
            string usermail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
           
            var userId = (await _appUserRepository.GetListAsync()).Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            var row = await _appUserRepository.GetFirstOrDefaultAsync(x => x.Id == userId);
            if (row != null)
            {
                return new SuccessDataResult<AppUser>(row);
            }
            return new SuccessDataResult<AppUser>(Messages.recordMessage);

        }

        //sonradan eklenecek
        public Task<IDataResult<AppUser>> SignInAsyncWithToken(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}

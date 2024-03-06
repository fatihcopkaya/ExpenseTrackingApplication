using MasrafTakipYonetim.Application.Helpers;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.JWT;
using MasrafTakipYonetim.Persistence.Contacts;
using MasrafTakipYonetim.Persistence.Security.Hashing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MasrafTakipYonetim.Persistence.Manager
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMailHelper _mailHelper;

        public AuthenticationService(IAppUserRepository appUserRepository, IMailHelper mailHelper)
        {
            _appUserRepository = appUserRepository;
            _mailHelper = mailHelper;
        }

       

        public async Task<IDataResult<ClaimsPrincipal>> GetByRefreshTokenAsync(string userRefreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecretKey = Encoding.UTF8.GetBytes("3KBsVR697nrsqxfvvjlZDw==");

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7044",
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:7044",
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(userRefreshToken, tokenValidationParameters, out validatedToken);

                return new SuccessDataResult<ClaimsPrincipal>(principal);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ClaimsPrincipal>(null, "Invalid token");
            }
        }

        public async Task<IResult> PasswordResetAsync(string email)
        {
            try
            {
                AppUser user = await _appUserRepository.GetAppUserByEmailAsync(email);
                if (user != null)
                {
                    // Parola sıfırlama tokeni oluşturuluyor
                    TokenVM resetToken = JWToken.GeneratePasswordResetToken(email,user.Id);

                    // Oluşturulan token ve e-posta adresiyle sıfırlama işlemi için gerekli adımların atılması
                    //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken.AccessToken);//reset token olabilir
                    //string encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);
                    string token = resetToken.AccessToken;
                    await _mailHelper.SendPasswordResetMailAsync(email, user.Id, token);

                    return new SuccessResult("Şifre Yenileme Başarıyla Tamamlandı.");
                }
                return new ErrorResult("Kullanıcı Bulunamadı.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Error occurred: {ex.Message}");
            }
        }

        public async Task<IDataResult<TokenVM>> RefreshTokenLoginAsync(string refreshToken)
        {
        //    try
        //    {
        //        string fatih = "fatih";//geçici /düzeltilecek
        //        var principal = await GetByRefreshTokenAsync(refreshToken);

        //        if (principal != null)
        //        {
        //            var emailClaim = principal.Data.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        //            var roleClaim = principal.Data.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        //            //var nameClaim = principal.Data.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        //            if (emailClaim != null && roleClaim != null /*&& nameClaim != null*/)
        //            {
        //                var email = emailClaim.Value;
        //                var role = roleClaim.Value;
        //                //var name = nameClaim.Value;

        //                var tokenVM = JWToken.GetToken(email, role,fatih);

        //                return new SuccessDataResult<TokenVM>(tokenVM);
        //            }
        //        }

        //        return new ErrorDataResult<TokenVM>("Unauthorized");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorDataResult<TokenVM>(ex.Message);
        //    }
            return null;
        }
        public async Task<IDataResult<AppUser>> SignInAsyncWithToken(string email, string password)
        {
            var row = await _appUserRepository.GetAppUserByEmailAsync(email);
            if (row != null)
            {
                var isPasswordValid = HashingHelper.VerifyPasswordHash(password, row.PasswordHash, row.PasswordSalt);
                if (isPasswordValid)
                    return new SuccessDataResult<AppUser>(row, Messages.succesfulLogin);
            }
            return new ErrorDataResult<AppUser>(new AppUser(), Messages.userNotFound);
        }

       

        public async Task<bool> VerifyResetTokenAsync(string resetToken, Guid userId)
        {
            AppUser user = await _appUserRepository.GetAppUserById(userId);
            if (user != null)
            {

                try
                {

                    var claimsPrincipal = await JWToken.VerifyUserTokenAsync(resetToken); // VerifyTokenAsync fonksiyonunu çağırarak tokeni doğrula

                    // Token doğrulama başarılıysa ve kullanıcı eşleşiyorsa, işlemin başarılı olduğunu döndür
                    // Örneğin, token içerisindeki kullanıcı id'si ile userId eşleşiyor mu kontrol edebilirsiniz
                    // Burada bir örnek kontrol yapısı oluşturulabilir:

                    // Kullanıcı ID'sini token içerisindeki id ile karşılaştır
                    var tokenUserId = claimsPrincipal.FindFirst("UserId")?.Value;
                    // Örnek olarak NameIdentifier kullanıldı, gerçek duruma göre uygun olanı kullanın
                    if (!string.IsNullOrEmpty(tokenUserId) && tokenUserId == userId.ToString())
                    {
                        return true; // Kullanıcı eşleşiyorsa başarılı
                    }
                }
                catch (Exception ex)
                {
                    // Token doğrulama başarısız olduğunda yapılacak işlemler buraya yazılabilir
                    throw new Exception("Token doğrulama başarısız oldu.", ex);
                }
            }
            return false;
            //byte[] tokenBytes= WebEncoders.Base64UrlDecode(resetToken);
            //    resetToken=Encoding.UTF8.GetString(tokenBytes);
            //    byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken.AccessToken);//reset token olabilir
            //    string encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

               


            
        }
        public async Task UpdatePassword(Guid userId, string resetToken, string newPassword)
        {
            try
            {
                AppUser user = await _appUserRepository.GetAppUserById(userId);
                if (user != null)
                {
                    // Yeni parolayı hashle
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                    // Kullanıcının parola bilgisini güncelle
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    // Güncellenmiş kullanıcı bilgisini kaydet
                    await _appUserRepository.UpdateAppUser(user);
                }
                else
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Parola güncelleme hatası: {ex.Message}");
            }

        }
    }
}

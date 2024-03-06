using MasrafTakipYonetim.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;



namespace MasrafTakipYonetim.Infrastructure.JWT
{
    public class JWToken
    {
       

        public static TokenVM GetToken(string email, List<Permission> role,string fullName)
        {
            
            var launchSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MasrafTakipYonetim.API/appsettings.json");
            var configuration = JsonConfigurationExtensions.AddJsonFile(new ConfigurationBuilder(), launchSettingsPath).Build();

            string applicationUrl = configuration["Jwt:ValidAudience"];
            string[] urls = applicationUrl.Split(';');
            string selectedUrl = urls.FirstOrDefault(url => url.Contains("localhost"));
            var secretkey = configuration["Jwt:Secret"];
            var identity = new List<Claim>
             {
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.Email, email),
                   
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             };
            foreach (var item in role)
            {
                identity.Add(new Claim(ClaimTypes.Role,item.PermissionName));
            }
            TokenVM tokenVM = new TokenVM();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            SigningCredentials signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken securityToken = new
            (   issuer : selectedUrl,
                audience : selectedUrl,
                expires :DateTime.UtcNow.AddHours(48),
                claims: identity,
                signingCredentials : signingCredentials
            );



        //Token oluşturucu sınıfında bir örnek alıyoruz.
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            //Token üretiyoruz.
            tokenVM.AccessToken = tokenHandler.WriteToken(securityToken);

            tokenVM.Expiration = DateTime.Now.AddMinutes(10);

            //Refresh Token üretiyoruz.
            tokenVM.RefreshToken = CreateRefreshToken();
            tokenVM.RefreshTokenEndDate = DateTime.Now.AddDays(10);
            return tokenVM;
        }

        public static string CreateRefreshToken() //Refresh Token üretecek metot.
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
        public static TokenVM GeneratePasswordResetToken(string email,Guid userId)
        {
            var launchSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MasrafTakipYonetim.API/appsettings.json");
            var configuration = JsonConfigurationExtensions.AddJsonFile(new ConfigurationBuilder(), launchSettingsPath).Build();

            string applicationUrl = configuration["Jwt:ValidAudience"];
            string[] urls = applicationUrl.Split(';');
            string selectedUrl = urls.FirstOrDefault(url => url.Contains("localhost"));
            var secretkey = configuration["Jwt:Secret"];
            var authClaims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, email),
            new Claim("UserId", userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // Burada istediğiniz diğer talepleri (claims) ekleyebilirsiniz.
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var token = new JwtSecurityToken(
                issuer: selectedUrl,
                audience: selectedUrl,
                expires: DateTime.Now.AddHours(2), // Token'in geçerlilik süresini isteğinize göre ayarlayın
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);

            //JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //TokenVM tokenVM = new TokenVM
            var tokenVM=new TokenVM
            {
                AccessToken = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo
            };

            return tokenVM;
        }

       


        public static async Task<ClaimsPrincipal> VerifyUserTokenAsync(string token)
        {
            try
            {
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                var configuration = configBuilder.Build();

                string validAudience = configuration["Jwt:ValidAudience"];
                string validIssuer = configuration["Jwt:ValidIssuer"];
                string secretKey = configuration["Jwt:Secret"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkeyBytes),
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                return claimsPrincipal;
            }
            catch (Exception ex)
            {
                // Hata yönetimi burada yapılır
                throw new Exception("Token doğrulama başarısız oldu.", ex);
            }
        }

    }
}

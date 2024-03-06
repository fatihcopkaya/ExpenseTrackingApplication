

namespace MasrafTakipYonetim.Infrastructure.JWT
{
    public class TokenVM
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }

    }
}

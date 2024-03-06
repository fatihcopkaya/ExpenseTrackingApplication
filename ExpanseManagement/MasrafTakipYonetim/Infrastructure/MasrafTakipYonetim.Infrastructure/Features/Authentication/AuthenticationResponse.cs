using MasrafTakipYonetim.Infrastructure.JWT;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.Authentication
{
    public class AuthenticationResponse : IRequest<AuthenticationRequest>
    {
        public TokenVM? TokenVM { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

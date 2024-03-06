using MasrafTakipYonetim.Application.Dtos.Authentication;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.Authentication
{
    public class AuthenticationRequest : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        //public SigninDto SigninDto { get; set; }
    }
}

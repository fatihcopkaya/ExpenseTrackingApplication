using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AuthenticaitonRequestAndResponses;
using MasrafTakipYonetim.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.Authentication
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, Results>
    {
        private readonly IAuthenticationService _authenticationService;

        public UpdatePasswordCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<Results> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Parolaların eşleşip eşleşmediğini kontrol et
                if (request.Password != request.PasswordConfirm)
                {
                    return Results.Failure(new string[] { "Parolalar eşleşmiyor." });
                }

                // Tokenın doğruluğunu ve kullanıcının varlığını doğrula
                bool isTokenValid = await _authenticationService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
                if (!isTokenValid)
                {
                    return Results.Failure(new string[] { "Geçersiz token veya kullanıcı." });
                }

                // Parolayı güncelle
                await _authenticationService.UpdatePassword(request.UserId, request.ResetToken, request.Password);

                return Results.Success();
            }
            catch (Exception ex)
            {
                return Results.Failure(new string[] { ex.Message });
            }
        }
    
    }
}

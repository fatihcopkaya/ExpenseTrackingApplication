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
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, Results>
    {
       private readonly IAuthenticationService _authenticationService;

        public PasswordResetCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Results> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            await _authenticationService.PasswordResetAsync(request.Email);
            return Results.Success();
        }
    }
}

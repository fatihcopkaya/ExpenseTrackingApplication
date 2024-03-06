using MasrafTakipYonetim.Application.Cqrs.Commands.AuthenticaitonRequestAndResponses;
using MasrafTakipYonetim.Infrastructure.Features.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        //[AllowAnonymous]
       
        public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
        {
            //AuthenticationResponse response = await _mediator.Send(authenticationRequest);
            return Ok(await _mediator.Send(authenticationRequest));

        }
        [HttpPost("Password-reset")]
        //[AllowAnonymous]
       
        public async Task<IActionResult> PasswordReset([FromBody]PasswordResetCommandRequest passwordResetCommandRequest)
        {
           
            return Ok(await _mediator.Send(passwordResetCommandRequest));
        }
        [HttpPost("Verify-Reset-Token")]
        //[AllowAnonymous]
       
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
            
        }
        [HttpPost("UpdatePassword")]
        //[AllowAnonymous]
        
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
          
            return Ok(await _mediator.Send(updatePasswordCommandRequest));

        }
        [HttpGet("CheckPermission")]
        //[AllowAnonymous]
       
        public async Task<IActionResult> CheckPermission([FromQuery] CheckPermissionCommandRequest checkPermissionCommandRequest)
        {
            return Ok(await this._mediator.Send(checkPermissionCommandRequest));
        }



    }
}

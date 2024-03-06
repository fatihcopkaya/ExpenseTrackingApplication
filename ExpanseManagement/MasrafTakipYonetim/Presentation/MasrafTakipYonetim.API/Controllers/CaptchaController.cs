using Azure.Core;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Domain.Entities;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using MasrafTakipYonetim.Application.Commons;
using Microsoft.EntityFrameworkCore;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Cqrs.Queries.Captcha;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CaptchaController:ControllerBase
    {
        private readonly IMediator _mediator;


        public CaptchaController(IMediator mediator)
        {
            _mediator = mediator;

        }
        
        [HttpGet("Get-Captcha")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetCaptcha([FromQuery] GetCaptchaRequest getCaptchaRequest)
        {
            return Ok(await _mediator.Send(getCaptchaRequest));
        }
        [HttpPost("Verify-Captcha")]
        //[AllowAnonymous]
        public async Task<IActionResult> VerifyCaptcha([FromBody] VerifyCaptchaRequest verifyCaptchaRequest)
        {
            return Ok(await _mediator.Send(verifyCaptchaRequest));
        }

    }
}

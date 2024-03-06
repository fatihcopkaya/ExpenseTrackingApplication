using MasrafTakipYonetim.Application.Cqrs.Commands.ChangePasswordRequestAndResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChangePasswordController(IMediator mediator)
        {
           _mediator = mediator;
        }

        [HttpPost("ChangePassword")]
        [Authorize(Roles = "Şifre Değiştir")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
           
            return Ok(await _mediator.Send(request));
        }

    }
}

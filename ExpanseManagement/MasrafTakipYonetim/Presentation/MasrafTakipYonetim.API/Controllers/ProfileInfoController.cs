using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Commands.ProfileInfoRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileInfoController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPut("UpdateProfileInfo")]
        [Authorize(Roles = "Profil Bilgisi Düzenle")]
        public async Task<IActionResult> UpdateProfileInfo(UpdateProfileInfoCommandRequest updateProfileInfoCommandRequest)
        {

            

            // Eğer bir hata kontrolü yapmanız gerekiyorsa, burada sonucu kontrol edebilirsiniz.

            return Ok(await _mediator.Send(updateProfileInfoCommandRequest));

            //return Ok(await _mediator.Send(updateAppUserCommandRequest));


        }
        [HttpGet("GetProfileInfoById")]
        [Authorize(Roles = "Profil Bilgisi Gör")]
        public async Task<IActionResult> GetProfileInfoById([FromQuery] GetProfileInfoByIdQueryRequest getProfileInfoByIdQueryRequest)
        {

            GetProfileInfoByIdQueryResponse response = await _mediator.Send(getProfileInfoByIdQueryRequest);
            return Ok(response);
        }

    }
}

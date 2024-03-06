using MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.PermissionQuery;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery;
using MasrafTakipYonetim.Infrastructure.Features.PermissionCommand;
using MasrafTakipYonetim.Infrastructure.Features.PermissionCommands;
using MasrafTakipYonetim.Infrastructure.Features.PermissionQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Yetki Oluştur")]
        public async Task<IActionResult> AddPermission(CreatePermissionCommandRequest createPermissionCommandRequest)
        {
          
            return Ok(await _mediator.Send(createPermissionCommandRequest));

        }
        [HttpPut("UpdatePermission")]
        [Authorize(Roles = "Yetki Düzenle")]
        public async Task<IActionResult> UpdatePermission(UpdatePermissionCommandRequest updatePermissionCommandRequest)
        {
           
            return Ok(await _mediator.Send(updatePermissionCommandRequest));

        }
        [HttpDelete("DeletePermission")]
        [Authorize(Roles = "Yetki Sil")]
        public async Task<IActionResult> DeletePermission(DeletePermissionCommandRequest deletePermissionCommandRequest)
        {
           
            return Ok(await _mediator.Send(deletePermissionCommandRequest));
        }
        [HttpPost("ListPermission")]
        [Authorize(Roles = "Yetki Listele")]
        public async Task<IActionResult> ListPermission(GetListPermissionQueryRequest getListPermissionQueryRequest)
        {
        
            return Ok(await _mediator.Send(getListPermissionQueryRequest));
        }

        [HttpGet("ListPermissionByRoleId")]
       // [Authorize(Roles = "Yetki Listele")]
        public async Task<IActionResult> ListPermissionByRoleId([FromQuery]ListPermissionByRoleIdRequest permissionByRoleIdRequest)
        {

            return Ok(await _mediator.Send(permissionByRoleIdRequest));
        }
    }
}

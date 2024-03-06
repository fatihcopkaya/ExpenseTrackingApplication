using MasrafTakipYonetim.Application.Cqrs.Commands.RoleUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Commands.UserRoleRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController: ControllerBase
    {
        private readonly IMediator _mediator;
        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateUserRole")]
        [Authorize(Roles = "Kullanıcıya Rol Ata")]
        public async Task<IActionResult> AddUserRole(CreateUserRoleCommandRequest createUserRoleCommandRequest)
        {
           
            return Ok(await _mediator.Send(createUserRoleCommandRequest));

        }
        [HttpPut("UpdateUserRole")]
        [Authorize(Roles = "Kullanıcı Rolünü Düzenle")]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleCommandRequest updateUserRoleCommandRequest)
        {

           
            return Ok(await _mediator.Send(updateUserRoleCommandRequest));
        }
        [HttpDelete("DeleteUserRole")]
        [Authorize(Roles = "Kullanıcı Rolünü Sil")]
        public async Task<IActionResult> DeleteUserRole([FromQuery]DeleteUserRoleCommandRequest deleteUserRoleCommandRequest)
        {

            
            return Ok(await _mediator.Send(deleteUserRoleCommandRequest));
        }
        [HttpPost("ListUserRole")]
        [Authorize(Roles = "Kullanıcı Rolünü Listele")]
        public async Task<IActionResult> ListUserRole([FromBody] GetUserRoleListQueryRequest getUserRoleListQueryRequest)
        {
           
            return Ok(await _mediator.Send(getUserRoleListQueryRequest));
        }
    }
}

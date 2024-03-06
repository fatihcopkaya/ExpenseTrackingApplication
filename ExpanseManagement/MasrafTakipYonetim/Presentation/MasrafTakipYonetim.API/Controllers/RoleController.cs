
using MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.RoleQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;


        public RoleController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost("CreateRoles")]
        [Authorize(Roles = "Rol Oluştur")]
        public async Task<IActionResult> CreateRole(CreateRoleCommandRequest CreateRoleRequest)
        {
           
            return Ok(await _mediator.Send(CreateRoleRequest));


        }

        [HttpPut("UpdateRoles")]
        [Authorize(Roles = "Rol Düzenle")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            //updateRoleCommandRequest.UpdateRoleDto.Id= Id;
            //UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
            return Ok(await _mediator.Send(updateRoleCommandRequest));
        }

        [HttpDelete("DeleteRoles")]
        [Authorize(Roles = "Rol Sil")]
        public async Task<IActionResult> DeleteRole([FromQuery]DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            //deleteRoleCommandRequest.DeleteRoleDto.Id= Id;
            //DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(await _mediator.Send(deleteRoleCommandRequest));
        }

        [HttpPost("ListRoles")]
        [Authorize(Roles = "Rol Listele")]
        public async Task<IActionResult> GetRoleList(GetRoleListQueryRequest getRoleListQueryRequest)
        {

            return Ok(await _mediator.Send(getRoleListQueryRequest));


        }

        [HttpPost("ListRolesForPagination")]
        [Authorize(Roles = "Rol Listele")]
        public async Task<IActionResult> GetRoleListForPagination(GetRoleListForPaginationQueryRequest getRoleListForPaginationQueryRequest)
        {

            return Ok(await _mediator.Send(getRoleListForPaginationQueryRequest));


        }
    }
}


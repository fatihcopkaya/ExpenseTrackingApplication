
using MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Role Yetki Ata")]
        public async Task<IActionResult> CreateRolePermission(CreateRolePermissionCommandRequest rolePermissionCommandRequest)
        {

           
            return Ok(await _mediator.Send(rolePermissionCommandRequest));
            // Hata durumunu işlemek için try-catch bloğuna ihtiyaç yok.
          

        }


        [HttpPut("UpdateRolePermission")]
        [Authorize(Roles = "Rol Yetkisini Güncelle")]
        public async Task<IActionResult> UpdateRolePermission(UpdateRolePermissionCommandRequest updateRolePermissionCommandRequest)
        {

           
            return Ok(await _mediator.Send(updateRolePermissionCommandRequest));
        }

        [HttpDelete("DeleteRolePermission")]
        [Authorize(Roles = "Rol Yetkisini Sil")]
        public async Task<IActionResult> DeleteRolePermission([FromQuery] DeleteRolePermissionCommandRequest deleteRolePermissionCommandRequest)
        {

           
            return Ok(await _mediator.Send(deleteRolePermissionCommandRequest));
        }

        [HttpPost("ListRolePermission")]
        [Authorize(Roles = "Rol Yetkisini Listele")]
        public async Task<IActionResult> ListRolePermission([FromBody] GetListRolePermissionQueryRequest getListRolePermissionQueryRequest)
        {

            return Ok(await _mediator.Send(getListRolePermissionQueryRequest));
        }


        [HttpPost("ListRolePermissionById")]
        [Authorize(Roles = "Rol Yetkisini Listele")]
        public async Task<IActionResult> ListRolePermissionById([FromBody] GetRolePermissionByIdQueryRequest getListRolePermissionQueryRequest)
        {

            return Ok(await _mediator.Send(getListRolePermissionQueryRequest));
        }

    }
}

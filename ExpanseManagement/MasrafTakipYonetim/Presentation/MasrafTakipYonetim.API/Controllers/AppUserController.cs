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
using Microsoft.AspNetCore.Authorization;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        


        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
           
        }

        //[HttpPost("CreateAppUser")]
        //[Authorize(Roles = "Kullanıcı Oluştur")]
        //public async Task<IActionResult> AddAppUser(CreateAppUserCommandRequest createAppUserCommandRequest)
        //{
        //    var response = await _mediator.Send(createAppUserCommandRequest);
        //    return Ok(response);

        //}
        [HttpPost("AddAppUser")]
        [Authorize(Roles = "Kullaniciyi Oluştur")]
        public async Task<IActionResult> CreateAppUser(CreateAppUserCommandRequest createAppUserCommandRequest)
        {
            //var response = await _mediator.Send(createAppUserCommandRequest);
            return Ok(await _mediator.Send(createAppUserCommandRequest));

        }
        [HttpPut("UpdateAppUser")]
        [Authorize(Roles = "Kullaniciyi Güncelle")]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommandRequest updateAppUserCommandRequest)
        {

            var result = await _mediator.Send(updateAppUserCommandRequest);

            // Eğer bir hata kontrolü yapmanız gerekiyorsa, burada sonucu kontrol edebilirsiniz.

            return Ok(result);

            //return Ok(await _mediator.Send(updateAppUserCommandRequest));


        }



        [HttpDelete("DeleteAppUser")]
        [Authorize(Roles = "Kullaniciyi Sil")]
        public async Task<IActionResult> DeleteAppUser( DeleteAppUserCommandRequest deleteAppUserCommandRequest)   // [FromQuery] DeleteAppUserCommandRequest deleteAppUserCommandRequest
        {
           
            return Ok(await _mediator.Send(deleteAppUserCommandRequest));


        }


        [HttpDelete("DeleteAppUserByUserExpense")]
        
        public async Task<IActionResult> DeleteAppUserByUserExpense([FromQuery] DeleteAppUserByUserExpenseRequest deleteAppUserByUserExpenseRequest)   // [FromQuery] DeleteAppUserCommandRequest deleteAppUserCommandRequest
        {

            return Ok(await _mediator.Send(deleteAppUserByUserExpenseRequest));


        }



        [HttpPost("GetAppUsers")]
        [Authorize(Roles = "Kullanici Listele")]
        public async Task<IActionResult> GetAppUsersByExpenseCategoryTypeAsync([FromBody] AppUserListQueryRequest appUserListQueryRequest)
        {
            return Ok(await _mediator.Send(appUserListQueryRequest));

        }

        //[HttpPost("GetAppUsersByExpenseCategory")]
        //[Authorize(Roles = "Kullanıcı Listele")]
        //public async Task<IActionResult> GetAppUsersByExpenseCategoryTypeAsync([FromBody] AppUserListByExpenseCategoryRequest appUserListByExpenseCategoryRequest)
        //{
        //    return Ok(await _mediator.Send(appUserListByExpenseCategoryRequest));

        //}

        [HttpPost("GetAppUserById")]
        public async Task<IActionResult> GetAppUserById([FromBody] GetAppUserByIdQueryRequest getAppUserByIdQueryRequest)
        {
            return Ok(await _mediator.Send(getAppUserByIdQueryRequest)); 
        }
        [HttpPost("GetNameByAuthenticatedUser")]
        public async Task<IActionResult> GetNameByAuthenticatedUser(GetNameByAuthenticatedUserQueryRequest getNameByAuthenticatedUserQueryRequest)
        {
            return Ok(await _mediator.Send(getNameByAuthenticatedUserQueryRequest));
        }

        [HttpGet("GetAppUserCount")]

        public async Task<IActionResult> GetAppUserCount([FromQuery] GetAppUserCountQueryRequest getAppUserCountQueryRequest)
        {
            return Ok(await _mediator.Send(getAppUserCountQueryRequest));

        }
        
        [HttpPost("GetAppUsersForPayment")]
        public async Task<IActionResult> GetAppUsersForPayment( ListAppUserPaymentRequest listAppUserPaymentRequest)
        {
            return Ok(await _mediator.Send(listAppUserPaymentRequest));

        }

       



    }
}


using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletByExpenseCategoryController:ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletByExpenseCategoryController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("GetExpenseByIdAsync")]
        public async Task<IActionResult> GetExpenseByIdAsync([FromBody]GetWalletByCategoryByExpenseCategoryIdRequest getWalletByCategoryByExpenseCategoryIdRequest )
        {

            GetWalletByCategoryByExpenseCategoryIdResponse response = await _mediator.Send(getWalletByCategoryByExpenseCategoryIdRequest);
            return Ok(response);
            //return Ok(await _mediator.Send(getWalletByCategoryByExpenseCategoryIdRequest));

        }


        [HttpGet("GetAllWalletByCategoryByExpenseCategoryId")]
        public async Task<IActionResult> GetAllWalletByCategoryByExpenseCategoryId()
        {
            GetAllWalletByCategoryByExpenseCategoryIdResponse response = await _mediator.Send(new GetAllWalletByCategoryByExpenseCategoryIdRequest());
            return Ok(response);
        }

    }
}

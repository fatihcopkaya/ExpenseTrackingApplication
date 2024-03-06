using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController:ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("payments-and-expenses")]
        public async Task<IActionResult> GetPaymentsAndExpensesFromWallet([FromQuery]GetPaymentsAndExpenseFromWalletRequest getPaymentsAndExpenseFromWalletRequest) 
        {
            return Ok(await _mediator.Send(getPaymentsAndExpenseFromWalletRequest));
        }
    }
}

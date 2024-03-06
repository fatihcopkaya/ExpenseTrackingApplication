using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Commands.PaymentRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseCommands;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateExpense")]
        [Authorize(Roles = "Harcama Oluştur")]
        public async Task<IActionResult> CreateExpense(CreateExpenseCommandRequest request)
        {
            
            return Ok(await _mediator.Send(request));

        }
        [HttpPost("UpdateExpense")]
        [Authorize(Roles = "Harcama Güncelle")]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseCommandRequest request)
        {
            var result = await _mediator.Send(request);
            
            return Ok(result);

            //return Ok(await _mediator.Send(request));

        }
        [HttpDelete("DeleteExpense")]
        [Authorize(Roles = "Harcama Sil")]
        public async Task<IActionResult> DeleteExpense([FromQuery] DeleteExpenseCommandRequest request)
        {
           
            return Ok(await _mediator.Send(request));
        }
        [HttpPost("GetExpenseList")]
        [Authorize(Roles = "Harcama Listele")]
        public async Task<IActionResult> GetExpenseList([FromBody]GetExpenseListQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }



        [HttpPost("GetExpenseForReportList")]
        [Authorize(Roles = "Harcama Listele")]
        public async Task<IActionResult> GetExpenseForReportList([FromBody] GetExpenseListForReportRequest request)
        {
            return Ok(await _mediator.Send(request));
        }



    }
}

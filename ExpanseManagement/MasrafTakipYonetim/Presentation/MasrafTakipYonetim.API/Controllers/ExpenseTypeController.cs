using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{
    //[Authorize(Roles = "Fatih ")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IMediator _mediator;



        public  ExpenseTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpPost("AddExpenseType")]
        [Authorize(Roles = "Harcama Tipi Oluştur")]
        public async Task<IActionResult> AddExpenseType([FromBody]CreateExpenseTypeCommandRequest createExpenseTypeCommandRequest)
        {
            //CreateExpenseTypeCommandResponse response = await _mediator.Send(createExpenseTypeCommandRequest);
            return Ok(await _mediator.Send(createExpenseTypeCommandRequest));

        }
        [HttpPost("UpdateExpenseType")]
        //[Authorize(Roles = "Süper Admin,Admin")]
        public async Task<IActionResult> UpdateExpenseType(UpdateExpenseTypeCommandRequest updateExpenseTypeCommandRequest)
        {
            
            UpdateExpenseTypeCommandResponse response = await _mediator.Send(updateExpenseTypeCommandRequest);
            return Ok(response);
        }
        [HttpPost("DeleteExpenseType")]
        //[Authorize(Roles = "Süper Admin,Admin")]
        public async Task<IActionResult> DeleteExpenseType( DeleteExpenseTypeCommandRequest deleteExpenseTypeCommandRequest)
        {
            
            DeleteExpenseTypeCommandResponse response = await _mediator.Send(deleteExpenseTypeCommandRequest);
            return Ok(response);
        }
        [HttpPost("ListExpenseType")]
        //[Authorize(Roles = "Süper Admin,Admin,Kullanıcı")]
        public async Task<IActionResult> ListExpenseType([FromQuery] ExpenseTypeListQueryRequest expenseTypeListQueryRequest)
        {
            ExpenseTypeListQueryResponse response = await _mediator.Send(expenseTypeListQueryRequest);
            return Ok(response);
        }

    }
    
}

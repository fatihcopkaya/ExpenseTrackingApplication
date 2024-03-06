using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryCommands;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipYonetim.API.Controllers
{   
    [Route("api/controller")]
    [ApiController]
    public class ExpenseCategoryController:Controller
    {
        private readonly IMediator _mediator;

        public ExpenseCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Harcama Kategorisi Ekle")]
        public async Task<IActionResult> CreateExpenseCategory(CreateExpenseCategoryCommandRequest createExpenseCategoryCommandRequest)
        {
            CreateExpenseCategoryCommandResponse createExpenseCategoryCommandResponse= await _mediator.Send(createExpenseCategoryCommandRequest);

            return Ok(createExpenseCategoryCommandResponse);
        }

        [HttpPut("UpdateExpenseCategory")]
        [Authorize(Roles = "HarcamaKategorisi Düzenle")]

        public async Task<IActionResult>UpdateExpenseCategory(UpdateExpenseCategoryCommandRequest updateExpenseCategoryCommandRequest) 
        { 
           
            UpdateExpenseCategoryResponse updateExpenseCategoryResponse = await _mediator.Send(updateExpenseCategoryCommandRequest);

            return Ok(updateExpenseCategoryResponse);
            

        }

        [HttpPost("DeleteExpenseCategory")]
        [Authorize(Roles = "Harcama Kategorisi Sil")]
        public async Task<IActionResult> DeleteExpenseCategory(DeleteExpenseCategoryCommandRequest deleteExpenseCategoryRequest) 
        {
           
            DeleteExpenseCategoryResponse deleteExpenseCategoryResponse=await _mediator.Send(deleteExpenseCategoryRequest);
            return Ok(deleteExpenseCategoryResponse);
        }

        [HttpGet("ListExpenseCategory")]
        [Authorize(Roles = "Harcama Kategorisi Gör")]

        public async Task<IActionResult> ListExpenseCategory([FromQuery] GetExpenseCategoryListQueryRequest getExpenseCategoryListQueryRequest)
        {
            return Ok(await _mediator.Send(getExpenseCategoryListQueryRequest));
        }
    }
}

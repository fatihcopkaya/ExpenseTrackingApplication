using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses
{
    public class CreateExpenseTypeCommandRequest : IRequest<Results>
    {
        //public CreateExpenseTypeRequestDto createExpenseTypeRequestDto { get; set; }
        public string Name { get; set; }
    }
}

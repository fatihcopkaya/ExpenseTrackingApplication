using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses
{
    public class UpdateExpenseTypeCommandRequest : IRequest<UpdateExpenseTypeCommandResponse>
    {
        public UpdateExpenseTypeRequestDto updateExpenseTypeRequestDto { get; set; }
    }
}

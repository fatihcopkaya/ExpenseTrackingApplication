using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses
{
    public class DeleteExpenseTypeCommandRequest : IRequest<DeleteExpenseTypeCommandResponse>
    {
        public DeleteExpenseTypeRequestDto deleteExpenseTypeRequestDto { get; set; }
    }
}

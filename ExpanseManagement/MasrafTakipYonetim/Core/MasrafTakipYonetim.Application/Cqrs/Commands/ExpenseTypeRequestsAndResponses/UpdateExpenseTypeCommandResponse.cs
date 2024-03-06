using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses
{
    public class UpdateExpenseTypeCommandResponse : IRequest<UpdateExpenseTypeCommandRequest>
    {
        public string Message { get; set; }
    }
}

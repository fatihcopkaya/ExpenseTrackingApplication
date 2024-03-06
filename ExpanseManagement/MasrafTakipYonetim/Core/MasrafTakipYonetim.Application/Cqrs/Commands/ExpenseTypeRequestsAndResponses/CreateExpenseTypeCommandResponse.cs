using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses
{
    public class CreateExpenseTypeCommandResponse : IRequest<CreateExpenseTypeCommandRequest>
    {
        public string Message { get; set; }
    }
}

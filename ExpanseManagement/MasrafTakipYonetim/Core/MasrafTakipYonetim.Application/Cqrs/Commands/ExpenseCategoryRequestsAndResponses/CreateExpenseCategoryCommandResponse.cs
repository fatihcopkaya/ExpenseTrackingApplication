using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses
{
    public class CreateExpenseCategoryCommandResponse : IRequest<CreateExpenseCategoryCommandRequest>
    {
        public string Message { get; set; }
        // public int ExpenseCategoryId { get; set; }
    }
}

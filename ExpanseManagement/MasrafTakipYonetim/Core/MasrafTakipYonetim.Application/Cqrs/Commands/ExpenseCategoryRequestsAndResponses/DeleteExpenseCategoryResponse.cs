using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses
{
    public class DeleteExpenseCategoryResponse : IRequest<DeleteExpenseCategoryCommandRequest>
    {
        public string Message { get; set; }
    }
}

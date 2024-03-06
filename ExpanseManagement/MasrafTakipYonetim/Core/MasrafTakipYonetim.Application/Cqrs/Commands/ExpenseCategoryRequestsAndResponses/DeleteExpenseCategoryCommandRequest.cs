using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses
{
    public class DeleteExpenseCategoryCommandRequest : IRequest<DeleteExpenseCategoryResponse>
    {
        public DeleteExpenseCategoryRequestDto deleteExpenseCategoryRequestDto { get; set; }
    }
}

using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses
{
    public class CreateExpenseCategoryCommandRequest : IRequest<CreateExpenseCategoryCommandResponse>
    {
        public CreateExpenseCategoryRequestDto ExpenseCategoryRequestDto { get; set; }

    }
}

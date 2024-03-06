using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses
{
    public class UpdateExpenseCategoryCommandRequest : IRequest<UpdateExpenseCategoryResponse>
    {
        public UpdateExpenseCategoryRequestDto updateExpenseCategoryRequestDto { get; set; }
    }
}

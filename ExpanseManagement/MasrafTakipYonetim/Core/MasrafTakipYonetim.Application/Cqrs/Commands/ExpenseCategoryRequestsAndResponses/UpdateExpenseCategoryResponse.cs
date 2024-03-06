using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses
{
    public class UpdateExpenseCategoryResponse : IRequest<UpdateExpenseCategoryCommandRequest>
    {
        public string Message { get; set; }

    }
}

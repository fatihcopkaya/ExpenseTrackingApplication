using AutoMapper;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;

using MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryQueries;

using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryCommands
{
    public class UpdateExpenseCategoryCommandHandler : IRequestHandler<UpdateExpenseCategoryCommandRequest, UpdateExpenseCategoryResponse>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper, IMediator mediator)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UpdateExpenseCategoryResponse> Handle(UpdateExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            GetExpenseCategoryByIdQueryRequest getExpenseCategoryByIdQueryRequest = new GetExpenseCategoryByIdQueryRequest { Id = request.updateExpenseCategoryRequestDto.Id };
            GetExpenseCategoryByIdQueryResponse getExpenseCategoryByIdQueryResponse = await _mediator.Send(getExpenseCategoryByIdQueryRequest);

            var _mappedExpenseCategory = _mapper.Map<UpdateExpenseCategoryRequestDto, ExpenseCategory>(request.updateExpenseCategoryRequestDto, getExpenseCategoryByIdQueryResponse.ExpenseCategory);
            var updatedExpenseCategory = await _expenseCategoryRepository.UpdateExpenseCategoryAsync(_mappedExpenseCategory);
            if (!updatedExpenseCategory.Success)
            {
                throw new MasrafTakipCustomException($"{request.updateExpenseCategoryRequestDto.Id} Has ID {nameof(ExpenseCategory)} Failed To Update!", 500);
            }

            return new UpdateExpenseCategoryResponse { Message = updatedExpenseCategory.Message };
        }
    }
}
        
    

        

    


using AutoMapper;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeQueries;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeCommands
{
    public class UpdateExpenseTypeCommandHandler : IRequestHandler<UpdateExpenseTypeCommandRequest, UpdateExpenseTypeCommandResponse>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateExpenseTypeCommandHandler(IExpenseTypeRepository expenseTypeRepository, IMapper mapper, IMediator mediator)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<UpdateExpenseTypeCommandResponse> Handle(UpdateExpenseTypeCommandRequest request, CancellationToken cancellationToken)
        {
            GetExpenseTypeByIdQueryRequest getExpenseTypeByIdQueryRequest = new GetExpenseTypeByIdQueryRequest() { Id = request.updateExpenseTypeRequestDto.Id };
            GetExpenseTypeByIdQueryResponse getExpenseTypeByIdQueryResponse = await _mediator.Send(getExpenseTypeByIdQueryRequest);

            var _mappedExpenseType = _mapper.Map<UpdateExpenseTypeRequestDto, ExpenseType>(request.updateExpenseTypeRequestDto, getExpenseTypeByIdQueryResponse.ExpenseType);

            var updatedExpenseType = await _expenseTypeRepository.UpdateExpenseType(_mappedExpenseType);
            if (!updatedExpenseType.Success)
            {
                throw new MasrafTakipCustomException($"{request.updateExpenseTypeRequestDto.Id} Has ID {nameof(ExpenseType)} Failed To Update!", 500);
            }
            return new UpdateExpenseTypeCommandResponse { Message = updatedExpenseType.Message };

        }
    }
}

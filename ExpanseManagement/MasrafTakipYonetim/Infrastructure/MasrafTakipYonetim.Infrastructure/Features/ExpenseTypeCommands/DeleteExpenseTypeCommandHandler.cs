using AutoMapper;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses;

using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.ExpenseTypeQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeQueries;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeCommands
{
    public class DeleteExpenseTypeCommandHandler : IRequestHandler<DeleteExpenseTypeCommandRequest, DeleteExpenseTypeCommandResponse>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteExpenseTypeCommandHandler(IExpenseTypeRepository expenseTypeRepository, IMapper mapper, IMediator mediator)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<DeleteExpenseTypeCommandResponse> Handle(DeleteExpenseTypeCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GetExpenseTypeByIdQueryRequest getExpenseTypeByIdQueryRequest = new GetExpenseTypeByIdQueryRequest() { Id = request.deleteExpenseTypeRequestDto.Id };
                GetExpenseTypeByIdQueryResponse getExpenseTypeByIdQueryResponse = await _mediator.Send(getExpenseTypeByIdQueryRequest);
                var _mappedExpenseType = _mapper.Map<DeleteExpenseTypeRequestDto, ExpenseType>(request.deleteExpenseTypeRequestDto, getExpenseTypeByIdQueryResponse.ExpenseType);
                _mappedExpenseType.IsDeleted = true;
                var deletedExpenseType = await _expenseTypeRepository.DeleteExpenseType(_mappedExpenseType);
                if (!deletedExpenseType.Success)
                {
                    throw new MasrafTakipCustomException($"{request.deleteExpenseTypeRequestDto.Id}' Has ID {nameof(ExpenseType)} Could Not Be Deleted! ", 500);
                }
                return new DeleteExpenseTypeCommandResponse { Message = deletedExpenseType.Message };

            }
            catch (Exception ex)
            {
                return new DeleteExpenseTypeCommandResponse { Message = ex.Message };
            }

        }
    }
}

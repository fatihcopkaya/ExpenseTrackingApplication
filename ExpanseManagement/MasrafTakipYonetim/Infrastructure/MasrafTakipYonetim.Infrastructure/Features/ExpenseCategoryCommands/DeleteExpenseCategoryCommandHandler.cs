using AutoMapper;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseCategoryQuery;
using MasrafTakipYonetim.Application.CustomExceptions;

using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryCommands
{
    public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommandRequest, DeleteExpenseCategoryResponse>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper, IMediator mediator)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DeleteExpenseCategoryResponse> Handle(DeleteExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GetExpenseCategoryByIdQueryRequest getExpenseCategoryByIdQueryRequest = new GetExpenseCategoryByIdQueryRequest() { Id = request.deleteExpenseCategoryRequestDto.Id };
                GetExpenseCategoryByIdQueryResponse getExpenseCategoryByIdQueryResponse = await _mediator.Send(getExpenseCategoryByIdQueryRequest, cancellationToken);
                var _mappedCategory = _mapper.Map<DeleteExpenseCategoryRequestDto, ExpenseCategory>(request.deleteExpenseCategoryRequestDto, getExpenseCategoryByIdQueryResponse.ExpenseCategory);
                _mappedCategory.IsDeleted = true;
                var deletedExpenseCategory = await _expenseCategoryRepository.DeleteExpenseCategoryAsync(_mappedCategory);
                if (!deletedExpenseCategory.Success)
                {
                    throw new MasrafTakipCustomException($"Unable To Delete {nameof(ExpenseCategory)} With ID {request.deleteExpenseCategoryRequestDto.Id}", 500);

                }
                return new DeleteExpenseCategoryResponse { Message = deletedExpenseCategory.Message };
            }
            catch (Exception ex)
            {
                return new DeleteExpenseCategoryResponse { Message = ex.Message };
            }
        }
        }
    }


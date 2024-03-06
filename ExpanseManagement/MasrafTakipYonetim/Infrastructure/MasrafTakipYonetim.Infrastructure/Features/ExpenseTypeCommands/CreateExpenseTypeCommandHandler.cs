using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseTypeRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.ExpenseType;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeCommands
{
    public class CreateExpenseTypeCommandHandler : IRequestHandler<CreateExpenseTypeCommandRequest, Results>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateExpenseTypeCommandHandler> _logger;

        public CreateExpenseTypeCommandHandler(IExpenseTypeRepository expenseTypeRepository, IMapper mapper, ILogger<CreateExpenseTypeCommandHandler> logger)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Results> Handle(CreateExpenseTypeCommandRequest request, CancellationToken cancellationToken)
        {
            //var _mappedExpenseType = _mapper.Map<CreateExpenseTypeRequestDto, ExpenseType>(request.createExpenseTypeRequestDto);
            var newexpenseType = new ExpenseType
            {
                Name=request.Name,
            };
            var expensedType = await _expenseTypeRepository.CreateExpenseType(newexpenseType);
            _logger.LogInformation("Oluşturuldu");
            if (!expensedType.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(ExpenseType)} Could Not Be Created!", 500);
            }
            return Results.Success();


           
        }
    }
}

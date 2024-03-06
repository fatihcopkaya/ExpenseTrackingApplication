using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Expense;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.Features.ExpenseTypeQueries;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCommands
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommandRequest, Results>
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;
        private readonly IMediator _mediator;
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public CreateExpenseCommandHandler(IMapper mapper, IExpenseRepository expenseRepository, IWalletRepository walletRepository, IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository, IMediator mediator, IExpenseTypeRepository expenseTypeRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _walletRepository = walletRepository;
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
            _mediator = mediator;
        }

        public async Task<Results> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            //var _mappedExpense = _mapper.Map<CreateExpenseRequestDto, Expense>(request.createExpenseDto);
            

            
            var newExpense = new Expense
            {
                ExpenseCategoryId = request.ExpenseCategoryId,
                Amount = request.Amount,
                Description = request.Description,
                ExpenseTypeId = request.ExpenseTypeId,
                PaymentDate= request.PaymentDate,


            };
            var createdExpense = await _expenseRepository.CreateExpenseAsync(newExpense);
            if(!createdExpense.Success) 
            {
                throw new MasrafTakipCustomException($"{nameof(Expense)} Could Not Be Created", 500);
            }
            // Expense oluşturulduktan sonra oluşturulan expensin categorysine göre amount değeri wallete + olarak eklenir
            var walletByExpenseByExpenseCategoryId = new GetWalletByCategoryByExpenseCategoryIdRequest() 
            { ExpenseCategoryId = createdExpense.Data.ExpenseCategoryId };
            var walletByExpenseCategory = await _mediator.Send(walletByExpenseByExpenseCategoryId);
            walletByExpenseCategory.WalletByExpenseCategory.TotalExpenseByExpenseCategory += createdExpense.Data.Amount;
            var updatedWalletByCategory = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(walletByExpenseCategory.WalletByExpenseCategory);
            if(!updatedWalletByCategory.Success) 
            {
                throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Updated", 500);
            }
            //categorye göre olan wallet değeri eklendikten sonra genel wallete de bu değer eklenir
            var walletRequest = new GetWalletRequest() { };
            var wallet = await  _mediator.Send(walletRequest);
            wallet.Wallet.TotalExpenses += createdExpense.Data.Amount;
            var updatedWallet = await _walletRepository.UpdateWalletAsync(wallet.Wallet);
            if (!updatedWallet.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(Wallet)} Could Not Be Updated", 500);
            }


            return Results.Success();
            





        }

        private Task GetExpenseTypeNameByIdAsync(object expenseTypeId)
        {
            throw new NotImplementedException();
        }
    }
}

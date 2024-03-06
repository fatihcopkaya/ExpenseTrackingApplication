using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.ExpenseQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletByExpenseCategoryQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.WalletQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Expense;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCommands
{
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommandRequest, Results>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;
        private readonly IWalletRepository _walletRepository;

        public DeleteExpenseCommandHandler(IMediator mediator, IMapper mapper, IExpenseRepository expenseRepository, IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository, IWalletRepository walletRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
            _walletRepository = walletRepository;
        }

        public async Task<Results> Handle(DeleteExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            //var expenseQuerey = new GetExpenseByIdQueryRequest() { ExpenseId = request.Id };
            var expense = request.Id;
            var expenseByIdResponse = await _expenseRepository.GetExpenseByIdAsync(expense);
            //var expenseByIdResponse = await _mediator.Send(expenseQuerey);
           

                    //var _mappedExpense = _mapper.Map<DeleteExpenseRequestDto, Expense>(request.deleteExpenseDto, expenseByIdResponse.Expense);
                    //_mappedExpense.IsDeleted = true;
                   
                    if (expenseByIdResponse == null)
                    {
                        throw new MasrafTakipCustomException($"Unable To Delete {nameof(Expense)} With ID {request.Id}", 500);

                    }
                    expenseByIdResponse.Data.Id = request.Id;
                    expenseByIdResponse.Data.IsDeleted= true;
                    var deletedExpense = await _expenseRepository.DeleteExpenseAsync(expenseByIdResponse.Data);
            //silinen expense in amount değeri expenseCategorysinin ilişkili olduğu walletten eksiltilir
                   var walletByExpenseCategoryId = new GetWalletByCategoryByExpenseCategoryIdRequest()
                  { ExpenseCategoryId = deletedExpense.ExpenseCategoryId };
                  var walletByExpenseCategory = await _mediator.Send(walletByExpenseCategoryId);
                  walletByExpenseCategory.WalletByExpenseCategory.TotalExpenseByExpenseCategory -= deletedExpense.Amount;  
                  var updatedWalletByCategory = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(walletByExpenseCategory.WalletByExpenseCategory);
                  if (!updatedWalletByCategory.Success)
                  {
                    throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Deleted", 500);
                  }
                //silinen expense in amount değeri walletten eksiltilir
                var walletRequest = new GetWalletRequest() { };
                  var wallet = await _mediator.Send(walletRequest);
                  
                  wallet.Wallet.TotalExpenses -= deletedExpense.Amount;
                  var deletedWallet = await _walletRepository.UpdateWalletAsync(wallet.Wallet);
                if (!deletedWallet.Success)
                {
                throw new MasrafTakipCustomException($"{nameof(Wallet)} Could Not Be Updated", 500);
                }


                return Results.Success();
            

                
                

                

            
            
        }
    }
}

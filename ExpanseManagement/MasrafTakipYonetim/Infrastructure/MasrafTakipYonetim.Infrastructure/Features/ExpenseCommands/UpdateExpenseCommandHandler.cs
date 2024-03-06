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
    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommandRequest, Results>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;

        public UpdateExpenseCommandHandler(IMapper mapper, IMediator mediator, IExpenseRepository expenseRepository, IWalletRepository walletRepository, IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _expenseRepository = expenseRepository;
            _walletRepository = walletRepository;
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
        }

        public async Task<Results> Handle(UpdateExpenseCommandRequest request, CancellationToken cancellationToken)
        {
            var expenseQuery = new GetExpenseByIdQueryRequest() { ExpenseId = request.Id };
            var expenseByIdResponse = await _mediator.Send(expenseQuery);
            var expenseAmountWithoutUpdate = expenseByIdResponse.Expense.Amount;
            var expenseCategoryIdWithoutUpdate = expenseByIdResponse.Expense.ExpenseCategoryId;



            // var _mappedExpense = _mapper.Map<UpdateExpenseRequestDto, Expense>(request, expenseByIdResponse.Expense);
                    expenseByIdResponse.Expense.ExpenseCategoryId = request.ExpenseCategoryId;
                    expenseByIdResponse.Expense.Amount = request.Amount;
                    expenseByIdResponse.Expense.Description = request.Description;
                    expenseByIdResponse.Expense.ExpenseTypeId = request.ExpenseTypeId;
                    expenseByIdResponse.Expense.PaymentDate = request.PaymentDate;


            var updatedExpense = await _expenseRepository.UpdateExpenseAsync(expenseByIdResponse.Expense);
                    if (!updatedExpense.Success)
                    {
                        throw new MasrafTakipCustomException($"{request.Id} Has ID {nameof(Expense)} Failed to Update! ", 500);
                    }
                    //hem amount hemde ExpensCategory güncellenirse güncellemeden önceki amount ait olduğu ExpenseCategorye gore walletten silinir
                    //güncellenen expenseCategory e ait wallette güncellenen amount eklenir
                if (expenseCategoryIdWithoutUpdate != updatedExpense.Data.ExpenseCategoryId)
                {
                    var walletByExpenseCategoryIdBeforeUpdate = new GetWalletByCategoryByExpenseCategoryIdRequest()
                    { ExpenseCategoryId = expenseCategoryIdWithoutUpdate };
                    var walletByExpenseCategoryBeforeUpdate = await _mediator.Send(walletByExpenseCategoryIdBeforeUpdate);
                    walletByExpenseCategoryBeforeUpdate.WalletByExpenseCategory.TotalExpenseByExpenseCategory -= expenseAmountWithoutUpdate;
                    var updatedExpenseByBeforeId = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(walletByExpenseCategoryBeforeUpdate.WalletByExpenseCategory);

                   
                    var response = await _mediator.Send(new GetWalletByCategoryByExpenseCategoryIdRequest() 
                    {ExpenseCategoryId = updatedExpense.Data.ExpenseCategoryId });
                    response.WalletByExpenseCategory.TotalExpenseByExpenseCategory += updatedExpense.Data.Amount;
                    var updatedExpenseById = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(response.WalletByExpenseCategory);

                    if(!updatedExpenseByBeforeId.Success && !updatedExpenseById.Success)
                    {
                        throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Updated", 500);
                    }
                        

                }
                else 
                {
                    var walletByExpenseCategoryId = new GetWalletByCategoryByExpenseCategoryIdRequest()
                    { ExpenseCategoryId = updatedExpense.Data.ExpenseCategoryId };
                    var walletByExpenseCategory = await _mediator.Send(walletByExpenseCategoryId);

                    //güncellemeden önce ve sonrasındaki amount değerlerinin farkı alınıp güncellenir.
                    var updatedAmount = updatedExpense.Data.Amount;
                    updatedAmount -= expenseAmountWithoutUpdate;
                    walletByExpenseCategory.WalletByExpenseCategory.TotalExpenseByExpenseCategory += updatedAmount;
                    var updatedWalletByCategory = await _walletByExpenseCategoryRepository.UpdateExpenseAsync(walletByExpenseCategory.WalletByExpenseCategory);
                    if (!updatedWalletByCategory.Success)
                    {
                        throw new MasrafTakipCustomException($"{nameof(WalletByExpenseCategory)} Could Not Be Updated", 500);
                    }

                }
                              
               
                //categorye göre olan wallet değeri eklendikten sonra genel wallete de bu değer eklenir
                var walletRequest = new GetWalletRequest() { };
                var wallet = await _mediator.Send(walletRequest);
                // güncellendikten sonraki değerden güncellenmeden önceki değeri çıkartıyoruz ondan sonra walleta ekliyoruz
                var updatedAmountForWallet = updatedExpense.Data.Amount;
                updatedAmountForWallet -= expenseAmountWithoutUpdate;
                wallet.Wallet.TotalExpenses += updatedAmountForWallet;
                var updatedWallet = await _walletRepository.UpdateWalletAsync(wallet.Wallet);

            if (!updatedWallet.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(Wallet)} Could Not Be Updated", 500);
            }
                

            return Results.Success();
        }
               
                


            
    }
}


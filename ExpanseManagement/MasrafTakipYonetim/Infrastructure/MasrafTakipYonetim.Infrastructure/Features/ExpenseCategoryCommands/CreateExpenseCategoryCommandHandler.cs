using AutoMapper;
using MasrafTakipYonetim.Application.Cqrs.Commands.ExpenseCategoryRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.ExpenseCategoryCommands
{
    public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommandRequest, CreateExpenseCategoryCommandResponse>
    {         
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IWalletByExpenseCategoryRepository _walletByExpenseCategoryRepository;

        public CreateExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper, IWalletByExpenseCategoryRepository walletByExpenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
            _walletByExpenseCategoryRepository = walletByExpenseCategoryRepository;
        }

        public async Task<CreateExpenseCategoryCommandResponse> Handle(CreateExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            // CreateExpenseCategoryCommandRequest'ten ExpenseCategoryCreateDto'yu çıkarır
            // DTO'yu ExpenseCategory sınıfına eşleme (mapping)
            var _mappedExpenseCategory =  _mapper.Map<CreateExpenseCategoryRequestDto, ExpenseCategory>(request.ExpenseCategoryRequestDto);
            var createdExpenseCategory = await _expenseCategoryRepository.CreateExpenseCategory(_mappedExpenseCategory);

            // Veritabanında yeni harcama kategorisi oluşturma işlemi
            

            if (!createdExpenseCategory.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(ExpenseCategory)} Could Not be Created!", 500);
               
            }
            //Expense Category oluşturulduktan sonra walletByCategory tablosuna yeni bir row oluşturulur ve expensecategoryId wallete verilir
            var createdWalletByCategory =  await  _walletByExpenseCategoryRepository.CreateExpenseAsync(new WalletByExpenseCategory
            {
               CreatedDate = DateTime.UtcNow,
               ExpenseCategoryId = createdExpenseCategory.Data.Id,
               TotalExpenseByExpenseCategory = 0,
               TotalPaymentByExpenseCategory = 0,
               IsDeleted = false,

            });



            return new CreateExpenseCategoryCommandResponse { Message = createdExpenseCategory.Message };
        }
    }
    }


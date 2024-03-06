using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserCommands
{
    public class DeleteAppUserByUserExpenseHandler : IRequestHandler<DeleteAppUserByUserExpenseRequest, Results>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserExpenseCategoryRepository _userExpenseCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteAppUserByUserExpenseHandler(IAppUserRepository appUserRepository, IUserExpenseCategoryRepository userExpenseCategoryRepository, IMapper mapper, IMediator mediator)
        {
            _appUserRepository = appUserRepository;
            _userExpenseCategoryRepository = userExpenseCategoryRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Results> Handle(DeleteAppUserByUserExpenseRequest request, CancellationToken cancellationToken)

        {

            var userExpensecategoryId = request.Id;
             var  userExpenseCategory = await _userExpenseCategoryRepository.GetUserExpenseCategoryById(userExpensecategoryId);
            if (userExpenseCategory == null)
            {
                throw new MasrafTakipCustomException($"{request.Id} numaralı Id'ye sahip{nameof(UserExpenseCategory)} bulunamadı", 404);

            }

            userExpenseCategory.IsDeleted=true;
             await _userExpenseCategoryRepository.DeleteUserExpenseCategory(userExpenseCategory);

    
          

            return Results.Success();
        }
    }
}

// AppUser appUser = await _appUserRepository.GetAppUserById(request.AppUserId);

//bool hasDeletedUserExpenseCategories = await _userExpenseCategoryRepository
//.HasDeletedUserExpenseCategoriesForAppUser(request.AppUserId);

// if (hasDeletedUserExpenseCategories)
// {
//     // Eğer silinen UserExpenseCategory kayıtları varsa, AppUser'ı sil
//     appUser.IsDeleted = true;
//     var deletedAppuser = await _appUserRepository.DeleteAppUser(appUser);
// }
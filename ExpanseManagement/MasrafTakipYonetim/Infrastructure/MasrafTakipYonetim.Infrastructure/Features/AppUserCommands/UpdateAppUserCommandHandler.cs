using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserCommands
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommandRequest, Results>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserExpenseCategoryRepository _userExpenseCategoryRepository;
        private readonly ILogger<UpdateAppUserCommandHandler> _logger;


        public UpdateAppUserCommandHandler(IAppUserRepository appUserRepository, IMapper mapper, IMediator mediator, IUserExpenseCategoryRepository userExpenseCategoryRepository, ILogger<UpdateAppUserCommandHandler> logger)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _mediator = mediator;
            _userExpenseCategoryRepository = userExpenseCategoryRepository; 
            _logger = logger;
        }



        public async Task<Results> Handle(UpdateAppUserCommandRequest request, CancellationToken cancellationToken)
        {



            var appuserId = request.Id;
           
            
            //UserExpenseCategory updatedUser = await _userExpenseCategoryRepository.GetUserExpenseCategoryById(appuserId);
           // updatedUser.AppUser = new AppUser();
            var updatedUser = await _appUserRepository.GetAppUserById(appuserId);

            if (updatedUser == null)
            {
                _logger.LogError($"{appuserId} 'ye sahip{nameof(AppUser)} bulunamadı");
                throw new MasrafTakipCustomException($"{appuserId} 'ye sahip{nameof(AppUser)} bulunamadı", 404);
            }
        

                updatedUser.Id= request.Id;
                updatedUser.RoleId= request.RoleId;
                updatedUser.FirstName = request.FirstName;
                updatedUser.LastName = request.LastName;
                updatedUser.Email = request.Email;
                updatedUser.PhoneNumber = request.PhoneNumber;
                
                
               

            var result = await _appUserRepository.UpdateAppUser(updatedUser);

            _logger.LogInformation("AppUser güncellendi.");
            return Results.Success();
                    


           

        }
    }
}

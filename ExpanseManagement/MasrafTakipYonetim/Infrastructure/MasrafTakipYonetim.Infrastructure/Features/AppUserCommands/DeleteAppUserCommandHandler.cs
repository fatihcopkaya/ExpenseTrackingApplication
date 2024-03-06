using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.AppUserCommands
{
    public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommandRequest,Results>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteAppUserCommandHandler(IAppUserRepository appUserRepository, IMapper mapper, IMediator mediator)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        public async Task<Results>Handle(DeleteAppUserCommandRequest request, CancellationToken cancellationToken)
        {
           


                            
                AppUser appUserById =  await _appUserRepository.GetAppUserById(request.Id);
                if (appUserById == null)
                {
                    throw new MasrafTakipCustomException($"{request.Id} numaralı Id'ye sahip{nameof(AppUser)} bulunamadı", 404);
                }



                appUserById.IsDeleted = true;
                var deletedAppUser = await _appUserRepository.DeleteAppUser(appUserById);

                return Results.Success();

            
            
        }
    }
}

// var _mappedUser = _mapper.Map<AppUser>(request.Id); // Eğer DeleteAppUserDto özelliğini kullanıyorsanız



//if (!deletedAppUser.Success)
//{
//    throw new MasrafTakipCustomException($"{request.Id}' ye ait {nameof(AppUser)} silinemedi ",500);
//}
//return new DeleteAppUserCommandResponse { Message = deletedAppUser.Message };
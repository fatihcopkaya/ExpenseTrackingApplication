using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Commands.ProfileInfoRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.ProfileInfoCommands
{
    public class UpdateProfileInfoCommandHandler : IRequestHandler<UpdateProfileInfoCommandRequest, Results>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IAppUserService _appUserService;
        public UpdateProfileInfoCommandHandler(IAppUserRepository appUserRepository, IMapper mapper, IMediator mediator, IAppUserService appUserService)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _mediator = mediator;
            _appUserService = appUserService;
        }

        public async Task<Results> Handle(UpdateProfileInfoCommandRequest request, CancellationToken cancellationToken)
        {
            
            var authenticatedUser= await _appUserService.GetAuthenticatedUser();
            //GetProfileInfoByIdQueryResponse getProfileInfoByIdQueryResponse = new GetProfileInfoByIdQueryResponse();
            //getProfileInfoByIdQueryResponse.AppUser.FirstName = request.FirstName;
            //getProfileInfoByIdQueryResponse.AppUser.LastName = request.LastName;
            //getProfileInfoByIdQueryResponse.AppUser.Email = request.Email;
            //getProfileInfoByIdQueryResponse.AppUser.PhoneNumber = request.PhoneNumber;

            if (authenticatedUser.Data == null)
            {
                throw new MasrafTakipCustomException($"{authenticatedUser.Data.Id} 'ye sahip{nameof(AppUser)} bulunamadı", 404);
            }
            
            authenticatedUser.Data.Email = request.Email;
            authenticatedUser.Data.FirstName = request.FirstName;
            authenticatedUser.Data.LastName = request.LastName;
            authenticatedUser.Data.PhoneNumber = request.PhoneNumber;

            var result = await _appUserRepository.UpdateAppUser(authenticatedUser.Data);//database e yazmıyor.
            return Results.Success();
        }
    }
}

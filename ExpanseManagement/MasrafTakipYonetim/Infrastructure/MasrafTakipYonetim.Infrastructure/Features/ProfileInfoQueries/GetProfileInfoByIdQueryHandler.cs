using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.ProfileInfoQueries
{
    public class GetProfileInfoByIdQueryHandler : IRequestHandler<GetProfileInfoByIdQueryRequest, GetProfileInfoByIdQueryResponse>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserService _appUserService;

        public GetProfileInfoByIdQueryHandler(IAppUserRepository appUserRepository, IAppUserService appUserService)
        {
            _appUserRepository = appUserRepository;
            _appUserService = appUserService;

        }
        public async Task<GetProfileInfoByIdQueryResponse> Handle(GetProfileInfoByIdQueryRequest request, CancellationToken cancellationToken)
        {

            var appuser = await _appUserService.GetAuthenticatedUser();
            //appuser.Data.Id = request.Id;


            //var profileInfoById = await _profileInfoRepository.GetProfileInfoById(request.Id);
            if (appuser.Data == null)
            {
                throw new MasrafTakipCustomException($"{appuser.Data.Id} numaralı Id'ye sahip{nameof(AppUser)} bulunamadı", 404);
            }
            return new GetProfileInfoByIdQueryResponse { AppUser = appuser.Data };
        }
    }
}

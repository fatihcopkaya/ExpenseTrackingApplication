using MasrafTakipYonetim.Application.Cqrs.Queries.ProfileInfoQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.ProfileInfoQueries
{
    public class GetNameByAuthenticatedUserQueryHandler : IRequestHandler<GetNameByAuthenticatedUserQueryRequest, GetNameByAuthenticatedUserQueryResponse>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserService _appUserService;

        public GetNameByAuthenticatedUserQueryHandler(IAppUserRepository appUserRepository, IHttpContextAccessor httpContextAccessor, IAppUserService appUserService)
        {
            _appUserRepository = appUserRepository;
            _httpContextAccessor = httpContextAccessor;
            _appUserService = appUserService;
            _appUserService = appUserService;
        }
        public async Task<GetNameByAuthenticatedUserQueryResponse> Handle(GetNameByAuthenticatedUserQueryRequest request, CancellationToken cancellationToken)
        {

            var authenticatedUser = await _appUserService.GetAuthenticatedUser();
            //string usermail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var userId = (await _appUserRepository.GetListAsync()).Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            //var row = await _appUserRepository.GetFirstOrDefaultAsync(x => x.Id == userId);

            if (authenticatedUser.Data != null)
            {
               
                var response = new GetNameByAuthenticatedUserQueryResponse
                {
                    FirstName = authenticatedUser.Data.FirstName,
                    LastName = authenticatedUser.Data.LastName
                };

                return response;
            }

            
            return null; 
        }




    }
}


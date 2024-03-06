using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserQueries
{
    public class GetAppUserCountQueryHandler : IRequestHandler<GetAppUserCountQueryRequest, Results>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAppUserCountQueryHandler(IAppUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Results> Handle(GetAppUserCountQueryRequest request, CancellationToken cancellationToken)
        {
           

            var userlist = await _userRepository.GetListAsync();
            int appuserCount = userlist.Count();
            

            return Results<GetAppUserCountQueryResponse>.Success(new GetAppUserCountQueryResponse() { AppUserCount = appuserCount});
        }
    }
}

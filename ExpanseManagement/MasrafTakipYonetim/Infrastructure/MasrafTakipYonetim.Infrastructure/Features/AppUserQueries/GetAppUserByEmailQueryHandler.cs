
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserQueries
{
    public class GetAppUserByEmailQueryHandler : IRequestHandler<GetAppUserByEmailQueryRequest, GetAppUserByEmailQueryResponse>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetAppUserByEmailQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<GetAppUserByEmailQueryResponse> Handle(GetAppUserByEmailQueryRequest request, CancellationToken cancellationToken)
        {
            var row = await _appUserRepository.GetAppUserByEmailAsync(request.Email);
            //var row = await _appUserRepository.GetAppUserByEmailAsync(request.Header); bunu profile ınfo önek için yazıldı.
            if (row == null) 
            {
                throw new MasrafTakipCustomException($"{request.Email}'a sahip {nameof(AppUser)} bulunamadı", 404);
            };
            return new GetAppUserByEmailQueryResponse { AppUser = row };
        }
    }
}

using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;

using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.AppUserQueries
{
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQueryRequest, GetAppUserByIdQueryResponse>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetAppUserByIdQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;

        }

        public async Task<GetAppUserByIdQueryResponse> Handle(GetAppUserByIdQueryRequest request, CancellationToken cancellationToken)
        {

           var appUserById = await _appUserRepository.GetAppUserById(request.Id);
           if (appUserById == null) 
           {
                throw new MasrafTakipCustomException($"{request.Id} numaralı Id'ye sahip{nameof(AppUser)} bulunamadı", 404);
            }
           return new GetAppUserByIdQueryResponse { AppUser = appUserById };
            
            
           


        }
        
    }
}

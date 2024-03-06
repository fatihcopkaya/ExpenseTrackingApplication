using MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.UserRoleQueries
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQueryRequest, GetUserRoleByIdQueryResponse>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public GetUserRoleByIdQueryHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;

        }
        public async Task<GetUserRoleByIdQueryResponse> Handle(GetUserRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var userRoleById = await _userRoleRepository.GetUserRoleById(request.Id);
            if (userRoleById.Data == null)
            {
                throw new MasrafTakipCustomException($"{request.Id} Has ID Number{nameof(UserRole)} Could Not Be Found!", 404);
            }
            return new GetUserRoleByIdQueryResponse { UserRole = userRoleById.Data };

        }
    }
}

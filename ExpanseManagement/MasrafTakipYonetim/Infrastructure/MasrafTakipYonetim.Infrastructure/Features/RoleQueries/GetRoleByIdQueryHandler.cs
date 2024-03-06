using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.RoleQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.RoleQueries
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleById(request.Id);
            if (role.Data == null)
            {

                throw new MasrafTakipCustomException($"{request.Id}' ye sahip {nameof(Roles)} bulunamadı", 404);

            }
            return new GetRoleByIdQueryResponse { Roles = role.Data };

        }
    }
}

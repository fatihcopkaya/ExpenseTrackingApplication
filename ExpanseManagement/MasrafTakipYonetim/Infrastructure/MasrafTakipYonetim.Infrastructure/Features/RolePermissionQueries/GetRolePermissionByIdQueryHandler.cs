
using MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.RolePermissionQueries
{
    public class GetRolePermissionByIdQueryHandler : IRequestHandler<GetRolePermissionByIdQueryRequest, GetRolePermissionByIdQueryResponse>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public GetRolePermissionByIdQueryHandler(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<GetRolePermissionByIdQueryResponse> Handle(GetRolePermissionByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var row = await _rolePermissionRepository.GetRolePermissionById(request.RolePermissionId);
            if (row.Data == null)
            {
                throw new MasrafTakipCustomException($"{request.RolePermissionId} Has ID Number {nameof(RolePermission)} Could Not Be Found", 404);
            }
            return new GetRolePermissionByIdQueryResponse { RolePermission = row.Data };
        }
    }
}

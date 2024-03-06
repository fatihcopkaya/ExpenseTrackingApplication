using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.PermissionQueries
{
    public class GetListPermissionQueryHandler : IRequestHandler<GetListPermissionQueryRequest, Results>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetListPermissionQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;

        }
        public async Task<Results> Handle(GetListPermissionQueryRequest request, CancellationToken cancellationToken)
        {
            var row = await _permissionRepository.GetListAsync();

            var permission = row.Select(p => new ListPermissionDto()
            {
                Id = p.Id,
                PermissionName = p.PermissionName,
            }).AsQueryable();

            if (row.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(Permission)} Listelenirken bir hata oluştu",404);
            }


            return Results<List<ListPermissionDto>>.Success(permission.ToList());
        }
    }
}

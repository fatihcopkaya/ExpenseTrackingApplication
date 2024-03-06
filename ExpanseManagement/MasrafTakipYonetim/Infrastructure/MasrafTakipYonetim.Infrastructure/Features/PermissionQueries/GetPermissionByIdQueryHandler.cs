using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.PermissionQueries
{
    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQueryRequest, GetPermissionByIdQueryResponse>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionByIdQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;

        }
        public async Task<GetPermissionByIdQueryResponse> Handle(GetPermissionByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var row = await _permissionRepository.GetPermissionByIdAsync(request.Id);
            if(row.Data == null) 
            {
                throw new MasrafTakipCustomException($"{request.Id}' ye sahip {nameof(Permission)} bulunamadı", 404);
            }
            return new GetPermissionByIdQueryResponse { Permission = row.Data };
        }
    }
}

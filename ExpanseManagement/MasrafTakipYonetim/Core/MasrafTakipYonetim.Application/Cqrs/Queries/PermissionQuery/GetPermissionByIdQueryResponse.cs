using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery
{
    public class GetPermissionByIdQueryResponse:IRequest<GetPermissionByIdQueryRequest>
    {
        public Permission Permission { get; set; }
    }
}

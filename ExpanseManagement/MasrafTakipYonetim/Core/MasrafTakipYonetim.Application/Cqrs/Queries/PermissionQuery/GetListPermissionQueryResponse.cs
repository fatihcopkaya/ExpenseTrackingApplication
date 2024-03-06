using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery
{
    public class GetListPermissionQueryResponse:IRequest<GetListPermissionQueryRequest>
    {
        public List<Permission>? permissions { get; set; }
        public bool Success { get; set; }


    }
}

using MediatR;


namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery
{
    public class GetPermissionByIdQueryRequest:IRequest<GetPermissionByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

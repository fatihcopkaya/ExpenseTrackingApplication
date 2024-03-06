using MediatR;

namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.RoleQuery
{
    public class GetRoleByIdQueryRequest:IRequest<GetRoleByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

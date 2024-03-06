using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.QueriesRequestsAndResponses.RoleQuery
{
    public class GetRoleByIdQueryResponse:IRequest<GetRoleByIdQueryRequest>
    {
        public Roles? Roles { get; set; }
        public string Message { get; set; }

      
    }
}

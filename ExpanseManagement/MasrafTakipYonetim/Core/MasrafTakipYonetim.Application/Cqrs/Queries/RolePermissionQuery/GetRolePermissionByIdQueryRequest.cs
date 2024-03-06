using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery
{
    public class GetRolePermissionByIdQueryRequest:IRequest<GetRolePermissionByIdQueryResponse>
    {

        public Guid RolePermissionId { get; set; }
        //public Guid PermissionId { get; set; }
    }
}

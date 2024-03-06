using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery
{
    public class GetListRolePermissionQueryResponse:IRequest<GetListRolePermissionQueryRequest>
    {

        public List<RolePermission> RolePermissions { get; set; }
        //public List<Permission> Permissions { get; set; }             //Amacı: Belirli bir role ait izinleri listelemek.
        public string Message { get; set; }
    }
}

using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery
{
    public class GetRolePermissionByIdQueryResponse:IRequest<GetRolePermissionByIdQueryRequest>
    {

        public RolePermission RolePermission { get; set; }

        /*public List<Roles> Roles { get; set; } */     //Amacı: Belirli bir izne sahip olan rolleri listelemek.
        public string Message { get; set; }
    }
}

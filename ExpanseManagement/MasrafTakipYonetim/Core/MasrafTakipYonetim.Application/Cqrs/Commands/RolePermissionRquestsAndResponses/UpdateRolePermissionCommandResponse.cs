using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses
{
    public class UpdateRolePermissionCommandResponse:IRequest<UpdateRolePermissionCommandRequest>
    {
        public string Message { get; set; }
    }
}

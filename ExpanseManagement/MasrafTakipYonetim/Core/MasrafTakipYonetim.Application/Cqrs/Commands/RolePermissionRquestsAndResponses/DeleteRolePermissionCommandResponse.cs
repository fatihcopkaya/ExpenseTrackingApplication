using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses
{
    public class DeleteRolePermissionCommandResponse:IRequest<DeleteRolePermissionCommandRequest>
    {
        public string Message { get; set; }
    }
}

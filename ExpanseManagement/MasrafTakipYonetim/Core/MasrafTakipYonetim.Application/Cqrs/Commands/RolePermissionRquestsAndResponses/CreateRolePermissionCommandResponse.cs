using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses
{
    public class CreateRolePermissionCommandResponse:IRequest<CreateRolePermissionCommandRequest>
    {
        public string Message { get; set; }
    }
}

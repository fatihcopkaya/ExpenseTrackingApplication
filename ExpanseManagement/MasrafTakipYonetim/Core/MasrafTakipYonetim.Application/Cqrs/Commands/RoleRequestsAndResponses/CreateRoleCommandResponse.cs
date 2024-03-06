using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses
{
    public class CreateRoleCommandResponse : IRequest<CreateRoleCommandRequest>
    {
        public string Messages { get; set; }
    }
}

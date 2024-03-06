using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses
{
    public class UpdatePermissionCommandResponse : IRequest<UpdatePermissionCommandRequest>
    {
        public string Message { get; set; }
    }
}

using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses
{
    public class DeletePermissionCommandResponse : IRequest<DeletePermissionCommandRequest>
    {
        public string Message { get; set; }
    }
}

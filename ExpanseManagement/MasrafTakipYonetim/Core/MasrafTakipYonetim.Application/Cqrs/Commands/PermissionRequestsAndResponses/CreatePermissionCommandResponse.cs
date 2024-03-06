using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses
{
    public class CreatePermissionCommandResponse : IRequest<CreatePermissionCommandRequest>
    {
        public string Message { get; set; }


    }
}

using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class UpdateAppUserCommandResponse : IRequest<UpdateAppUserCommandRequest>
    {
        public string Message { get; set; }
    }
}

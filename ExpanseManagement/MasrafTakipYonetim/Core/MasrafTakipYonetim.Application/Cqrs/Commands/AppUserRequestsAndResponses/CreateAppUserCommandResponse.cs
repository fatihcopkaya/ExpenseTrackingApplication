using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses

{
    public class CreateAppUserCommandResponse : IRequest<CreateAppUserCommandRequest>
    {

        public string Message { get; set; }


    }
}

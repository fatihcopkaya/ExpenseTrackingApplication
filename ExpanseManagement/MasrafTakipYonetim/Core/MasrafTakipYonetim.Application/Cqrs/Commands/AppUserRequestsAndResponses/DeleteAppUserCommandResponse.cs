
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class DeleteAppUserCommandResponse : IRequest<DeleteAppUserCommandRequest>
    {
        public string Message { get; set; }
        public Guid Id { get; set; }
    }
}

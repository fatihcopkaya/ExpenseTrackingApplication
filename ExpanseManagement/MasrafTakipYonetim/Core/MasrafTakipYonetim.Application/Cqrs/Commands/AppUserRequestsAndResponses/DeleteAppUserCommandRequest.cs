using MediatR;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Commons;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class DeleteAppUserCommandRequest : IRequest<Results>
    {
        // public DeleteAppUserDto deleteAppUserDto { get; set; }
        public Guid Id { get; set; }
        public List<Guid> ExpenseCategoryIds { get; set; }



    }
}

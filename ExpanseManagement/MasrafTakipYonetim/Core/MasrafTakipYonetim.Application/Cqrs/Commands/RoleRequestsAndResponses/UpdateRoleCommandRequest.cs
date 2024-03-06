using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses
{
    public class UpdateRoleCommandRequest : IRequest<Results>
    {
        public Guid Id { get; set; }
        public string ?Name { get; set; }
    }


}


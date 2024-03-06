using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses
{
    public class CreateRoleCommandRequest : IRequest<Results>
    {
        
        public string ?Name { get; set; }



    }
}


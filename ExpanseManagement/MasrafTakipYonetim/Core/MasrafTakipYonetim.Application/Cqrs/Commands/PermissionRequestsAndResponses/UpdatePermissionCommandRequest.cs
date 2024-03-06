using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses
{
    public class UpdatePermissionCommandRequest : IRequest<Results>
    {
        public string ?PermissionName { get; set; }
        public Guid Id { get; set; }
        
    }
}

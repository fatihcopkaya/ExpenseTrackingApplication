using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MediatR;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses
{
    public class DeletePermissionCommandRequest : IRequest<Results>
    {
        public Guid Id { get; set; }
        
    }
}

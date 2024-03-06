using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.Features.PermissionQueries;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.PermissionCommands
{
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommandRequest, Results>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DeletePermissionCommandHandler(IPermissionRepository permissionRepository, IMapper mapper, IMediator mediator)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Results> Handle(DeletePermissionCommandRequest request, CancellationToken cancellationToken)
        {
           

            var deletedPermission = await _permissionRepository.GetPermissionByIdAsync(request.Id);
           
            if (deletedPermission != null)
            {
                deletedPermission.Data.IsDeleted = true;
                var deletedRole = await _permissionRepository.DeletePermissionAsync(deletedPermission.Data);

                if (deletedRole.Success)
                {
                    return Results.Success();
                }
                else
                {
                    throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol silinemedi.", 500);
                }
            }
            else
            {
                throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol bulunamadı.", 404);
            }

           
        }
    }
    
}

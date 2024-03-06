using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.PermissionCommand
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommandRequest, Results>
    {  
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public CreatePermissionCommandHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public async Task<Results> Handle(CreatePermissionCommandRequest request, CancellationToken cancellationToken)
        {
           
            var newPermission = new Permission
            {
                PermissionName = request.PermissionName,

            };

            
            var createdPermission = await _permissionRepository.CreatePermissionAsync(newPermission);
            if (!createdPermission.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(Permission)} oluşturulamadı", 500);
            }
            return Results.Success();

        }
    }
}

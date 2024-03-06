using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.PermissionRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.PermissionQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.PermissionCommands
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommandRequest, Results>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IMapper mapper, IMediator mediator)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Results> Handle(UpdatePermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var updatePermission = await _permissionRepository.GetPermissionByIdAsync(request.Id);

            if (updatePermission != null)
            {
                updatePermission.Data.PermissionName = request.PermissionName;

                try
                {
                    await _permissionRepository.UpdatePermissionAsync(updatePermission.Data);
                    return Results.Success();
                }
                catch (Exception ex)
                {
                    throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol güncellenirken bir hata oluştu: {ex.Message}", 500);
                }
            }
            else
            {
                throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol bulunamadı.", 404);
            }

        }
    }
}

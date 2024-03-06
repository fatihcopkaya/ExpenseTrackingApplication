using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.RolePermissionCommands
{
    public class DeleteRolePermissionCommandHandler : IRequestHandler<DeleteRolePermissionCommandRequest, Results>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;

        public DeleteRolePermissionCommandHandler(IMediator mediator, IMapper mapper, IRolePermissionRepository rolePermissionRepository, IRoleRepository roleRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Results> Handle(DeleteRolePermissionCommandRequest request, CancellationToken cancellationToken)
        {


            var superadminRole = await _roleRepository.GetRoleById(request.RoleId);
            Guid superAdminRoleId = new Guid("C9F40AF4-31A5-4D22-5E6D-08DBFB20E122"); // Süper adminin rol ID'sini burada belirttim

            // Eğer istek, süper adminin rol ID'sine eşitse, yetki değiştirme işlemi gerçekleşmemeli
            if (request.RoleId == superAdminRoleId)
            {
                throw new MasrafTakipCustomException($"Süper adminin yetkileri silinemez.", 400);
            }
            var rolePermissionsToDelete = await _rolePermissionRepository.GetRolePermissionByRoleId(request.RoleId);

            List<string> permissionIdStrings = request.PermissionIds.Split(',').ToList();

            // Listeyi Guid listesine çevirme
            List<Guid> permissionIds = permissionIdStrings.Select(Guid.Parse).ToList();

            foreach (var permissionId in permissionIds) // .Data koleksiyonunu işleyin
            {


                var rolePermission = rolePermissionsToDelete.Data.FirstOrDefault(rp => rp.PermissionId == permissionId);
                // var rolePermission = rolePermissionsToDelete.Data.FirstOrDefault(rp => rp.PermissionId.ToString() == permissionId);

                if (rolePermission != null)
                {

                 
                    rolePermission.IsDeleted = true;
                    var deletedRole = await _rolePermissionRepository.DeleteRolePermission(rolePermission);

                    if (!deletedRole.Success)
                    {
                        throw new MasrafTakipCustomException($"{rolePermission.PermissionId} ID'sine sahip yetki silinemedi.", 500);
                    }
                }
                else
                {
                    throw new MasrafTakipCustomException($"Yetki bulunamadı.", 404);
                }
            }
            return Results.Success();
        }
    }
}

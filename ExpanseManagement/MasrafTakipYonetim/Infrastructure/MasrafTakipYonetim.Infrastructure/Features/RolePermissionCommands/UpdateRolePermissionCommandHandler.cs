using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System.Linq;

namespace MasrafTakipYonetim.Infrastructure.Features.RolePermissionCommands

{
    public class UpdateRolePermissionCommandHandler : IRequestHandler<UpdateRolePermissionCommandRequest, Results>
    {
        IMediator _mediator;
        IMapper _mapper;
        IRolePermissionRepository _rolePermissionRepository;
        IRoleRepository _roleRepository;

        public UpdateRolePermissionCommandHandler(IMediator mediator, IMapper mapper, IRolePermissionRepository rolePermissionRepository,IRoleRepository roleRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Results> Handle(UpdateRolePermissionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var superadminRole= await _roleRepository.GetRoleById(request.RoleId);
                Guid superAdminRoleId = new Guid("76FE887F-29AC-426E-B3D5-08DC12EC0AB5"); // Süper adminin rol ID'sini burada belirttim

                // Eğer istek, süper adminin rol ID'sine eşitse, yetki değiştirme işlemi gerçekleşmemeli
                if (request.RoleId == superAdminRoleId)
                {
                    throw new MasrafTakipCustomException($"Süper adminin yetkileri değiştirilemez.", 400);
                }

                var existingRolePermissions = await _rolePermissionRepository.GetRolePermissionByRoleId(request.RoleId);




                if (existingRolePermissions == null || existingRolePermissions.Data == null)
                {
                    throw new MasrafTakipCustomException($"Bu rol için atanmış herhangi bir yetki bulunmamaktadır.", 404);
                }

                var rolePermissions = existingRolePermissions.Data;

                foreach (var rolePermission in rolePermissions)
                {
                    if (rolePermission.PermissionId == request.PermissionIds && rolePermission.RoleId == request.RoleId)
                    {
                        throw new MasrafTakipCustomException($"Bu yetki zaten bu role atanmış durumdadır.", 400);
                    }
                }



                var existingRolePermission = await _rolePermissionRepository.GetRolePermissionById(request.Id);

                if (existingRolePermission == null)
                {
                    throw new MasrafTakipCustomException($"{request.Id} ID Rol Yetkisi Bulunamadı!", 404);
                }


                if (existingRolePermission.Data.PermissionId != request.PermissionIds)
                {
                    // Mevcut RolePermission nesnesini güncelle
                    existingRolePermission.Data.Id = request.Id;
                    existingRolePermission.Data.RoleId = request.RoleId;
                    existingRolePermission.Data.PermissionId = request.PermissionIds;
                    // Diğer özellikler de burada gerekirse güncellenebilir

                    var updatedRolePermission = await _rolePermissionRepository.UpdateRolePermission(existingRolePermission.Data);

                    if (!updatedRolePermission.Success)
                    {
                        throw new MasrafTakipCustomException($"{request.Id} Id'li {nameof(RolePermission)} Güncellenemedi", 500);
                    }
                }

                return Results.Success(); // Başarılı sonucu dön
            }
            catch (Exception ex)
            {
                throw new MasrafTakipCustomException($"An error occurred while updating RolePermission: {ex.Message}", 500);
            }
        }
    }
}

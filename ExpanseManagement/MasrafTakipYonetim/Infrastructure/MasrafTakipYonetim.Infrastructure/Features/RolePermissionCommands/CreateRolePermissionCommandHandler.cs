using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RolePermissionRquestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.RolePermissionCommands
{
    public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommandRequest, Results>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;

        public CreateRolePermissionCommandHandler(IRolePermissionRepository rolePermissionRepository, IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<Results> Handle(CreateRolePermissionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roleId = request.RoleId;
                var permissionIds = request.PermissionIds;

                var existingRolePermissions = await _rolePermissionRepository.GetRolePermissionByRoleId(roleId);
                // var existingRolePermissionsData = existingRolePermissions.Data;
                if (existingRolePermissions == null || existingRolePermissions.Data == null)
                {
                    // Rol için atanmış herhangi bir yetki yoksa uygun bir işlem yapılmalı
                    throw new MasrafTakipCustomException("Rolle ilişkilendirilmiş izin bulunamadı!", 404);
                }

                var existingRolePermissionsData = existingRolePermissions.Data;


                var duplicatedPermissions = existingRolePermissionsData
                    .Where(rp => permissionIds.Contains(rp.PermissionId))
                    .ToList();


                if (duplicatedPermissions.Any())
                {
                    var duplicatedPermissionNames = duplicatedPermissions
                        .Where(rp => rp.Permission != null && rp.Permission.PermissionName != null)
                        .Select(rp => rp.Permission.PermissionName)
                        .ToList();

                    throw new MasrafTakipCustomException($"Bu izinler zaten rolle ilişkilendirilmiş: {string.Join(", ", duplicatedPermissionNames)}", 400);
                }

                foreach (var permissionId in permissionIds)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    };

                    var result = await _rolePermissionRepository.CreateRolePermission(rolePermission);

                    if (!result.Success)
                    {
                        throw new MasrafTakipCustomException($"{nameof(RolePermission)} Could Not Be Created!", 500);
                    }
                }
                return Results.Success();
            }
            catch (Exception ex)
            {
                throw new MasrafTakipCustomException($"An error occurred while creating RolePermission: {ex.Message}", 500);
            }
        }
    }
}

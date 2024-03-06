using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.PermissionQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.PermissionQueries
{
    public class ListPermissionsByRoleIdHandler : IRequestHandler<ListPermissionByRoleIdRequest, Results>
    {

        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public ListPermissionsByRoleIdHandler(IPermissionRepository permissionRepository, IRoleRepository roleRepository,IRolePermissionRepository rolePermissionRepository)
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<Results> Handle(ListPermissionByRoleIdRequest request, CancellationToken cancellationToken)
        {
            var listpermissions = await _rolePermissionRepository.GetListAsync(x => x.IsDeleted == false,includes:x=>x.Permission);
            var roleResult = await _roleRepository.GetRoleById(request.RoleId);
           // var permissionResult = await _permissionRepository.GetListAsync();

            if (roleResult.Success )
            {
                var roles = roleResult.Data;
                var permissionsDto = roles.RolePermissions.Select(p => new ListPermissionByRoleIdDto
                {
                    Id = p.PermissionId,
                    PermissionName = p.Permission.PermissionName,
                }).AsQueryable();


                if (!string.IsNullOrEmpty(request.PermissionName))
                {
                    permissionsDto = permissionsDto.Where(x =>
                    (string.IsNullOrEmpty(request.PermissionName) ||
                     x.PermissionName.ToLower().Contains(request.PermissionName.Trim().ToLower()) ||
                     x.PermissionName.ToUpper().Contains(request.PermissionName.Trim().ToUpper()))
                );
                }




                return Results<List<ListPermissionByRoleIdDto>>.Success(permissionsDto.ToList());

            }

            else
            {
                throw new MasrafTakipCustomException($"{nameof(Permission)} Listelenirken bir hata oluştu", 404);
            }

        
            


        }
    } 
}

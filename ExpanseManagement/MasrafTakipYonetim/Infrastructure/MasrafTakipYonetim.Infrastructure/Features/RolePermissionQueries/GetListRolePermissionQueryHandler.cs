
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.RolePermissionQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MassTransit.Internals.GraphValidation;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.RolePermissionQueries
{
    public class GetListRolePermissionQueryHandler : IRequestHandler<GetListRolePermissionQueryRequest, Results>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public GetListRolePermissionQueryHandler(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async  Task<Results> Handle(GetListRolePermissionQueryRequest request, CancellationToken cancellationToken)
        {



            var rolePermissionList = await _rolePermissionRepository.GetRolePermissionList();

            var rolePermission = rolePermissionList.Data.Select(p=>new ListRolePermissionDto()
            {

                Id = p.Id,
                RoleId = p.RoleId,
                RoleName = p.Role.Name,
                PermissionId=p.Permission.Id,
                PermissionName=p.Permission.PermissionName,


                
            }).AsQueryable();

            //if (rolePermission == null)
            //{
            //    throw new MasrafTakipCustomException($"{nameof(RolePermission)} Listesi bulunamadı", 404);
            //};

            Results result;
            //if (!string.IsNullOrEmpty(request.RoleName) || !string.IsNullOrEmpty(request.PermissionName))
            //{
            //        rolePermission = rolePermission.Where(x =>
            //        (string.IsNullOrEmpty(request.RoleName) ||
            //         x.RoleName.ToLower().Contains(request.RoleName.Trim().ToLower()) ||
            //         x.RoleName.ToUpper().Contains(request.RoleName.Trim().ToUpper())) &&
            //        (string.IsNullOrEmpty(request.PermissionName) ||
            //         x.PermissionName.ToLower().Contains(request.PermissionName.Trim().ToLower()) ||
            //         x.PermissionName.ToUpper().Contains(request.PermissionName.Trim().ToUpper()))
            //    );
            //}



            if (rolePermission == null)
            {
                throw new MasrafTakipCustomException($"{nameof(RolePermission)} Listesi bulunamadı", 404);
            };

            return Results<List<ListRolePermissionDto>>.Success(rolePermission.ToList());





        }
    }
}

using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.RoleQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.RoleQueries
{
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQueryRequest, Results>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleListQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Results> Handle(GetRoleListQueryRequest request, CancellationToken cancellationToken)
        {
            var rolelist = await _roleRepository.GetListAsync(x => !x.IsDeleted);
            var roleListDto = rolelist.Select(x => new ListRoleDto()
            {
                Id=x.Id,
                Name=x.Name,

            }).AsQueryable();

            if (rolelist.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(AppUser)} Listesi bulunamadı", 404);
            };

            return Results<List<ListRoleDto>>.Success(roleListDto.ToList());
        }
    }
}

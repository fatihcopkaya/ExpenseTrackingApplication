using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.UserRole;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;

namespace MasrafTakipYonetim.Infrastructure.Features.UserRoleQueries
{
    public class GetUserRoleListQueryHandler : IRequestHandler<GetUserRoleListQueryRequest, Results>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public GetUserRoleListQueryHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;

        }
        public  async Task<Results> Handle(GetUserRoleListQueryRequest request, CancellationToken cancellationToken)
        {
            var userRolelist = await _userRoleRepository.GetUserRoleList();

            var userRoleListDto = userRolelist.Data.Select(R => new ListUserRoleDto()
            {
                Id=R.Id,
                AppUserId=R.AppUserId,
                AppUserFirstName=R.User.FirstName,
                AppUserLastName=R.User.LastName,
                RoleId=R.RoleId,
                RoleName=R.Role.Name


            }).AsQueryable();

            Results result;

            if (!string.IsNullOrEmpty(request.AppUserFirstName) || !string.IsNullOrEmpty(request.RoleName)|| !string.IsNullOrEmpty(request.AppUserLastName))
            {
                userRoleListDto = userRoleListDto.Where(x =>
                    (string.IsNullOrEmpty(request.AppUserFirstName) ||
                     x.AppUserFirstName.ToLower().Contains(request.AppUserFirstName.Trim().ToLower()) ||
                     x.AppUserFirstName.ToUpper().Contains(request.AppUserFirstName.Trim().ToUpper())) &&
                    (string.IsNullOrEmpty(request.RoleName) ||
                     x.RoleName.ToLower().Contains(request.RoleName.Trim().ToLower()) ||
                     x.RoleName.ToUpper().Contains(request.RoleName.Trim().ToUpper()))&&
                      (string.IsNullOrEmpty(request.AppUserLastName) ||
                     x.AppUserLastName.ToLower().Contains(request.AppUserLastName.Trim().ToLower()) ||
                     x.AppUserLastName.ToUpper().Contains(request.AppUserLastName.Trim().ToUpper()))
                );
            }



            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                userRoleListDto = userRoleListDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(userRoleListDto.OrderBy(x => x.AppUserId)
                    .QueryResourceNotMapped<ListUserRoleDto>(request.PageNumber, request.PageSize));
            }



            result = await Task.FromResult(userRoleListDto.QueryResourceNotMapped<ListUserRoleDto>(request.PageNumber, request.PageSize));
            return result;
        }
    }
}

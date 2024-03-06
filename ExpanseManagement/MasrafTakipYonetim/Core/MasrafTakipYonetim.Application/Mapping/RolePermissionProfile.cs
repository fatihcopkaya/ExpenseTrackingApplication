using MasrafTakipYonetim.Application.Dtos.RolePermission;
using MasrafTakipYonetim.Domain.Entities;
using AutoMapper;


namespace MasrafTakipYonetim.Application.Mapping
{
    public class RolePermissionProfile:Profile
    {
        public RolePermissionProfile()
        {

            CreateMap<CreateRolePermissionRequesDto, RolePermission>().ReverseMap();
            CreateMap<DeleteRolePermissionRequestDto, RolePermission>().ReverseMap();
            CreateMap<UpdateRolePermissionRequestDto, RolePermission>().ReverseMap();
            
        }
    }
}

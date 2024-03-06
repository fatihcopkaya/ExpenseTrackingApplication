using AutoMapper;
using MasrafTakipYonetim.Application.Dtos.Permission;
using MasrafTakipYonetim.Domain.Entities;


namespace MasrafTakipYonetim.Application.Mapping
{
    public class PermissionProfile:Profile
    {
        public PermissionProfile() 
        {
            CreateMap<CreatePermissionDto, Permission>().ReverseMap();
            CreateMap<UpdatePermissionDto, Permission>().ReverseMap();
            CreateMap<DeletePermissionDto, Permission>().ReverseMap();


        }
    }
}
